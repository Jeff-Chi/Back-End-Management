using System.Security.Cryptography;
using System.Text;

namespace Management.Infrastructure
{
    public class AesProtector: IAesProtector
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;
        //public AesProtector(byte[] key, byte[] iv)
        //{
        //    _key = key;
        //    _iv = iv;
        //}

        public AesProtector(string keySalt)
        {
            string keyStr = keySalt.PadLeft(32, '0').Right(32);
            _key = keyStr.ToCharArray().Select(p => (byte)p).ToArray();
            string ivStr = keyStr.Right(16);
            _iv = ivStr.ToCharArray().Select(p => (byte)p).ToArray();
        }

        //public AesProtector()
        //{
        //    _key = KeyGenerator.Random(32);
        //    _iv = KeyGenerator.Random(16);
        //}

        public string Encrypt(string text)
        {
            return Encrypt(text, _key, _iv);
        }

        public string Encrypt(string text, byte[] key, byte[] iv)
        {
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(text);
            byte[] resultArray = Encrypt(toEncryptArray, key, iv);
            return Convert.ToBase64String(resultArray);
        }

        public byte[] Encrypt(byte[] toEncryptArray)
        {
            return Encrypt(toEncryptArray, _key, _iv);
        }

        public byte[] Encrypt(byte[] toEncryptArray, byte[] key, byte[] iv)
        {
            RijndaelManaged rm = new RijndaelManaged
            {
                Key = key,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7,
                IV = iv,
            };
            ICryptoTransform cTransform = rm.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return resultArray;
        }

        public byte[] Decrypt(byte[] srcBytes, byte[] key, byte[] iv)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = key;
                aes.IV = iv;
                var enc = aes.CreateDecryptor(aes.Key, aes.IV);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, enc, CryptoStreamMode.Write))
                    {
                        cs.Write(srcBytes, 0, srcBytes.Length);
                    }
                    return ms.ToArray();
                }
            }
        }

        public string Decrypt(string text, byte[] key, byte[] iv)
        {
            byte[] srcBytes = Convert.FromBase64String(text);
            byte[] result = Decrypt(srcBytes, key, iv);
            return Encoding.UTF8.GetString(result);
        }

        public byte[] Decrypt(byte[] srcBytes)
        {
            return Decrypt(srcBytes, _key, _iv);
        }

        public string Decrypt(string text)
        {
            return Decrypt(text, _key, _iv);
        }
    }
}
