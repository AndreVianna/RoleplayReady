namespace RolePlayReady.Models.Contracts;

public interface IAttribute : IIdentification, ICloneable {
    IRuleSet RuleSet { get; set; }
    Type DataType => typeof(object);
    new string FullName => $"<{RuleSet.Abbreviation}:{Type}>{Name}({Abbreviation}):{DataType.Name}";
}

public interface IAttribute<TValue> : IAttribute {
    new string FullName => $"<{RuleSet.Abbreviation}:{Type}>{Name}({Abbreviation}):{DataType.Name}";
}
