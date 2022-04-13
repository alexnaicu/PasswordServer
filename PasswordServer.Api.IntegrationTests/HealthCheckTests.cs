using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;

using Xunit;

namespace PasswordServer.Api.IntegrationTests
{
    public class HealthCheckTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient httpClient;
        public HealthCheckTests(WebApplicationFactory<Startup> webAppFactory)
        {
            this.httpClient = webAppFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task HealthCheck_ReturnsOk()
        {
            var response = await httpClient.GetAsync("/healthcheck");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);            
        }
    }
}
