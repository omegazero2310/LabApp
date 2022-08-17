using System.Security.Cryptography;
using System.Text;

namespace WebApiLab.Exts
{
    public sealed class PasswordHelper
    {
        private static readonly int SaltSize = 16;
        public static byte[] GenerateSaltedHashPassword(byte[] plainText, byte[]? salt = null)
        {
            HashAlgorithm algorithm = SHA256.Create();
            if (salt == null)
                salt = GenerateSalt();
            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];
            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }
            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
        public static byte[] GenerateSalt()
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] buff = new byte[SaltSize];
            rng.GetBytes(buff);
            return buff;
        }
        public static bool ComparePassowrd(string input, string hashedPassword, string salt)
        {
            byte[] inputbytes = Encoding.ASCII.GetBytes(input);
            byte[] hashedPasswordbytes = Encoding.ASCII.GetBytes(hashedPassword);
            byte[] saltbytes = Encoding.ASCII.GetBytes(salt);
            //create byte[] from input + salt
            byte[] inputhasedbytes = GenerateSaltedHashPassword(inputbytes, saltbytes);
            //Compare password
            if (inputhasedbytes.Length != hashedPasswordbytes.Length)
            {
                return false;
            }
            for (int i = 0; i < inputhasedbytes.Length; i++)
            {
                if (inputhasedbytes[i] != hashedPasswordbytes[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
