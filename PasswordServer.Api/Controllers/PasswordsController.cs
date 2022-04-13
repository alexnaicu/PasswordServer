using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using PasswordServer.Api.Configurations;
using PasswordServer.Api.Models;
using PasswordServer.Api.Services;

namespace PasswordServer.Api.Controllers
{
    [Route("api/passwords")]
    [ApiController]
    public class PasswordsController : ControllerBase
    {
        private readonly PasswordConfiguration passwordConfiguration;
        private readonly IPasswordGenerator passwordGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IPasswordServerRepository passwordServerRepository;

        public PasswordsController(IOptions<PasswordConfiguration> passwordConfiguration, IPasswordGenerator passwordGenerator, IPasswordHasher passwordHasher, IPasswordServerRepository passwordServerRepository)
        {
            this.passwordConfiguration = passwordConfiguration.Value;
            this.passwordGenerator = passwordGenerator ?? throw new ArgumentNullException(nameof(passwordGenerator));
            this.passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            this.passwordServerRepository = passwordServerRepository ?? throw new ArgumentNullException(nameof(passwordServerRepository));
        }

        [HttpPost]
        public ActionResult<PasswordDtoRead> GeneratePassword(PasswordDtoCreate model)
        {
            var password = passwordGenerator.Generate(passwordConfiguration.Length, 0);
            var hashedPassword = passwordHasher.HashPassword(password);
                        
            var utcNow = DateTime.UtcNow;
            var passwordEntity = new Entities.PasswordData()
            {
                PasswordHash = hashedPassword,
                UserId = model.UserId,
                ValidFromUtc = utcNow,
                ValidUntilUtc = utcNow.AddSeconds(passwordConfiguration.ValidTimeInSeconds)
            };
            passwordServerRepository.AddPassword(passwordEntity);
            passwordServerRepository.Save();

            var passwordDtoRead = new PasswordDtoRead()
            {
                Password = password,
                UserId = model.UserId,
                ValidTimeInSeconds = passwordConfiguration.ValidTimeInSeconds,
                ValidUntilUtc = passwordEntity.ValidUntilUtc                
            };            

            return this.Ok(passwordDtoRead);
        }
    }
}
