namespace System.Security.Cryptography;

public interface IHasher {
    HashedSecret HashSecret(string secret);
    bool VerifySecret(string secret, HashedSecret hashedSecret);
}