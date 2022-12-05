using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class CommonService : ICommonService
    {
        private string passPhrase
        {
            get { return "a5e2f6d4!@"; }
        }

        private string initVector
        {
            get { return "tu89geji340t89u2"; }
        }

        private int keysize
        {
            get { return Convert.ToInt32("256"); }
        }

        public string EncryptString(string plainText)
        {
            var initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            var password = new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            var encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            var cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public string DecryptStirng(string cipherText)
        {
            var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            var cipherTextBytes = Convert.FromBase64String(cipherText);
            var password = new PasswordDeriveBytes(passPhrase, null);
            var keyBytes = password.GetBytes(keysize / 8);
            var symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
            var memoryStream = new MemoryStream(cipherTextBytes);
            var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        /// <summary>
        /// Author : Dharmraj Mangukiya
        /// Usage : To deal with Commas in string
        /// </summary>
        /// <param name="s"></param>
        /// <returns>string</returns>
        public static string Escape(string s)
        {
            string QUOTE = "\"";
            string ESCAPED_QUOTE = "\"\"";
            char[] CHARACTERS_THAT_MUST_BE_QUOTED = { ',', '"', '\n' };
            if (s.Contains(QUOTE))
                s = s.Replace(QUOTE, ESCAPED_QUOTE);

            if (s.IndexOfAny(CHARACTERS_THAT_MUST_BE_QUOTED) > -1)
                s = QUOTE + s + QUOTE;

            return s;
        }

        /// <summary>
        /// Generate Datatable
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="ColName"></param>
        /// <returns></returns>
        public static DataTable GenerateDataTable(string TableName, params string[] ColName)
        {
            DataTable NewTabel = new DataTable(TableName);
            for (int i = 0; i < ColName.Length; i++)
            {
                NewTabel.Columns.Add(new DataColumn().ColumnName = ColName[i]);
            }
            return NewTabel;
        }

        public string ForgotPasswordEncrypt(string plainText)
        {
            byte xorConstant = 0x53;
            var data = Encoding.UTF8.GetBytes(plainText);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            return Convert.ToBase64String(data);
        }

        public string ForgotPasswordDecrypt(string cipherText)
        {
            byte xorConstant = 0x53;
            var data = Convert.FromBase64String(cipherText);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)(data[i] ^ xorConstant);
            }
            return Encoding.UTF8.GetString(data);
        }

    }
}
