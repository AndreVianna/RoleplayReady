namespace RolePlayReady.DataAccess.Repositories;

public sealed class TrackedJsonFileRepositoryTests : IDisposable {
    private readonly IFileSystem _io;
    private readonly TrackedJsonFileRepository<TestData> _repository;

    private record TestData : IKey {
        public Guid Id { get; init; } = Guid.NewGuid();
    }

    private const string _rootFolder = "testBaseFolder";
    private const string _owner = "owner";
    private const string _baseFolder = $"{_rootFolder}/{_owner}";
    private const string _path = "testPath";
    private const string _finalFolder = $"{_baseFolder}/{_path}";

    private static readonly Guid _newFileId = Guid.NewGuid();
    private static readonly Guid _file1Id = Guid.NewGuid();
    private static readonly Guid _file2Id = Guid.NewGuid();
    private static readonly Guid _missingFileId = Guid.NewGuid();
    private static readonly Guid _invalidNameId = Guid.NewGuid();
    private static readonly Guid _invalidTimestampId = Guid.NewGuid();
    private static readonly Guid _invalidContentId = Guid.NewGuid();

    private const string _dataTimeFormat = "yyyyMMddHHmmss";
    private static readonly DateTime _now = DateTime.Parse("2022-04-07 12:00:00");
    private static readonly DateTime _dateTime1 = _now.AddDays(-1);
    private static readonly DateTime _dateTime2 = _dateTime1.AddHours(1);
    private static readonly DateTime _dateTime3 = _dateTime1.AddDays(-1);
    private static readonly DateTime _dateTime4 = _dateTime1.AddDays(-2);
    private static readonly DateTime _dateTime5 = _dateTime1.AddDays(-3);
    private static readonly string _nowTimestamp = _now.ToString(_dataTimeFormat);
    private static readonly string _timestamp1 = _dateTime1.ToString(_dataTimeFormat);
    private static readonly string _timestamp2 = _dateTime2.ToString(_dataTimeFormat);
    private static readonly string _timestamp3 = _dateTime3.ToString(_dataTimeFormat);
    private static readonly string _timestamp4 = _dateTime4.ToString(_dataTimeFormat);
    private static readonly string _timestamp5 = _dateTime5.ToString(_dataTimeFormat);
    private const string _invalidTimestamp = "99999999999999";

    private static readonly string _newFileName = $"+{_newFileId}_{_nowTimestamp}.json";
    private static readonly string _file1V4Name = $"+{_file1Id}_{_nowTimestamp}.json";
    private static readonly string _file1V3Name = $"+{_file1Id}_{_timestamp1}.json";
    private static readonly string _file1V3DeletedName = $"{_file1Id}_{_timestamp1}.json";
    private static readonly string _file2V0Name = $"+{_file2Id}_{_timestamp2}.json";
    private static readonly string _invalidContentFileName = $"+{_invalidContentId}_{_timestamp5}.json";
    private static readonly string _invalidNameFileName = $"+{_invalidNameId}_invalid.json";
    private static readonly string _invalidTimestampFileName = $"+{_invalidTimestampId}_{_invalidTimestamp}.json";

    private static readonly string _newFile = $"{_finalFolder}/{_newFileName}";
    private static readonly string _file1V4 = $"{_finalFolder}/{_file1V4Name}";
    private static readonly string _file1V3 = $"{_finalFolder}/{_file1V3Name}";
    private static readonly string _file1V3Deleted = $"{_finalFolder}/{_file1V3DeletedName}";
    private static readonly string _file2V0 = $"{_finalFolder}/{_file2V0Name}";
    private static readonly string _invalidContentFile = $"{_finalFolder}/{_invalidContentFileName}";
    private static readonly string _invalidNameFile = $"{_finalFolder}/{_invalidNameFileName}";
    private static readonly string _invalidTimestampFile = $"{_finalFolder}/{_invalidTimestampFileName}";

    private static readonly string[] _existingFiles = new[] {
        _file1V3,
        _file2V0,
        _invalidContentFile,
        _invalidNameFile,
        _invalidTimestampFile,
    };
    private readonly MemoryStream _file1Content;
    private readonly MemoryStream _file2Content;
    private readonly MemoryStream _invalidFileContent;

    private readonly TestData[] _expected = {
        new() {
            Id = _file1Id,
        },
        new() {
            Id = _file2Id,
        }
    };

