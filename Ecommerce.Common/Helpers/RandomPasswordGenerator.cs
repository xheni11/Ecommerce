using System;
using System.Security.Cryptography;
using System.Text;

namespace Ecommerce.Common
{
    public class RandomPasswordGenerator
    {
        public static string GenerateRandomPassword(int length, bool lowerAlpha, bool upperAlpha, bool numeric,
            bool special)
        {
            const string lowerCaseCharacters = "abcdefghijklmnopqrstuvwxyz";
            const string upperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberCharacters = "1234567890";
            const string specialCharacters = "%+'!#$^?:.(){%+]~";

            var randomPassword = new StringBuilder();

            using (var randomPasswordProvider = new RNGCryptoServiceProvider(lowerCaseCharacters))
            {
                while (length > 0)
                {
                    var rCharacter = new byte[4];

                    int rPosition;
                    if (lowerAlpha)
                    {
                        randomPasswordProvider.GetBytes(rCharacter);
                        rPosition = Math.Abs(BitConverter.ToInt32(rCharacter, 0));
                        randomPassword.Append(lowerCaseCharacters[rPosition % lowerCaseCharacters.Length]);
                        length--;
                    }


                    if (upperAlpha)
                    {
                        randomPasswordProvider.GetBytes(rCharacter);
                        rPosition = Math.Abs(BitConverter.ToInt32(rCharacter, 0));
                        randomPassword.Append(upperCaseCharacters[rPosition % upperCaseCharacters.Length]);
                        length--;
                    }

                    if (numeric)
                    {
                        randomPasswordProvider.GetBytes(rCharacter);
                        rPosition = Math.Abs(BitConverter.ToInt32(rCharacter, 0));
                        randomPassword.Append(numberCharacters[rPosition % numberCharacters.Length]);
                        length--;
                    }

                    if (!special)
                    {
                        continue;
                    }

                    randomPasswordProvider.GetBytes(rCharacter);
                    rPosition = Math.Abs(BitConverter.ToInt32(rCharacter, 0));
                    randomPassword.Append(specialCharacters[rPosition % specialCharacters.Length]);
                    length--;
                }
            }

            return randomPassword.ToString();
        }

    }
}
