using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
namespace SfLogger
{
    public static class EncryptionHelper
    {
        public static byte[] Encrypt(string data)
        {
            byte[] plaintext = Encoding.UTF8.GetBytes(data);
            byte[] ciphertext = ProtectedData.Protect(plaintext, null, DataProtectionScope.CurrentUser);
            return ciphertext;
        }

        public static string Decrypt(byte[] ciphertext)
        {
            byte[] plaintext = ProtectedData.Unprotect(ciphertext, null, DataProtectionScope.CurrentUser);
            return Encoding.UTF8.GetString(plaintext);
        }

    }
}
