namespace Management.Infrastructure
{
    public interface IAesProtector
    {
        string Encrypt(string text);
        string Encrypt(string text, byte[] key, byte[] iv);
        byte[] Encrypt(byte[] toEncryptArray);
        byte[] Encrypt(byte[] toEncryptArray, byte[] key, byte[] iv);
        byte[] Decrypt(byte[] srcBytes, byte[] key, byte[] iv);
        string Decrypt(string text, byte[] key, byte[] iv);
        byte[] Decrypt(byte[] srcBytes);
        string Decrypt(string text);
    }
}
