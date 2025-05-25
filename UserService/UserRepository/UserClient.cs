using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Application.Contracts;
using Microsoft.AspNetCore.Http;

namespace UserRepository
{
    public class UserClient : IHttpClient
    {
        private readonly HttpClient _client;

        public UserClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<HttpResponseMessage> DeleteProducts(string token)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return await _client.DeleteAsync("http://productservice:8081/api/products");
        }
    }
}
