using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Appalachia.Utility.Encryption
{
    [DebuggerStepThrough]
    public static class AppaCipher
    {
        #region Constants and Static Readonly

        private const CipherMode KEY_CIPHER_MODE = CipherMode.CBC;

        private const int BLOCK_SIZE = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int ITERATIONS = 10000;
        private const int KEY_BYTES = KEY_SIZE / 8;

        // This constant is used to determine the keysize of the encryption algorithm in bits.
        private const int KEY_SIZE = 256;
        private const PaddingMode KEY_PADDING_MODE = PaddingMode.PKCS7;

        #endregion

        /// <summary>
        ///     Method to perform a very simple (and classical) encryption for a string. This is NOT at
        ///     all secure, it is only intended to make the string value non-obvious at a first glance.
        ///     The shiftOrUnshift argument is an arbitrary "key value", and must be a non-zero integer
        ///     between -65535 and 65535 (inclusive). To decrypt the encrypted string you use the negative
        ///     value. For example, if you encrypt with -42, then you decrypt with +42, or vice-versa.
        ///     This is inspired by, and largely based on, this:
        ///     https://stackoverflow.com/a/13026595/253938
        /// </summary>
        /// <param name="inputString">string to be encrypted or decrypted, must not be null</param>
        /// <param name="shift">see above</param>
        /// <param name="unshift">see above</param>
        /// <returns>encrypted or decrypted string</returns>
        public static string Caesar(string inputString, ushort shift, bool unshift = false)
        {
            const int C64K = ushort.MaxValue + 1;
            
            // Check the arguments
            if (inputString == null)
            {
                throw new ArgumentException("Must not be null.", nameof(inputString));
            }

            if (shift == 0)
            {
                throw new ArgumentException("Must not be zero.", nameof(shift));
            }

            var shiftValue = unshift ? -shift : shift;
            
            // Perform the Caesar cipher shifting, using modulo operator to provide wrap-around
            var charArray = new char[inputString.Length];
            
            for (var i = 0; i < inputString.Length; i++)
            {
                var inputCharacter = inputString[i];
                var characterCode = Convert.ToInt32(inputCharacter);
                
                charArray[i] = Convert.ToChar((characterCode + shiftValue + C64K) % C64K);
            }

            // Return the result as a new string
            return new string(charArray);
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherBytes = Convert.FromBase64String(cipherText);

            var contentLength = cipherBytes.Length - (2 * KEY_BYTES);

            var saltStringBytes = new byte[KEY_BYTES];
            var ivStringBytes = new byte[KEY_BYTES];
            var cipherTextBytes = new byte[contentLength];

            Array.ConstrainedCopy(cipherBytes, 0,             saltStringBytes, 0, KEY_BYTES);
            Array.ConstrainedCopy(cipherBytes, KEY_BYTES,     ivStringBytes,   0, KEY_BYTES);
            Array.ConstrainedCopy(cipherBytes, 2 * KEY_BYTES, cipherTextBytes, 0, contentLength);
            
            Array.Reverse(saltStringBytes);
            Array.Reverse(ivStringBytes);
            
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, ITERATIONS))
            {
                var keyBytes = password.GetBytes(KEY_BYTES);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = BLOCK_SIZE;
                    symmetricKey.Mode = KEY_CIPHER_MODE;
                    symmetricKey.Padding = KEY_PADDING_MODE;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(
                                memoryStream,
                                decryptor,
                                CryptoStreamMode.Read
                            ))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(
                                    plainTextBytes,
                                    0,
                                    plainTextBytes.Length
                                );
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy(KEY_BYTES);
            var ivStringBytes = Generate256BitsOfRandomEntropy(KEY_BYTES);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, ITERATIONS))
            {
                var passwordBytes = password.GetBytes(KEY_BYTES);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = BLOCK_SIZE;
                    symmetricKey.Mode = KEY_CIPHER_MODE;
                    symmetricKey.Padding = KEY_PADDING_MODE;
                    using (var encryptor = symmetricKey.CreateEncryptor(passwordBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(
                                memoryStream,
                                encryptor,
                                CryptoStreamMode.Write
                            ))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();

                                var encryptedBytes = memoryStream.ToArray();

                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.

                                Array.Reverse(saltStringBytes);
                                Array.Reverse(ivStringBytes);
                                
                                var cipherBytes = saltStringBytes.Concat(ivStringBytes)
                                                                 .Concat(encryptedBytes)
                                                                 .ToArray();

                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherBytes);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy(int sizeBytes)
        {
            var randomBytes = new byte[sizeBytes]; // 32 Bytes will give us 256 bits.
            using var rngCsp = new RNGCryptoServiceProvider();

            // Fill the array with cryptographically secure random bytes.
            rngCsp.GetBytes(randomBytes);

            return randomBytes;
        }
    }
}
