namespace RoleplayReady.Domain.Utilities;

public class ElementBuilder {
    private readonly IElement _element;
    private readonly string _section;

    private ElementBuilder(IElement element, string section) {
        _element = element;
        _section = section;
    }

    public static ElementBuilder For(IElement element, string section)
        => new ElementBuilder(element, section);

    public IElement As(Action<ISectionBuilderMainCommands> configure) {
        configure(SectionBuilder.For(_element, _section));
        return _element;
    }
}