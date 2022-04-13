using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

using PasswordServer.Api.Models;

namespace PasswordServer.Api.IntegrationTests
{
    public class PasswordControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;
        public PasswordControllerTests(WebApplicationFactory<Startup> webAppFactory)
        {
            this.httpClient = webAppFactory.CreateDefaultClient(new Uri("https://localhost/api/passwords"));
        }

        [Fact]
        public async Task Post_WithValidPasswordDtoCreateJson_ReturnsOkResult()
        {
            var jsonContent = JsonContent.Create(new PasswordDtoCreate() { UserId = 1 });

            var response = await httpClient.PostAsync("", jsonContent);
            var passwordDtoRead = await response.Content.ReadFromJsonAsync<PasswordDtoRead>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);            
        }

        [Fact]
        public async Task Post_WithValidPasswordDtoCreateJson_ReturnsExpectedContent()
        {
            var jsonContent = JsonContent.Create(new PasswordDtoCreate() { UserId = 1 });

            var response = await httpClient.PostAsync("", jsonContent);
            var passwordDtoRead = await response.Content.ReadFromJsonAsync<PasswordDtoRead>();
            
            Assert.NotNull(passwordDtoRead);
            Assert.Equal(1, passwordDtoRead.UserId);
            Assert.NotNull(passwordDtoRead.Password);
            Assert.InRange(passwordDtoRead.ValidTimeInSeconds, 1, 30);
        }
    }
}
