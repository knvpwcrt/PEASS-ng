﻿using System.Collections.Generic;
using System.Text;

namespace winPEAS.Helpers.CredentialManager
{
    internal static class CredentialManager
    {
        // thanks to 
        // https://github.com/spolnik/Simple.CredentialsManager

        public static string UnicodeInfoText = "(Unicode Base64 encoded)";

        internal static List<string> GetCredentials()
        {
            var result = new List<string>();
            var credentials = Credential.LoadAll();

            foreach (var credential in credentials)
            {
                var isUnicode = MyUtils.IsUnicode(credential.Password);

                string clearTextPassword = credential.Password;
                string unicodeInfo = string.Empty;
                if (isUnicode)
                {
                    clearTextPassword = System.Convert.ToBase64String(Encoding.Unicode.GetBytes(credential.Password));
                    unicodeInfo = UnicodeInfoText;
                }

                string item = $"\nUsername: {credential.Username}\n" +
                              $"Password:{unicodeInfo} {clearTextPassword}\n" +
                              $"Target: {credential.Target}\n" +
                              $"PersistenceType: {credential.PersistenceType}\n" +
                              $"LastWriteTime: {credential.LastWriteTime}\n" +
                              "-------------------------------------------------------";

                result.Add(item);
            }

            return result;
        }
    }
}
