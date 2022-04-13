using System;
using System.Security.Cryptography;

namespace PasswordServer.Api.Services
{
    public class DefaultPasswordGenerator : IPasswordGenerator
    {
        private const int MinPasswordLength = 1;
        private const int MaxPasswordLength = 128;
        public static readonly char[] SpecialChars = "!@#$€%^&*()_-+=[{}];:>|./?".ToCharArray(); //.. "!@#$%^&*?_-"

        public string Generate(int length, int specialCharsMinimum = 0)
        {
            if (length < MinPasswordLength || length > MaxPasswordLength)
                throw new ArgumentException(nameof(length));

            if (specialCharsMinimum > length || specialCharsMinimum < 0)
                throw new ArgumentException(nameof(specialCharsMinimum));

            using (var rng = RandomNumberGenerator.Create())
            {
                var byteBuffer = new byte[length];

                rng.GetBytes(byteBuffer);

                var count = 0;
                var characterBuffer = new char[length];

                for (var iter = 0; iter < length; iter++)
                {
                    var i = byteBuffer[iter] % 87;

                    if (i < 10)
                        characterBuffer[iter] = (char)('0' + i);
                    else if (i < 36)
                        characterBuffer[iter] = (char)('A' + i - 10);
                    else if (i < 62)
                        characterBuffer[iter] = (char)('a' + i - 36);
                    else
                    {
                        characterBuffer[iter] = SpecialChars[i - 62];
                        count++;
                    }
                }

                if (count >= specialCharsMinimum)
                    return new string(characterBuffer);

                var rand = new Random();
                for (int j = 0; j < specialCharsMinimum - count; j++)
                {
                    int k;
                    do
                    {
                        k = rand.Next(0, length);
                    }
                    while (!char.IsLetterOrDigit(characterBuffer[k]));

                    characterBuffer[k] = SpecialChars[rand.Next(0, SpecialChars.Length)];
                }

                return new string(characterBuffer);
            }
        }
    }
}