    public TrackedJsonFileRepositoryTests() {
        var userAccessor = Substitute.For<IUserAccessor>();
        userAccessor.Username.Returns(_owner);
        _io = Substitute.For<IFileSystem>();
        _io.CombinePath(_rootFolder, _owner).Returns(_baseFolder);
        _io.CombinePath(_baseFolder, _path).Returns(_finalFolder);
        _io.CombinePath(_finalFolder, _newFileName).Returns(_newFile);
        _io.CombinePath(_finalFolder, _file1V3DeletedName).Returns(_file1V3Deleted);
        _io.CombinePath(_finalFolder, _file1V4Name).Returns(_file1V4);

        _io.GetFilesFrom(_finalFolder, "+*.json", SearchOption.TopDirectoryOnly).Returns(_existingFiles);

        _io.GetFilesFrom(_finalFolder, $"+{_missingFileId}*.json", SearchOption.TopDirectoryOnly).Returns(Array.Empty<string>());
        _io.GetFilesFrom(_finalFolder, $"+{_file1Id}*.json", SearchOption.TopDirectoryOnly).Returns(new[] { _file1V3 });
        _io.GetFilesFrom(_finalFolder, $"+{_file2Id}*.json", SearchOption.TopDirectoryOnly).Returns(new[] { _file2V0 });
        _io.GetFilesFrom(_finalFolder, $"+{_invalidNameId}*.json", SearchOption.TopDirectoryOnly).Returns(new[] { _invalidNameFile });
        _io.GetFilesFrom(_finalFolder, $"+{_invalidTimestampId}*.json", SearchOption.TopDirectoryOnly).Returns(new[] { _invalidTimestampFile });
        _io.GetFilesFrom(_finalFolder, $"+{_invalidContentId}*.json", SearchOption.TopDirectoryOnly).Returns(new[] { _invalidContentFile });

        _file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[0])));
        _io.OpenFileForReading(_file1V3).Returns(_file1Content);
        _file2Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(_expected[1])));
        _io.OpenFileForReading(_file2V0).Returns(_file2Content);
        _invalidFileContent = new MemoryStream("Failure"u8.ToArray());
        _io.OpenFileForReading(_invalidContentFile).Returns(_invalidFileContent);

        _io.ExtractFileNameFrom(_newFile).Returns(_newFileName);
        _io.ExtractFileNameFrom(_file1V4).Returns(_file1V4Name);
        _io.ExtractFileNameFrom(_file1V3).Returns(_file1V3Name);
        _io.ExtractFileNameFrom(_file2V0).Returns(_file2V0Name);
        _io.ExtractFileNameFrom(_invalidNameFile).Returns(_invalidNameFileName);
        _io.ExtractFileNameFrom(_invalidTimestampFile).Returns(_invalidTimestampFileName);

        _io.When(x => x.MoveFile(Arg.Any<string>(), Arg.Any<string>())).Do(_ => { });

        var dateTime = Substitute.For<IDateTime>();
        dateTime.Now.Returns(_now);
        dateTime.TryParseExact(_nowTimestamp, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => { x[4] = _now; return true; });
        dateTime.TryParseExact(_timestamp1, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => { x[4] = _dateTime1; return true; });
        dateTime.TryParseExact(_timestamp2, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => { x[4] = _dateTime2; return true; });
        dateTime.TryParseExact(_timestamp3, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => { x[4] = _dateTime3; return true; });
        dateTime.TryParseExact(_timestamp4, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => { x[4] = _dateTime4; return true; });
        dateTime.TryParseExact(_timestamp5, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => { x[4] = _dateTime5; return true; });
        dateTime.TryParseExact(_invalidTimestamp, Arg.Any<string>(), Arg.Any<IFormatProvider>(), Arg.Any<DateTimeStyles>(), out Arg.Any<DateTime>())
            .Returns(x => { x[4] = null; return false; });

        var configuration = Substitute.For<IConfiguration>();
        configuration[$"{nameof(TrackedJsonFileRepository<TestData>)}:BaseFolder"].Returns(_rootFolder);
        _repository = new(configuration, userAccessor, _io, dateTime, NullLoggerFactory.Instance);
    }

    public void Dispose() {
        _file1Content.Dispose();
        _file2Content.Dispose();
        _invalidFileContent.Dispose();
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
        // Act
        var result = await _repository.GetAllAsync(_path);

        // Assert
        var subject = result.Should().BeOfType<TestData[]>().Subject;
        subject.Should().BeEquivalentTo(_expected);
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
        var result = await _repository.GetByIdAsync(_path, _invalidNameId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidFileTimestamp_ReturnsNull() {
        // Act
        var result = await _repository.GetByIdAsync(_path, _invalidTimestampId);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidFileContent_ReturnsNull() {
        // Act
        var result = await _repository.GetByIdAsync(_path, _invalidContentId);

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
    public async Task InsertAsync_WhenAlreadyExists_InsertsNewDataFile() {
        // Arrange
        var newFileData = new TestData {
            Id = _file2Id,
        };

        // Act
        var action = () => _repository.InsertAsync(_path, newFileData);

        // Assert
        await action.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task UpdateAsync_PathAndIdGiven_InsertsNewDataFile() {
        // Arrange
        var updatedData = new TestData {
            Id = _file1Id,
        };

        var buffer = new byte[1024];
        using var memoryStream = new MemoryStream(buffer, true);
        _io.CreateNewFileAndOpenForWriting(_file1V4).Returns(memoryStream);

        using var file1Content = new MemoryStream(Encoding.UTF8.GetBytes(Serialize(updatedData)));
        _io.OpenFileForReading(_file1V4).Returns(file1Content);

        // Act
        var result = await _repository.UpdateAsync(_path, updatedData);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(updatedData);
        _io.Received(1).MoveFile(_existingFiles[0], _existingFiles[0].Replace("+", ""));
        _io.Received(1).CreateNewFileAndOpenForWriting(_file1V4);
    }

    [Fact]
    public async Task InsertAsync_FileNotFound_InsertsNewDataFile() {
        // Arrange
        var newFileData = new TestData {
            Id = _missingFileId,
        };

        // Act
        var result = await _repository.UpdateAsync(_path, newFileData);

        // Assert
        result.Should().BeNull();
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
        result.Should().BeTrue();
        _io.Received(1).MoveFile(_file1V3, _file1V3Deleted);
    }

    [Fact]
    public void Delete_WhenFileNotFound_ReturnsFalse() {
        // Act
        var result = _repository.Delete(_path, _missingFileId);

        // Assert
        result.Should().BeFalse();
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