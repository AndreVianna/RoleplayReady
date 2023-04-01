namespace RoleplayReady.Domain.Utilities;

public class ElementBuilder {
    private readonly IElement _parent;
    private readonly string _section;

    private ElementBuilder(IElement parent, string section) {
        _parent = parent;
        _section = section;
    }

    public static ElementBuilder For(IElement element, string section) => new ElementBuilder(element, section);

    public IElement As(Action<ISectionBuilderMainCommands> configure) {
        configure(SectionBuilder.For(_parent, _section));
        return _parent;
    }
}