using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ICreateTokenService
    {
        Task<string> CreateTokenAsync(string UserName);
    }
}
