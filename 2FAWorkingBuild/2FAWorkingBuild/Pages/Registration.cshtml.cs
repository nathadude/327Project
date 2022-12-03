using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Google.Authenticator;
using System.Security.Cryptography;

namespace _2FAWorkingBuild.Pages
{
    //code behind - working build before db implementation
    public class RegistrationModel : PageModel
    {
        public string ReturnUrl { get; set; }
        public string ManualCode { get; set; }
        public string Email { get; set; }


        public void OnGet()
        {
            string key = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode("Test Two Factor", "user@example.com", key, false, 3);

            ReturnUrl = setupInfo.QrCodeSetupImageUrl;
            ManualCode = setupInfo.ManualEntryKey;
        }

        //public void OnPost()
        //{
        //    string key = GenRandString(10);

        //    TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        //    SetupCode setupInfo = tfa.GenerateSetupCode("Test Two Factor", "user@example.com", key, false, 3);

        //    ReturnUrl = setupInfo.QrCodeSetupImageUrl;
        //    ManualCode = setupInfo.ManualEntryKey;
        //}

        //public static string GenRandString(int length, string allowedChars = null)
        //{
        //    if (string.IsNullOrEmpty(allowedChars))
        //    {
        //        allowedChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //    }

        //    // gen rand bytes
        //    var rand = new byte[length];
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        rng.GetBytes(rand);
        //    }

        //    // Output string
        //    var allowed = allowedChars.ToCharArray();
        //    var l = allowed.Length;
        //    var chars = new char[length];
        //    for (var i = 0; i < length; i++)
        //        chars[i] = allowed[rand[i] % l];

        //    return new string(chars);
        //}
    }
}
