namespace RolePlayReady.DataAccess.Repositories;

public class TrackedJsonFileRepositoryTests {
    private readonly IFileSystem _io;
    private readonly IDateTime _dateTime;
    private readonly TrackedJsonFileRepository<TestData> _repository;

    private record TestData : IKey {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Name { get; init; }
        public required int Number { get; init; }
    }

    private const string _baseFolder = "testBaseFolder";
    private const string _owner = "owner";
    private const string _path = "testPath";
    private static readonly Guid _id1 = Guid.NewGuid();
    private static readonly Guid _id2 = Guid.NewGuid();
    private static readonly Guid _newId = Guid.NewGuid();
    private readonly TestData[] _expected = {
        new() {
            Id = _id1,
            Name = "SomeName",
            Number = 42,
        },
        new() {
            Id = _id2,
            Name = "OtherName",
            Number = 7,
        }
    };

    public TrackedJsonFileRepositoryTests() {
        _io = Substitute.For<IFileSystem>();
        _dateTime = Substitute.For<IDateTime>();
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(TrackedJsonFileRepository<TestData>)}:BaseFolder"].Returns(_baseFolder);
        _repository = new(configuration, _io, _dateTime, NullLoggerFactory.Instance);
    }

    [Fact]
    public void Constructor_WithNulls_CreatesInstance() {
        // Arrange
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(TrackedJsonFileRepository<TestData>)}:BaseFolder"].Returns(_baseFolder);

        // Act
        var result = new TrackedJsonFileRepository<TestData>(configuration, null, null, null);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithConfigurationMissing_Throws() {
        // Arrange
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(TrackedJsonFileRepository<TestData>)}:BaseFolder"].Returns(default(string));

        // Act
        var action = () => new TrackedJsonFileRepository<TestData>(configuration, null, null, null);

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
        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[0])));
        _io.OpenFileForReading(filePaths[0]).Returns(file1Content);

        _io.ExtractFileNameFrom(filePaths[1]).Returns($"+{_id2}_20220406130000.json");
        _dateTime.TryParseExact("20220406130000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 13:00:00");
                return true;
            });
        using var file2Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[1])));
        _io.OpenFileForReading(filePaths[1]).Returns(file2Content);

        // Act
        var result = await _repository.GetAllAsync(_owner, _path);

        // Assert
        var subject = result.Should().BeOfType<TestData[]>().Subject;
        subject.Should().BeEquivalentTo(_expected);
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
        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[0])));
        _io.OpenFileForReading(filePaths[0]).Returns(file1Content);

        _io.ExtractFileNameFrom(filePaths[1]).Returns($"+{_id2}_20220406130000.json");
        _dateTime.TryParseExact("20220406130000", Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 13:00:00");
                return true;
            });
        using var file2Content = new MemoryStream("Failure"u8.ToArray());
        _io.OpenFileForReading(filePaths[1]).Returns(file2Content);

        // Act
        var result = await _repository.GetAllAsync(_owner, _path);

        // Assert
        var subject = result.Should().BeOfType<TestData[]>().Subject;
        subject.Should().BeEquivalentTo(_expected.Take(1));
    }

    [Fact]
    public async Task GetAllAsync_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.GetAllAsync(_owner, _path);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GetByIdAsync_PathAndIdGiven_DataFileFound_ReturnsDataFile() {
        // Arrange
        var filePath = $"{_baseFolder}/{_owner}/{_path}/+{_id1}_20220406123456.json";

        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { filePath });

        _io.ExtractFileNameFrom(filePath).Returns($"+{_id1}_20220406123456.json");
        _dateTime.TryParseExact("20220406123456", "yyyyMMddHHmmss", null, DateTimeStyles.None, out Arg.Any<DateTime>())
            .Returns(x => {
                x[4] = DateTime.Parse("2022-04-06 12:34:56");
                return true;
            });
        using var fileContent = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[0])));
        _io.OpenFileForReading(filePath).Returns(fileContent);

        // Act
        var result = await _repository.GetByIdAsync(_owner, _path, _id1);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_expected[0]);
    }

    [Fact]
    public async Task GetByIdAsync_PathAndIdGiven_DataFileNotFound_ReturnsNull() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(Array.Empty<string>());

        // Act
        var result = await _repository.GetByIdAsync(_owner, _path, _id1);

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
        var result = await _repository.GetByIdAsync(_owner, _path, _id1);

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
        var result = await _repository.GetByIdAsync(_owner, _path, _id1);

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
        using var fileContent = new MemoryStream("Invalid."u8.ToArray());
        _io.OpenFileForReading(filePath).Returns(fileContent);

        // Act
        var result = await _repository.GetByIdAsync(_owner, _path, _id1);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.GetByIdAsync(_owner, _path, _id1);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task UpsertAsync_PathAndIdGiven_InsertsNewDataFile() {
        // Arrange
        var existingFilePath = $"{_baseFolder}/{_owner}/{_path}/+{_id1}_20220406120000.json";
        var targetId = _newId;
        var filePath = $"{_baseFolder}/{_owner}/{_path}/+{targetId}_20220406230000.json";
        var newData = new TestData {
            Id = targetId,
            Name = "SomeNewName",
            Number = 69,
        };

        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { existingFilePath });
        _dateTime.Now.Returns(DateTime.Parse("2022-04-06 23:00:00"));
        _io.When(x => x.MoveFile(existingFilePath, existingFilePath.Replace("+", ""))).Do(_ => { });
        _io.CombinePath($"{_baseFolder}/{_owner}/{_path}", $"+{targetId}_20220406230000.json").Returns(filePath);
        var buffer = new byte[1024];
        using var memoryStream = new MemoryStream(buffer, true);
        _io.CreateNewFileAndOpenForWriting(filePath).Returns(memoryStream);

        _io.ExtractFileNameFrom(filePath).Returns($"+{targetId}_20220406230000.json");
        _dateTime.TryParseExact("20220406230000", "yyyyMMddHHmmss", null, DateTimeStyles.None, out Arg.Any<DateTime>())
                 .Returns(x => {
                      x[4] = DateTime.Parse("2022-04-06 23:00:00");
                      return true;
                  });
        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(newData)));
        _io.OpenFileForReading(filePath).Returns(file1Content);

        // Act
        var result = await _repository.InsertAsync(_owner, _path, newData);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(newData);
        _io.Received(1).CreateNewFileAndOpenForWriting(filePath);
    }

    [Fact]
    public async Task UpdateAsync_PathAndIdGiven_InsertsNewDataFile() {
        // Arrange
        var existingFilePath = $"{_baseFolder}/{_owner}/{_path}/+{_id1}_20220406120000.json";
        var targetId = _id1;
        var filePath = $"{_baseFolder}/{_owner}/{_path}/+{targetId}_20220406230000.json";
        var newData = new TestData {
            Id = targetId,
            Name = "SomeNewName",
            Number = 69,
        };

        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(new[] { existingFilePath });
        _dateTime.Now.Returns(DateTime.Parse("2022-04-06 23:00:00"));
        _io.When(x => x.MoveFile(existingFilePath, existingFilePath.Replace("+", ""))).Do(_ => { });
        _io.CombinePath($"{_baseFolder}/{_owner}/{_path}", $"+{targetId}_20220406230000.json").Returns(filePath);
        var buffer = new byte[1024];
        using var memoryStream = new MemoryStream(buffer, true);
        _io.CreateNewFileAndOpenForWriting(filePath).Returns(memoryStream);

        _io.ExtractFileNameFrom(filePath).Returns($"+{targetId}_20220406230000.json");
        _dateTime.TryParseExact("20220406230000", "yyyyMMddHHmmss", null, DateTimeStyles.None, out Arg.Any<DateTime>())
                 .Returns(x => {
                     x[4] = DateTime.Parse("2022-04-06 23:00:00");
                     return true;
                 });
        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(newData)));
        _io.OpenFileForReading(filePath).Returns(file1Content);

        // Act
        var result = await _repository.UpdateAsync(_owner, _path, newData);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(newData);
        _io.Received(1).MoveFile(existingFilePath, existingFilePath.Replace("+", ""));
        _io.Received(1).CreateNewFileAndOpenForWriting(filePath);
    }

    [Fact]
    public async Task InsertAsync_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        var data = _expected[0];
        _io.CombinePath(_baseFolder, _owner, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.InsertAsync(_owner, _path, data);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task UpdateAsync_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        var data = _expected[0];
        _io.CombinePath(_baseFolder, _owner, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.UpdateAsync(_owner, _path, data);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
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
        var result = _repository.Delete(_owner, _path, _id1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _io.Received(1).MoveFile(activeFilePath, deletedFilePath1);
        _io.Received(1).MoveFile(inactiveFilePath, deletedFilePath2);
    }

    [Fact]
    public void Delete_WhenFileNotFound_ReturnsFalse() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Returns($"{_baseFolder}/{_owner}/{_path}");
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"+{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(Array.Empty<string>());
        _io.GetFilesFrom($"{_baseFolder}/{_owner}/{_path}", $"{_id1}*.json", SearchOption.TopDirectoryOnly)
            .Returns(Array.Empty<string>());

        // Act
        var result = _repository.Delete(_owner, _path, _id1);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Delete_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        _io.CombinePath(_baseFolder, _owner, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.Delete(_owner, _path, _id1);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }
}