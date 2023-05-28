using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Management.Infrastructure
{
    public static class EncryptHelper
    {
        public static string MD5Encrypt(string input)
        {
            string result = string.Empty;
            //32位大写
            using (var md5 = MD5.Create())
            {
                var arr = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                var strResult = BitConverter.ToString(arr);
                result = strResult.Replace("-", "");
            }
            return result;
        }
    }
}
