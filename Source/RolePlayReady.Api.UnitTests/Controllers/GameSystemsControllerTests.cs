using RolePlayReady.Api.Controllers.GameSystems;
using RolePlayReady.Api.Controllers.GameSystems.Models;

namespace RolePlayReady.Api.Controllers;

public class GameSystemsControllerTests {
    [Fact]
    public async Task GetMany_ReturnsOkResult_WithArrayOfGameSystemRowModels() {
        // Arrange
        var rows = new[] {
            new Row {
                Id = Guid.NewGuid(),
                Name = "Lairs & Lizards 3e",
            },
            new Row {
                Id = Guid.NewGuid(),
                Name = "Road Scout",
            }
        };

        var expectedRows = rows.ToResponse();

        var handler = Substitute.For<IGameSystemHandler>();
        handler.GetManyAsync(Arg.Any<CancellationToken>())
               .Returns(Result.FromValue(rows.AsEnumerable()));

        var logger = Substitute.For<ILogger<GameSystemsController>>();

        var controller = new GameSystemsController(handler, logger);

        // Act
        var result = await controller.GetMany();

        // Assert
        var subject = result.Should().BeOfType<OkObjectResult>().Subject;
        var resultRows = subject.Value.Should().BeOfType<GameSystemRowResponse[]>().Subject;
        resultRows.Should().BeEquivalentTo(expectedRows);
    }
}
