namespace RolePlayReady.Models.Contracts;

public interface ITransferable<out TObject, in TOwner> where TObject : ITransferable<TObject, TOwner> {
    TObject TransferTo(TOwner newOwner);
}