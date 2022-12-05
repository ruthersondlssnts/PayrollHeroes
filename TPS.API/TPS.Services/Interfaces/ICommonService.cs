using System;
using System.Collections.Generic;
using System.Text;

namespace TPS.Services.Interfaces
{
    public interface ICommonService
    {
        string EncryptString(string plainText);
        string DecryptStirng(string cipherText);
        string ForgotPasswordEncrypt(string plainText);
        string ForgotPasswordDecrypt(string cipherText);
    }
}
