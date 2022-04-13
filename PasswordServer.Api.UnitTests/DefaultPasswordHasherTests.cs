using System;

using Xunit;

using PasswordServer.Api.Services;

namespace PasswordServer.Api.UnitTests
{
    public class DefaultPasswordHasherTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("random")]
        [InlineData("                 ")]
        public void HashPassword_WithPasswordNotNull_ReturnsNotNullAndDifferent(string password)
        {
            var sut = new DefaultPasswordHasher();

            var hashedPassword = sut.HashPassword(password);

            Assert.NotNull(hashedPassword);
            Assert.NotEqual(hashedPassword, password);
        }


        [Theory]
        [InlineData("")]
        [InlineData("random")]
        [InlineData("                 ")]
        public void VerifyHashedPassword_WithPasswordAndCorrespondingHashedPassword_ReturnsTrue(string password)
        {
            var sut = new DefaultPasswordHasher();

            var hashedPassword = sut.HashPassword(password);

            Assert.True(sut.VerifyHashedPassword(hashedPassword, password));
        }

        [Theory]
        [InlineData("")]
        [InlineData("random")]
        [InlineData("                 ")]
        public void VerifyHashedPassword_WithPasswordAndWrongHashedPassword_ReturnsFalse(string password)
        {
            var sut = new DefaultPasswordHasher();

            var hashedPassword = sut.HashPassword("wrong");            

            Assert.False(sut.VerifyHashedPassword(hashedPassword, password));
        }
    }
}
