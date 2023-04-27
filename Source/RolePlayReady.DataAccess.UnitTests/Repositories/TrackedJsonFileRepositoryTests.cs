using RolePlayReady.Security.Abstractions;

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

    private const string _rootFolder = "testBaseFolder";
    private const string _owner = "owner";
    private const string _baseFolder = $"{_rootFolder}/{_owner}";
    private const string _path = "testPath";
    private const string _finalFolder = $"{_baseFolder}/{_path}";

    private static readonly Guid _file1Id = Guid.NewGuid();
    private static readonly Guid _file2Id = Guid.NewGuid();
    private static readonly Guid _missingFileId = Guid.NewGuid();
    private static readonly Guid _invalidFileNameId = Guid.NewGuid();
    private static readonly Guid _invalidTimestampId = Guid.NewGuid();
    private static readonly Guid _newFileId = Guid.NewGuid();

    private const string _timestamp1 = "20220406120000";
    private static readonly DateTime _dateTime1 = DateTime.Parse("2022-04-06 12:00:00");
    private const string _timestamp2 = "20220406130000";
    private static readonly DateTime _dateTime2 = DateTime.Parse("2022-04-06 13:00:00");
    private const string _timestamp3 = "invalid";
    private const string _timestamp4 = "99999999999999";
    private const string _timestamp5 = "20220407230000";
    private const string _timestamp6 = "20220405120000";
    private static readonly DateTime _dateTime5 = DateTime.Parse("2022-04-07 23:00:00");

    private static readonly string[] _existingFiles = new[] {
        $"{_finalFolder}/+{_file1Id}_{_timestamp1}.json",
        $"{_finalFolder}/+{_file2Id}_{_timestamp2}.json",
        $"{_finalFolder}/{_file1Id}_{_timestamp6}.json",
        $"{_finalFolder}/-{_file1Id}_20220404123456.json",
        $"{_finalFolder}/-{_file1Id}_20220404120000.json",
    };

    private static readonly string _invalidFileName = $"{_finalFolder}/+{_invalidFileNameId}_{_timestamp3}.json";
    private static readonly string _invalidTimestamp = $"{_finalFolder}/+{_invalidTimestampId}_{_timestamp4}.json";
    private static readonly string _newFile = $"{_finalFolder}/+{_newFileId}_{_timestamp5}.json";
    private static readonly string _updatedFile = $"{_finalFolder}/+{_file1Id}_{_timestamp5}.json";
    private static readonly string _deletedFile1 = $"{_finalFolder}/-{_file1Id}_{_timestamp1}.json";
    private static readonly string _deletedFile2 = $"{_finalFolder}/-{_file1Id}_{_timestamp6}.json";

    private readonly TestData[] _expected = {
        new() {
            Id = _file1Id,
            Name = "SomeName",
            Number = 42,
        },
        new() {
            Id = _file2Id,
            Name = "OtherName",
            Number = 7,
        }
    };

    public TrackedJsonFileRepositoryTests() {
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.Username.Returns(_owner);
        _io = Substitute.For<IFileSystem>();
        _io.CombinePath(_rootFolder, _owner).Returns(_baseFolder);
        _io.CombinePath(_baseFolder, _path).Returns(_finalFolder);
        _io.CombinePath($"{_finalFolder}", $"+{_newFileId}_{_timestamp5}.json").Returns(_newFile);
        _io.CombinePath($"{_finalFolder}", $"-{_file1Id}_{_timestamp1}.json").Returns(_deletedFile1);
        _io.CombinePath($"{_finalFolder}", $"+{_file1Id}_{_timestamp5}.json").Returns(_updatedFile);
        _io.CombinePath($"{_finalFolder}", $"-{_file1Id}_{_timestamp6}.json").Returns(_deletedFile2);

        _io.GetFilesFrom(_finalFolder, "+*.json", SearchOption.TopDirectoryOnly).Returns(_existingFiles);
        _io.GetFilesFrom(_finalFolder, $"+{_file1Id}*.json", SearchOption.TopDirectoryOnly)
           .Returns(new[] { _existingFiles[0] });
        _io.GetFilesFrom(_finalFolder, $"{_file1Id}*.json", SearchOption.TopDirectoryOnly)
           .Returns(new[] { _existingFiles[2] });
        _io.GetFilesFrom(_finalFolder, $"+{_file2Id}*.json", SearchOption.TopDirectoryOnly)
           .Returns(new[] { _existingFiles[1] });
        _io.GetFilesFrom(_finalFolder, $"+{_missingFileId}*.json", SearchOption.TopDirectoryOnly)
           .Returns(Array.Empty<string>());
        _io.GetFilesFrom(_finalFolder, $"+{_invalidFileNameId}*.json", SearchOption.TopDirectoryOnly)
           .Returns(new[] { _invalidFileName });

        _io.ExtractFileNameFrom(_existingFiles[0]).Returns($"+{_file1Id}_{_timestamp1}.json");
        _io.ExtractFileNameFrom(_existingFiles[1]).Returns($"+{_file2Id}_{_timestamp1}.json");
        _io.ExtractFileNameFrom(_invalidFileName).Returns($"+{_invalidFileNameId}_invalid.json");
        _io.ExtractFileNameFrom(_invalidTimestamp).Returns($"+{_invalidTimestampId}_{_timestamp4}.json");
        _io.ExtractFileNameFrom(_newFile).Returns($"+{_newFileId}_{_timestamp5}.json");
        _io.ExtractFileNameFrom(_updatedFile).Returns($"+{_file1Id}_{_timestamp5}.json");
        _io.ExtractFileNameFrom(_existingFiles[2]).Returns($"{_file1Id}_{_timestamp6}.json");

        _io.When(x => x.MoveFile(Arg.Any<string>(), Arg.Any<string>())).Do(_ => { });

        _dateTime = Substitute.For<IDateTime>();
        _dateTime.Now.Returns(_dateTime5);
        _dateTime.TryParseExact(_timestamp1, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
                 .Returns(x => { x[4] = _dateTime1; return true; });
        _dateTime.TryParseExact(_timestamp2, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
                 .Returns(x => { x[4] = _dateTime2; return true; });
        _dateTime.TryParseExact(_timestamp4, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
                 .Returns(x => { x[4] = null; return false; });
        _dateTime.TryParseExact(_timestamp5, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
                 .Returns(x => { x[4] = _dateTime5; return true; });

        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(TrackedJsonFileRepository<TestData>)}:BaseFolder"].Returns(_rootFolder);
        _repository = new(configuration, userAccessor, _io, _dateTime, NullLoggerFactory.Instance);
    }

    [Fact]
    public void Constructor_WithNulls_CreatesInstance() {
        // Arrange
        var userAccessor = Substitute.For<IUserAccessor>();
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(TrackedJsonFileRepository<TestData>)}:BaseFolder"].Returns(_baseFolder);

        // Act
        var result = new TrackedJsonFileRepository<TestData>(configuration, userAccessor, null, null, null);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_WithConfigurationMissing_Throws() {
        // Arrange
        var userAccessor = Substitute.For<IUserAccessor>();
        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(TrackedJsonFileRepository<TestData>)}:BaseFolder"].Returns(default(string));

        // Act
        var action = () => new TrackedJsonFileRepository<TestData>(configuration, userAccessor, null, null, null);

        // Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public async Task GetAllAsync_PathGiven_ReturnsAllDataFiles() {
        // Arrange
        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[0])));
        _io.OpenFileForReading(_existingFiles[0]).Returns(file1Content);
        using var file2Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[1])));
        _io.OpenFileForReading(_existingFiles[1]).Returns(file2Content);

        // Act
        var result = await _repository.GetAllAsync(_path);

        // Assert
        var subject = result.Should().BeOfType<TestData[]>().Subject;
        subject.Should().BeEquivalentTo(_expected);
    }

    [Fact]
    public async Task GetAllAsync_WithInvalidFile_ReturnsOnlyValidOnes() {
        // Arrange
        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[0])));
        _io.OpenFileForReading(_existingFiles[0]).Returns(file1Content);
        using var file2Content = new MemoryStream("Failure"u8.ToArray());
        _io.OpenFileForReading(_existingFiles[1]).Returns(file2Content);

        // Act
        var result = await _repository.GetAllAsync(_path);

        // Assert
        var subject = result.Should().BeOfType<TestData[]>().Subject;
        subject.Should().BeEquivalentTo(_expected.Take(1));
    }

    [Fact]
    public async Task GetAllAsync_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        _io.CombinePath(_baseFolder, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.GetAllAsync(_path);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task GetByIdAsync_PathAndIdGiven_DataFileFound_ReturnsDataFile() {
        // Arrange
        using var fileContent = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[0])));
        _io.OpenFileForReading(_existingFiles[0]).Returns(fileContent);

        // Act
        var result = await _repository.GetByIdAsync(_path, _file1Id);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(_expected[0]);
    }

    [Fact]
    public async Task GetByIdAsync_PathAndIdGiven_DataFileNotFound_ReturnsNull() {
        // Act
        var result = await _repository.GetByIdAsync(_path, _missingFileId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidFileName_ReturnsNull() {
        // Act
        var result = await _repository.GetByIdAsync(_path, _file1Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidFileTimestamp_ReturnsNull() {
        // Act
        var result = await _repository.GetByIdAsync(_path, _file1Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidFileContent_ReturnsNull() {
        // Arrange
        using var fileContent = new MemoryStream("Invalid."u8.ToArray());
        _io.OpenFileForReading(_existingFiles[0]).Returns(fileContent);

        // Act
        var result = await _repository.GetByIdAsync(_path, _file1Id);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        _io.CombinePath(_baseFolder, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.GetByIdAsync(_path, _file1Id);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task InsertAsync_PathAndIdGiven_InsertsNewDataFile() {
        // Arrange
        var newFileData = new TestData {
            Id = _newFileId,
            Name = "SomeNewName",
            Number = 69,
        };

        var buffer = new byte[1024];
        using var memoryStream = new MemoryStream(buffer, true);
        _io.CreateNewFileAndOpenForWriting(_newFile).Returns(memoryStream);

        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(newFileData)));
        _io.OpenFileForReading(_newFile).Returns(file1Content);

        // Act
        var result = await _repository.InsertAsync(_path, newFileData);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(newFileData);
        _io.Received(1).CreateNewFileAndOpenForWriting(_newFile);
    }

    [Fact]
    public async Task UpdateAsync_PathAndIdGiven_InsertsNewDataFile() {
        // Arrange
        var updatedData = new TestData {
            Id = _file1Id,
            Name = "SomeNewName",
            Number = 69,
        };

        var buffer = new byte[1024];
        using var memoryStream = new MemoryStream(buffer, true);
        _io.CreateNewFileAndOpenForWriting(_updatedFile).Returns(memoryStream);

        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(updatedData)));
        _io.OpenFileForReading(_updatedFile).Returns(file1Content);

        // Act
        var result = await _repository.UpdateAsync(_path, updatedData);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedData);
        _io.Received(1).MoveFile(_existingFiles[0], _existingFiles[0].Replace("+", ""));
        _io.Received(1).CreateNewFileAndOpenForWriting(_updatedFile);
    }

    [Fact]
    public async Task InsertAsync_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        var data = _expected[0];
        _io.CombinePath(_baseFolder, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.InsertAsync(_path, data);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task UpdateAsync_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        var data = _expected[0];
        _io.CombinePath(_baseFolder, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.UpdateAsync(_path, data);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public void Delete_PathAndIdGiven_RemovesDataFile() {
        // Act
        var result = _repository.Delete(_path, _file1Id);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _io.Received(1).MoveFile(_existingFiles[0], _deletedFile1);
        _io.Received(1).MoveFile(_existingFiles[2], _deletedFile2);
    }

    [Fact]
    public void Delete_WhenFileNotFound_ReturnsFalse() {
        // Act
        var result = _repository.Delete(_path, _missingFileId);

        // Assert
        result.IsSuccess.Should().BeFalse();
    }

    [Fact]
    public void Delete_WithInternalError_ThrowsInvalidOperationException() {
        // Arrange
        _io.CombinePath(_baseFolder, _path).Throws<InvalidOperationException>();

        // Act
        var action = () => _repository.Delete(_path, _file1Id);

        // Assert
        action.Should().Throw<InvalidOperationException>();
    }
}