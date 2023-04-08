using System.Globalization;
using System.Text;

namespace RolePlayReady.DataAccess;

public class DataFileRepositoryTests {
    private readonly IIOProvider _io;
    private readonly IDateTimeProvider _dateTime;
    private readonly DataFileRepository _repository;

    private const string _baseFolder = "testBaseFolder";
    private const string _path = "testPath";
    private const string _id1 = "testId1";
    private const string _id2 = "testId2";

    public DataFileRepositoryTests() {
        _io = Substitute.For<IIOProvider>();
        _dateTime = Substitute.For<IDateTimeProvider>();
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(DataFileRepository)}:BaseFolder"].Returns(_baseFolder);
        _repository = new DataFileRepository(configuration, _io, _dateTime);
    }


    [Fact]
    public void Constructor_WithoutProxies_CreatesInstance() {
        // Arrange
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(DataFileRepository)}:BaseFolder"].Returns(_baseFolder);

        // Act
        var result = new DataFileRepository(configuration, null, null);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAllAsync_PathGiven_ReturnsAllDataFiles() {
        // Arrange
        var testFolderPath = $"{_baseFolder}/{_path}";
        var filePaths = new[] {
            $"{testFolderPath}/+{_id1}_20220406T120000.json",
            $"{testFolderPath}/+{_id2}_20220406T130000.json"
        };

        _io.CombinePath(_baseFolder, _path).Returns(testFolderPath);
        _io.GetFilesFrom(testFolderPath, "+*.json", SearchOption.TopDirectoryOnly).Returns(filePaths);

        _io.ExtractFileNameFrom(filePaths[0]).Returns($"+{_id1}_20220406T120000.json");
        _dateTime.TryParseExact("20220406T120000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:00:00");
                return true;
            });
        _io.OpenFileForReading(filePaths[0]).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"SomeName\",\"Number\":42}")));

        _io.ExtractFileNameFrom(filePaths[1]).Returns($"+{_id2}_20220406T130000.json");
        _dateTime.TryParseExact("20220406T130000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 13:00:00");
                return true;
            });
        _io.OpenFileForReading(filePaths[1]).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"OtherName\",\"Number\":7}")));

        // Act
        var result = (await _repository.GetAllAsync<TestData>(_path)).ToArray();

        // Assert
        result.Should().HaveCount(2);
        result.First().Id.Should().Be(_id1);
        result.First().Content.Name.Should().Be("SomeName");
        result.Last().Id.Should().Be(_id2);
        result.Last().Content.Name.Should().Be("OtherName");
    }

    [Fact]
    public async Task GetAllAsync_NoPathGiven_ReturnsAllDataFiles() {
        // Arrange
        var filePaths = new[] {
            $"{_baseFolder}/+{_id1}_20220406T120000.json",
            $"{_baseFolder}/+{_id2}_20220406T130000.json"
        };

        _io.CombinePath(_baseFolder, string.Empty).Returns(_baseFolder);
        _io.GetFilesFrom(_baseFolder, "+*.json", SearchOption.TopDirectoryOnly).Returns(filePaths);

        _io.ExtractFileNameFrom(filePaths[0]).Returns($"+{_id1}_20220406T120000.json");
        _dateTime.TryParseExact("20220406T120000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:00:00");
                return true;
            });
        _io.OpenFileForReading(filePaths[0]).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"SomeName\",\"Number\":42}")));

        _io.ExtractFileNameFrom(filePaths[1]).Returns($"+{_id2}_20220406T130000.json");
        _dateTime.TryParseExact("20220406T130000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 13:00:00");
                return true;
            });
        _io.OpenFileForReading(filePaths[1]).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"OtherName\",\"Number\":7}")));

        // Act
        var result = (await _repository.GetAllAsync<TestData>()).ToArray();

        // Assert
        result.Should().HaveCount(2);
        result.First().Id.Should().Be(_id1);
        result.First().Content.Name.Should().Be("SomeName");
        result.Last().Id.Should().Be(_id2);
        result.Last().Content.Name.Should().Be("OtherName");
    }

    [Fact]
    public async Task GetByIdAsync_PathAndIdGiven_DataFileFound_ReturnsDataFile() {
        // Arrange
        var filePath = $"{_baseFolder}/{_path}/+{_id1}_20220406T123456.json";
        var expectedData = GenerateTestData();

        _io.CombinePath(_baseFolder, _path).Returns($"{_baseFolder}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { filePath });

        _io.ExtractFileNameFrom(filePath).Returns($"+{_id1}_20220406T123456.json");
        _dateTime.TryParseExact("20220406T123456", "yyyyMMddTHHmmss", null, DateTimeStyles.None, out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:34:56");
                return true;
            });
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"SomeName\",\"Number\":42}"));
        _io.OpenFileForReading(filePath).Returns(memoryStream);

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_path, _id1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(_id1);
        result.Timestamp.Should().Be(DateTime.Parse("2022-04-06 12:34:56"));
        result.Content.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task GetByIdAsync_PathAndIdGiven_DataFileNotFound_ReturnsNull() {
        // Arrange
        _io.CombinePath(_baseFolder, _path).Returns($"{_baseFolder}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(Array.Empty<string>());

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_path, _id1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_IdGiven_DataFileFound_ReturnsDataFile() {
        // Arrange
        var filePath = $"{_baseFolder}/+{_id1}_20220406T123456.json";
        var expectedData = GenerateTestData();

        _io.CombinePath(_baseFolder, string.Empty).Returns(_baseFolder);
        _io.GetFilesFrom(_baseFolder, $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { filePath });

        _io.ExtractFileNameFrom(filePath).Returns($"+{_id1}_20220406T123456.json");
        _dateTime.TryParseExact("20220406T123456", "yyyyMMddTHHmmss", null, DateTimeStyles.None, out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:34:56");
                return true;
            });
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"SomeName\",\"Number\":42}"));
        _io.OpenFileForReading(filePath).Returns(memoryStream);

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_id1);

        // Assert
        result.Should().NotBeNull();
        result!.Id.Should().Be(_id1);
        result.Timestamp.Should().Be(DateTime.Parse("2022-04-06 12:34:56"));
        result.Content.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task GetByIdAsync_IdGiven_DataFileNotFound_ReturnsNull() {
        // Arrange
        _io.CombinePath(_baseFolder, string.Empty).Returns(_baseFolder);
        _io.GetFilesFrom($"{_baseFolder}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(Array.Empty<string>());

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_id1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpsertAsync_PathAndIdGiven_InsertsNewDataFile() {
        // Arrange
        var currentFilePath = $"{_baseFolder}/{_path}/+{_id1}_20220406T120000.json";
        var newFilePath = $"{_baseFolder}/{_path}/+{_id1}_20220406T230000.json";
        var data = GenerateTestData();

        _io.CombinePath(_baseFolder, _path).Returns($"{_baseFolder}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { currentFilePath });
        _dateTime.Now.Returns(DateTime.Parse("2022-04-06 23:00:00"));
        _io.When(x => x.MoveFile(currentFilePath, currentFilePath.Replace("+", ""))).Do(_ => { });
        _io.CombinePath($"{_baseFolder}/{_path}", $"+{_id1}_20220406T230000.json").Returns(newFilePath);
        var buffer = new byte[1024];
        using var memoryStream = new MemoryStream(buffer, true);
        _io.CreateNewFileAndOpenForWriting(newFilePath).Returns(memoryStream);

        // Act
        await _repository.UpsertAsync(_path, _id1, data);

        // Assert
        _io.Received(1).MoveFile(currentFilePath, currentFilePath.Replace("+", ""));
        _io.Received(1).CreateNewFileAndOpenForWriting(newFilePath);
    }

    [Fact]
    public async Task UpsertAsync_IdGiven_InsertsNewDataFile() {
        // Arrange
        var currentFilePath = $"{_baseFolder}/+{_id1}_20220406T120000.json";
        var newFilePath = $"{_baseFolder}/+{_id1}_20220406T230000.json";
        var data = GenerateTestData();

        _io.CombinePath(_baseFolder, string.Empty).Returns(_baseFolder);
        _io.GetFilesFrom(_baseFolder, $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { currentFilePath });
        _dateTime.Now.Returns(DateTime.Parse("2022-04-06 23:00:00"));
        _io.When(x => x.MoveFile(currentFilePath, currentFilePath.Replace("+", ""))).Do(_ => { });
        _io.CombinePath(_baseFolder, $"+{_id1}_20220406T230000.json").Returns(newFilePath);
        var buffer = new byte[1024];
        using var memoryStream = new MemoryStream(buffer, true);
        _io.CreateNewFileAndOpenForWriting(newFilePath).Returns(memoryStream);

        // Act
        await _repository.UpsertAsync(_id1, data);

        // Assert
        _io.Received(1).MoveFile(currentFilePath, currentFilePath.Replace("+", ""));
        _io.Received(1).CreateNewFileAndOpenForWriting(newFilePath);
    }

    [Fact]
    public void Delete_PathAndIdGiven_RemovesDataFile() {
        // Arrange
        var activeFilePath = $"{_baseFolder}/{_path}/+{_id1}_20220406T123456.json";
        var inactiveFilePath = $"{_baseFolder}/{_path}/{_id1}_20220405T120000.json";
        var deletedFilePath1 = $"{_baseFolder}/{_path}/-{_id1}_20220406T123456.json";
        var deletedFilePath2 = $"{_baseFolder}/{_path}/-{_id1}_20220405T120000.json";

        _io.CombinePath(_baseFolder, _path).Returns($"{_baseFolder}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { activeFilePath });
        _io.GetFilesFrom($"{_baseFolder}/{_path}", $"{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { inactiveFilePath });

        _io.ExtractFileNameFrom(activeFilePath).Returns($"+{_id1}_20220406T123456.json");
        _io.CombinePath($"{_baseFolder}/{_path}", $"-{_id1}_20220406T123456.json").Returns(deletedFilePath1);
        _io.When(x => x.MoveFile(activeFilePath, deletedFilePath1)).Do(_ => { });

        _io.ExtractFileNameFrom(inactiveFilePath).Returns($"{_id1}_20220406T120000.json");
        _io.CombinePath($"{_baseFolder}/{_path}", $"-{_id1}_20220406T120000.json").Returns(deletedFilePath2);
        _io.When(x => x.MoveFile(inactiveFilePath, deletedFilePath2)).Do(_ => { });

        // Act
        _repository.Delete(_path, _id1);

        // Assert
        _io.Received(1).MoveFile(activeFilePath, deletedFilePath1);
        _io.Received(1).MoveFile(inactiveFilePath, deletedFilePath2);
    }

    [Fact]
    public void Delete_IdGiven_RemovesDataFile() {
        // Arrange
        var activeFilePath = $"{_baseFolder}/+{_id1}_20220406T123456.json";
        var inactiveFilePath = $"{_baseFolder}/{_id1}_20220405T120000.json";
        var deletedFilePath1 = $"{_baseFolder}/-{_id1}_20220406T123456.json";
        var deletedFilePath2 = $"{_baseFolder}/-{_id1}_20220405T120000.json";

        _io.CombinePath(_baseFolder, string.Empty).Returns(_baseFolder);
        _io.GetFilesFrom(_baseFolder, $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { activeFilePath });
        _io.GetFilesFrom(_baseFolder, $"{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { inactiveFilePath });

        _io.ExtractFileNameFrom(activeFilePath).Returns($"+{_id1}_20220406T123456.json");
        _io.CombinePath(_baseFolder, $"-{_id1}_20220406T123456.json").Returns(deletedFilePath1);
        _io.When(x => x.MoveFile(activeFilePath, deletedFilePath1)).Do(_ => { });

        _io.ExtractFileNameFrom(inactiveFilePath).Returns($"{_id1}_20220406T120000.json");
        _io.CombinePath(_baseFolder, $"-{_id1}_20220406T120000.json").Returns(deletedFilePath2);
        _io.When(x => x.MoveFile(inactiveFilePath, deletedFilePath2)).Do(_ => { });

        // Act
        _repository.Delete(_id1);

        // Assert
        _io.Received(1).MoveFile(activeFilePath, deletedFilePath1);
        _io.Received(1).MoveFile(inactiveFilePath, deletedFilePath2);
    }

    private TestData GenerateTestData()
        => new() {
            Name = "SomeName",
            Number = 42,
        };

    private DataFile<TestData> GenerateDataFile()
        => new() {
            Id = _id1,
            Timestamp = DateTime.UtcNow,
            Content = GenerateTestData(),
        };

    private IEnumerable<DataFile<TestData>> GenerateDataFiles()
        => new[] { GenerateDataFile() };

    private record TestData {
        public required string Name { get; init; }
        public required int Number { get; init; }
    }
}
