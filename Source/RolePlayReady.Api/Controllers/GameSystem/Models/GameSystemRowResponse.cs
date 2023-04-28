namespace RolePlayReady.Api.Controllers.GameSystem.Models;

[SwaggerSchema("The model that identifies a game system in a list.", ReadOnly = true)]
public record GameSystemRowResponse {
    [Required]
    [SwaggerSchema("The id of the game system.", ReadOnly = true)]
    public required string Id { get; init; }
    [Required]
    [MaxLength(Validation.Name.MaximumLength)]
    [MinLength(Validation.Name.MinimumLength)]
    [SwaggerSchema("The name of the game system.", ReadOnly = true)]
    public required string Name { get; init; }
}