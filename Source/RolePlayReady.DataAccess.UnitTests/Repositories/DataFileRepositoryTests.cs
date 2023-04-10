using NSubstitute.ExceptionExtensions;

namespace RolePlayReady.DataAccess.Repositories;

public class DataFileRepositoryTests {
    private readonly IFileSystem _io;
    private readonly IDateTime _dateTime;
    private readonly DataFileRepository _repository;

    private const string _baseFolder = "testBaseFolder";
    private const string _owner = "owner";
    private const string _path = "testPath";
    private const string _id1 = "testId1";
    private const string _id2 = "testId2";

    public DataFileRepositoryTests() {
        _io = Substitute.For<IFileSystem>();
        _dateTime = Substitute.For<IDateTime>();
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(DataFileRepository)}:BaseFolder"].Returns(_baseFolder);
        _repository = new DataFileRepository(configuration, _io, _dateTime, NullLoggerFactory.Instance);
    }

    [Fact]
    public void Constructor_WithNulls_CreatesInstance() {
        // Arrange
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(DataFileRepository)}:BaseFolder"].Returns(_baseFolder);

        // Act
        var result = new DataFileRepository(configuration, null, null, null);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithConfigurationMissing_Throws() {
        // Arrange
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(DataFileRepository)}:BaseFolder"].Returns(default(string));

        // Act
        var action = () => new DataFileRepository(configuration, null, null, null);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public async Task GetAllAsync_PathGiven_ReturnsAllDataFiles() {
        // Arrange
        const string testFolderPath = $"{_baseFolder}/{_owner}/{_path}";
        var filePaths = new[] {
            $"{testFolderPath}/+{_id1}_20220406120000.json",
            $"{testFolderPath}/+{_id2}_20220406130000.json"
        };

        _io.CombinePath(_baseFolder, _owner, _path).Returns(testFolderPath);
        _io.GetFilesFrom(testFolderPath, "+*.json", SearchOption.TopDirectoryOnly).Returns(filePaths);

        _io.ExtractFileNameFrom(filePaths[0]).Returns($"+{_id1}_20220406120000.json");
        _dateTime.TryParseExact("20220406120000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:00:00");
                return true;
            });
        _io.OpenFileForReading(filePaths[0]).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"SomeName\",\"Number\":42}")));

        _io.ExtractFileNameFrom(filePaths[1]).Returns($"+{_id2}_20220406130000.json");
        _dateTime.TryParseExact("20220406130000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 13:00:00");
                return true;
            });
        _io.OpenFileForReading(filePaths[1]).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"OtherName\",\"Number\":7}")));

        // Act
        var result = (await _repository.GetAllAsync<TestData>(_owner, _path)).ToArray();

        // Assert
        result.Should().HaveCount(2);
        result.First().Name.Should().Be(_id1);
        result.First().Content.Name.Should().Be("SomeName");
        result.First().Content.Number.Should().Be(42);
        result.Last().Name.Should().Be(_id2);
        result.Last().Content.Name.Should().Be("OtherName");
        result.Last().Content.Number.Should().Be(7);
    }

    [Fact]
    public async Task GetAllAsync_WithException_ReturnsEmpty() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Throws<Exception>();

        // Act
        var result = await _repository.GetAllAsync<TestData>(_owner, _path);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsync_WithInvalidFile_ReturnsOnlyValidOnes() {
        // Arrange
        const string testFolderPath = $"{_baseFolder}/{_owner}/{_path}";
        var filePaths = new[] {
            $"{testFolderPath}/+{_id1}_20220406120000.json",
            $"{testFolderPath}/+{_id2}_20220406130000.json"
        };

        _io.CombinePath(_baseFolder, _owner, _path).Returns(testFolderPath);
        _io.GetFilesFrom(testFolderPath, "+*.json", SearchOption.TopDirectoryOnly).Returns(filePaths);

        _io.ExtractFileNameFrom(filePaths[0]).Returns($"+{_id1}_20220406120000.json");
        _dateTime.TryParseExact("20220406120000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:00:00");
                return true;
            });
        _io.OpenFileForReading(filePaths[0]).Returns(new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"SomeName\",\"Number\":42}")));

        _io.ExtractFileNameFrom(filePaths[1]).Returns($"+{_id2}_20220406130000.json");
        _dateTime.TryParseExact("20220406130000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 13:00:00");
                return true;
            });
        _io.OpenFileForReading(filePaths[1]).Returns(new MemoryStream(Encoding.UTF8.GetBytes("Invalid")));

        // Act
        var result = (await _repository.GetAllAsync<TestData>(_owner, _path)).ToArray();

        // Assert
        result.Should().HaveCount(1);
        result.First().Name.Should().Be(_id1);
        result.First().Content.Name.Should().Be("SomeName");
        result.First().Content.Number.Should().Be(42);
    }

    [Fact]
    public async Task GetByIdAsync_PathAndIdGiven_DataFileFound_ReturnsDataFile() {
        // Arrange
        const string filePath = $"{_baseFolder}/{_owner}/{_path}/+{_id1}_20220406123456.json";
        var expectedData = GenerateTestData();

        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { filePath });

        _io.ExtractFileNameFrom(filePath).Returns($"+{_id1}_20220406123456.json");
        _dateTime.TryParseExact("20220406123456", "yyyyMMddHHmmss", null, DateTimeStyles.None, out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:34:56");
                return true;
            });
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("{\"Name\":\"SomeName\",\"Number\":42}"));
        _io.OpenFileForReading(filePath).Returns(memoryStream);

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_owner, _path, _id1);

        // Assert
        result.Should().NotBeNull();
        result!.Name.Should().Be(_id1);
        result.Timestamp.Should().Be(DateTime.Parse("2022-04-06 12:34:56"));
        result.Content.Should().BeEquivalentTo(expectedData);
    }


    [Fact]
    public async Task GetByIdAsync_PathAndIdGiven_DataFileNotFound_ReturnsNull() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(Array.Empty<string>());

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_owner, _path, _id1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidFileName_ReturnsNull() {
        // Arrange
        var filePath = $"{_baseFolder}/+{_id1}_invalid.json";

        _io.CombinePath(_baseFolder, _owner, _path).Returns(_baseFolder);
        _io.GetFilesFrom(_baseFolder, $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { filePath });

        _io.ExtractFileNameFrom(filePath).Returns($"+{_id1}_invalid.json");

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_owner, _path, _id1);

        // Assert
        result.Should().BeNull();
    }


    [Fact]
    public async Task GetByIdAsync_WithInvalidFileTimestamp_ReturnsNull() {
        // Arrange
        var filePath = $"{_baseFolder}/+{_id1}_99999999999999.json";

        _io.CombinePath(_baseFolder, _owner, _path).Returns(_baseFolder);
        _io.GetFilesFrom(_baseFolder, $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { filePath });

        _io.ExtractFileNameFrom(filePath).Returns($"+{_id1}_99999999999999.json");

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_owner, _path, _id1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidFileContent_ReturnsNull() {
        // Arrange
        var filePath = $"{_baseFolder}/+{_id1}_20220406123456.json";

        _io.CombinePath(_baseFolder, _owner, _path).Returns(_baseFolder);
        _io.GetFilesFrom(_baseFolder, $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { filePath });

        _io.ExtractFileNameFrom(filePath).Returns($"+{_id1}_20220406123456.json");
        _dateTime.TryParseExact("20220406123456", "yyyyMMddHHmmss", null, DateTimeStyles.None, out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:34:56");
                return true;
            });
        using var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("Invalid file."));
        _io.OpenFileForReading(filePath).Returns(memoryStream);

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_owner, _path, _id1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithException_ReturnsNull() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Throws<Exception>();

        // Act
        var result = await _repository.GetByIdAsync<TestData>(_owner, _path, _id1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpsertAsync_PathAndIdGiven_InsertsNewDataFile() {
        // Arrange
        var currentFilePath = $"{_baseFolder}/{_owner}/{_path}/+{_id1}_20220406120000.json";
        var newFilePath = $"{_baseFolder}/{_owner}/{_path}/+{_id1}_20220406230000.json";
        var data = GenerateTestData();

        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { currentFilePath });
        _dateTime.Now.Returns(DateTime.Parse("2022-04-06 23:00:00"));
        _io.When(x => x.MoveFile(currentFilePath, currentFilePath.Replace("+", ""))).Do(_ => { });
        _io.CombinePath($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}_20220406230000.json").Returns(newFilePath);
        var buffer = new byte[1024];
        using var memoryStream = new MemoryStream(buffer, true);
        _io.CreateNewFileAndOpenForWriting(newFilePath).Returns(memoryStream);

        // Act
        await _repository.UpsertAsync(_owner, _path, _id1, data);

        // Assert
        _io.Received(1).MoveFile(currentFilePath, currentFilePath.Replace("+", ""));
        _io.Received(1).CreateNewFileAndOpenForWriting(newFilePath);
    }

    [Fact]
    public async Task UpsertAsync_WithException_ReturnsFalse() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Throws<Exception>();

        // Act
        var result = await _repository.UpsertAsync<TestData>(_owner, _path, _id1, GenerateTestData());

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Delete_PathAndIdGiven_RemovesDataFile() {
        // Arrange
        var activeFilePath = $"{_baseFolder}/{_owner}/{_path}/+{_id1}_20220406123456.json";
        var inactiveFilePath = $"{_baseFolder}/{_owner}/{_path}/{_id1}_20220405120000.json";
        var deletedFilePath1 = $"{_baseFolder}/{_owner}/{_path}/-{_id1}_20220406123456.json";
        var deletedFilePath2 = $"{_baseFolder}/{_owner}/{_path}/-{_id1}_20220405120000.json";

        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { activeFilePath });
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { inactiveFilePath });

        _io.ExtractFileNameFrom(activeFilePath).Returns($"+{_id1}_20220406123456.json");
        _io.CombinePath($"{_baseFolder}/{_owner}/{_path}", $"-{_id1}_20220406123456.json").Returns(deletedFilePath1);
        _io.When(x => x.MoveFile(activeFilePath, deletedFilePath1)).Do(_ => { });

        _io.ExtractFileNameFrom(inactiveFilePath).Returns($"{_id1}_20220406120000.json");
        _io.CombinePath($"{_baseFolder}/{_owner}/{_path}", $"-{_id1}_20220406120000.json").Returns(deletedFilePath2);
        _io.When(x => x.MoveFile(inactiveFilePath, deletedFilePath2)).Do(_ => { });

        // Act
        _repository.Delete(_owner, _path, _id1);

        // Assert
        _io.Received(1).MoveFile(activeFilePath, deletedFilePath1);
        _io.Received(1).MoveFile(inactiveFilePath, deletedFilePath2);
    }

    [Fact]
    public void Delete_WithException_ReturnsFalse() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Throws<Exception>();

        // Act
        var result = _repository.Delete(_owner, _path, _id1);

        // Assert
        result.Should().BeFalse();
    }

    private TestData GenerateTestData()
        => new() {
            Name = "SomeName",
            Number = 42,
        };

    private record TestData {
        public required string Name { get; init; }
        public required int Number { get; init; }
    }
}