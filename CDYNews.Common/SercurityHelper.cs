using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CDYNews.Common
{
    public class SercurityHelper
    {
        
        public static string Decrypt(string src)
        {
            string hash = "";
            string result;
            byte[] data = Convert.FromBase64String(src);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform trans = trip.CreateDecryptor();
                    byte[] kq = trans.TransformFinalBlock(data, 0, data.Length);
                    result= UTF8Encoding.UTF8.GetString(kq);
                }
            }
            return result;
        }
    }
}
