using System;
using System.Text;

namespace CommonLayer.Models
{
    public class EncryptionDecryption
    {
        public static string Encryption(string password)
        {
            byte[] bytesToEncode = Encoding.UTF8.GetBytes(password);
            string encryptedPassword = Convert.ToBase64String(bytesToEncode);
            return encryptedPassword;
        }
    }
}
