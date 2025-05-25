using Repository.Extensions;
using Entities.Exceptions;
using System.Net.Http;
using System.Net.Http.Json;
using Application.Dtos;
using Application.Contracts;

namespace Repository
{
    public class ProductClient : IHttpClient
    {
        private readonly HttpClient _client;

        public ProductClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<UserDTO> GetUser(Guid id)
        {
            var response = await _client.GetAsync($"http://userservice:8080/api/users/{id}");

            var user = await response.Content.ReadFromJsonAsync<UserDTO>();

            return user;
        }
    }
}
