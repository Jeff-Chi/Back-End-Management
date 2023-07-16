using System.Security.Cryptography;

namespace Management.Infrastructure
{
    public static class HashSign
    {
        //public const string MD5 = "MD5";
        public const string SHA1 = "SHA1";
        public const string SHA256 = "SHA256";
        public const string SHA384 = "SHA384";
        public const string SHA512 = "SHA512";
        public const string HMACSHA256 = "HMACSHA256";

        private static readonly HashSet<string> SupportedSignTypes = new HashSet<string> {SHA1, SHA256, SHA384, SHA512, HMACSHA256 };

        public static bool IsSupported(string signType)
        {
            return SupportedSignTypes.Contains(signType.ToUpper());
        }

        public static byte[] Sign(string signType, byte[] data)
        {
            signType = signType.ToUpper();
            //if (!IsSupported(signType))
            //{
            //    throw new Exception("the sign type is not supported");
            //}
            if (signType.ToUpper().StartsWith("HMAC"))
            {
                using (HashAlgorithm algorithm = new HMACSHA256())
                {
                    return algorithm.ComputeHash(data);
                }
            }
            else
            {
                using (HashAlgorithm algorithm = MD5.Create())
                {
                    return algorithm.ComputeHash(data);
                }
            }
        }
    }
}
