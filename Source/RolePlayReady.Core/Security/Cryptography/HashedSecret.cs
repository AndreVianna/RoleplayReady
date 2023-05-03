namespace System.Security.Cryptography;

public record HashedSecret {
    public HashedSecret(string hash, string salt) {
        Hash = Ensure.IsNotNullOrWhiteSpace(hash);
        Salt = Ensure.IsNotNullOrWhiteSpace(salt);
    }

    public HashedSecret(byte[] hashBytes, byte[] saltBytes) {
        Hash = Convert.ToBase64String(Ensure.IsNotNullOrEmpty(hashBytes));
        Salt = Convert.ToBase64String(Ensure.IsNotNullOrEmpty(saltBytes));
    }

    public string Hash { get; init; }
    public string Salt { get; init; }

    public void Deconstruct(out string hash, out string salt) {
        hash = Hash;
        salt = Salt;
    }

    public void Deconstruct(out byte[] hashBytes, out byte[] saltBytes) {
        hashBytes = Convert.FromBase64String(Hash);
        saltBytes = Convert.FromBase64String(Salt);
    }
}