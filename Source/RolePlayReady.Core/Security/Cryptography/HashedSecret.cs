namespace System.Security.Cryptography;

public sealed record HashedSecret {
    public HashedSecret(string hash, string salt) {
        Hash = Ensure.IsNotNullOrWhiteSpace(hash);
        Salt = Ensure.IsNotNullOrWhiteSpace(salt);
        HashBytes = Convert.FromBase64String(Hash);
        SaltBytes = Convert.FromBase64String(Salt);
    }

    public HashedSecret(byte[] hashBytes, byte[] saltBytes) {
        HashBytes = Ensure.IsNotNullOrEmpty(hashBytes);
        SaltBytes = Ensure.IsNotNullOrEmpty(saltBytes);
        Hash = Convert.ToBase64String(HashBytes);
        Salt = Convert.ToBase64String(SaltBytes);
    }

    public string Hash { get; }
    public string Salt { get; }

    public byte[] HashBytes { get; }
    public byte[] SaltBytes { get; }

    public bool Verify(string secret, IHasher hasher) {
        var hashedSecret = hasher.HashSecret(secret, SaltBytes);
        return hashedSecret.Hash == Hash;
    }
}