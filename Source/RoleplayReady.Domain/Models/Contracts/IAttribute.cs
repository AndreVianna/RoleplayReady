namespace RolePlayReady.Models.Contracts;

public interface IAttribute : IIdentification, IMayHaveASource {
    IRuleSet RuleSet { get; set; }
    Type DataType => typeof(object);
    new string FullName => $"<{RuleSet.Abbreviation}:{Type}>{Name}({Abbreviation}):{DataType.Name}";
}

public interface IAttribute<TValue> : IAttribute {
    new Type DataType => typeof(TValue);
}
