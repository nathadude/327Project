using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Cryptography;
using Google.Authenticator;
using _2FAImplementation.Models;

// Code behind
namespace _2FAImplementation.Pages
{
    public class RegistrationModel : PageModel
    {
        public string QrURL { get; set; }
        public string ManualCode { get; set; }
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public void OnPost([FromServices] AppDbContext db)
        {
            // TODO secure this
            string key = GenRandString(10);

            // TODO db
            // User {ID, Email, Key}
            db.Users.Add(new User
            {
                Id = Guid.NewGuid(),
                Email = Email,
                key = key
            });

            //Setup code and turn into
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            SetupCode setupInfo = tfa.GenerateSetupCode("Test Two Factor", "user@example.com", key, false, 3);

            QrURL = setupInfo.QrCodeSetupImageUrl;
            ManualCode = setupInfo.ManualEntryKey;
        }

        public static string GenRandString(int length, string allowedChars = null)
        {
            if (string.IsNullOrEmpty(allowedChars))
            {
                allowedChars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
                
            // gen rand bytes
            var rand = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(rand);
            }

            // Output string
            var allowed = allowedChars.ToCharArray();
            var l = allowed.Length;
            var chars = new char[length];
            for (var i = 0; i < length; i++)
                chars[i] = allowed[rand[i]%l];

            return new string(chars);
        }
    }
}
