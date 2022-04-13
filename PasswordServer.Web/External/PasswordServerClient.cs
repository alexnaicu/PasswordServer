using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using PasswordServer.Web.External.Models;

namespace PasswordServer.Web.External
{
    public class PasswordServerClient : IPasswordServerClient
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<PasswordServerClient> logger;
        private const string PasswordsEndpoint = "api/passwords";

        public PasswordServerClient(HttpClient  httpClient, IConfiguration configuration, ILogger<PasswordServerClient> logger)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.httpClient.BaseAddress = new Uri(configuration.GetValue<string>("PasswordServer"));
        }

        public async Task<PasswordDtoResult> GetPassword(int userId)
        {            
            try
            {
                var serializedRequestObject = JsonSerializer.Serialize(new PasswordDtoCreate() { UserId = userId });

                var request = new HttpRequestMessage(HttpMethod.Post, PasswordsEndpoint);
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                request.Content = new StringContent(serializedRequestObject);
                request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await httpClient.SendAsync(request);               

                response.EnsureSuccessStatusCode();

                var passwordDtoResult = await response.Content.ReadFromJsonAsync<PasswordDtoResult>();

                return passwordDtoResult;
            }
            catch (HttpRequestException e)
            {
                logger.LogError(e, "Failed to get a password from the password server.");
            }

            return null;
        }
    }
}
