using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Saver
{
    public interface ICrypto
    {
        string EncryptJson(object data);
        T DecryptJson<T>(string encryptedData);
    }
    
    public class AesEncryption : ICrypto
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public AesEncryption(string key, string iv)
        {
            _key = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            _iv = Encoding.UTF8.GetBytes(iv.PadRight(16).Substring(0, 16));
        }

        public string EncryptJson(object data)
        {
            string json = JsonConvert.SerializeObject(data);
            return Encrypt(json);
        }

        public T DecryptJson<T>(string encryptedData)
        {
            string json = Decrypt(encryptedData);
            return JsonConvert.DeserializeObject<T>(json);
        }

        private string Encrypt(string plainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private string Decrypt(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = _key;
                aes.IV = _iv;

                using (MemoryStream ms = new MemoryStream(buffer))
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}