using System;
using System.Security.Cryptography;
using System.Text;

namespace Inha.Commons.Security
{
    public class SecurityUtil
    {
        /// <summary>
        ///     Encrypt string with DES Algorithm
        /// </summary>
        /// <param name="toEncrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string toEncrypt,
                                     string key)
        {
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(key + toEncrypt);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key)),
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        ///     Decrypt string with DES Algorithm
        /// </summary>
        /// <param name="toDecrypt"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string toDecrypt,
                                     string key)
        {
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider
            {
                Key = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key))
            };
            ;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            var result = UTF8Encoding.UTF8.GetString(resultArray);

            return result.Substring(key.Length);
        }
    }
}
