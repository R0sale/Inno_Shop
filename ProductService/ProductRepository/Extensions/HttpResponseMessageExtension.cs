using Entities.Exceptions;
using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions
{
    public static class HttpResponseMessageExtension
    {
        public async static Task EnsureSuccessOrThrow(this HttpResponseMessage response, Guid id)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadFromJsonAsync<UserDTO>();

                if (content is null)
                    throw new ArgumentNullException("Content is null");

                if (content.Id.Equals(Guid.Empty))
                    throw new UserNotFoundException(id);
            }
        }
    }
}
