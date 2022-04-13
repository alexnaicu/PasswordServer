using System;

using Xunit;

using PasswordServer.Api.Services;

namespace PasswordServer.Api.UnitTests
{
    public class DefaultPasswordGeneratorTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(13)]
        [InlineData(128)]
        public void Generate_Returns_PasswordWithExpectedLength(int passwordLength)
        {            
            var sut = new DefaultPasswordGenerator();            

            var password = sut.Generate(passwordLength);

            Assert.NotNull(password);
            Assert.Equal(passwordLength, password.Length);
        }


        [Theory]
        [InlineData(1, 1)]
        [InlineData(13, 2)]
        [InlineData(128, 12)]
        public void Generate_Returns_PasswordWithExpectedMinimumOfSpecialCharacters(int passwordLength, int specialCharsMinimum)
        {
            var sut = new DefaultPasswordGenerator();

            var password = sut.Generate(passwordLength, specialCharsMinimum);
            var specialCharactersNumber = GetSpecialCharactersIn(password);

            Assert.NotNull(password);            
            Assert.Equal(passwordLength, password.Length);
            Assert.InRange(specialCharactersNumber, specialCharsMinimum, passwordLength);
        }

        private int GetSpecialCharactersIn(string password)
        {
            int specialCharactersNumber = 0;
            for (int i = 0; i < password.Length; i++)
            {
                if (Array.IndexOf(DefaultPasswordGenerator.SpecialChars, password[i]) > 0)
                    specialCharactersNumber++;
            }

            return specialCharactersNumber;
        }
    }
}
