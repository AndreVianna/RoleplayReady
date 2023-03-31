namespace RoleplayReady.Domain.Utilities;

public interface IFluentBuilder {
    ILet Let(string attribute);
    ICheck CheckIf(string attribute);
    IConnectBuilders AddJournalEntry(EntrySection section, string title, string text);
    IConnectBuilders AddJournalEntry(EntrySection section, string text);
    IConnectBuilders AddTag(string tag);
    IConnectBuilders AddPowerSource(string name, string description, Action<IFluentBuilder> build);

}