namespace System.Security.Cryptography;

public class Hasher : IHasher {
    private const int _keySize = 64;
    private const int _iterations = 350000;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    public HashedSecret HashSecret(string secret) {
        var secretBytes = Encoding.UTF8.GetBytes(Ensure.IsNotNullOrWhiteSpace(secret));
        var saltBytes = RandomNumberGenerator.GetBytes(_keySize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            secretBytes,
            saltBytes,
            _iterations,
            _hashAlgorithm,
            _keySize);

        return new HashedSecret(Convert.ToBase64String(hash), Convert.ToBase64String(saltBytes));
    }

    public bool VerifySecret(string secret, HashedSecret hashedSecret) {
        var secretBytes = Encoding.UTF8.GetBytes(Ensure.IsNotNullOrWhiteSpace(secret));
        var saltBytes = Convert.FromBase64String(hashedSecret.Salt);
        var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(secretBytes, saltBytes, _iterations, _hashAlgorithm, _keySize);
        var hashBytes = Convert.FromBase64String(hashedSecret.Hash);
        return hashToCompare.SequenceEqual(hashBytes);
    }
}
