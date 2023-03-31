using System.Xml.Linq;

namespace RoleplayReady.Domain.Models;

public record Entry : IEntry {
    public Entry() {
        
    }

    [SetsRequiredMembers]
    public Entry(EntrySection section, string title, string text) {
        Section = section;
        Title = title;
        Text = text;
    }

    public required EntrySection Section { get; init; }
    public required string Title { get; init; }
    public required string Text { get; init; }

    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public bool IsDeleted { get; init; }
}