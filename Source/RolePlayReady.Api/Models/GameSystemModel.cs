namespace RolePlayReady.Api.Models;

[SwaggerSchema("The model that represents a game system.", ReadOnly = true)]
public record GameSystemModel {
    [Required]
    [SwaggerSchema("The id of the game system.", ReadOnly = true)]
    public required Guid Id { get; init; }
    [Required]
    [MaxLength(Constants.Constants.Validation.Definition.MaximumNameLength)]
    [MinLength(Constants.Constants.Validation.Definition.MinimumNameLength)]
    [SwaggerSchema("The name of the game system.", ReadOnly = true)]
    public required string Name { get; init; }
    [Required]
    [MaxLength(Constants.Constants.Validation.Definition.MaximumDescriptionLength)]
    [MinLength(Constants.Constants.Validation.Definition.MinimumDescriptionLength)]
    [SwaggerSchema("The description of the game system.", ReadOnly = true)]
    public required string Description { get; init; }
    [MinLength(Constants.Constants.Validation.Definition.MinimumShortNameLength)]
    [MaxLength(Constants.Constants.Validation.Definition.MaximumShortNameLength)]
    [SwaggerSchema("The optional short name of the game system.", ReadOnly = true)]
    public string? ShortName { get; init; }
    [SwaggerSchema("A collection of tags used to qualify the game system.", ReadOnly = true)]
    public ICollection<string> Tags { get; init; } = Array.Empty<string>();
}