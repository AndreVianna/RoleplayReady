namespace RolePlayReady.DataAccess.Repositories;

public interface IDataMapper<TModel, out TRowModel, TData> {
    TData ToData(TModel input);
    TRowModel ToRow(TData input);
    TModel? ToModel(TData? input);
}