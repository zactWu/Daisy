using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaisyDBProject
{
    public interface IAuthenticateService
    {
        bool IsAuthenticated(LoginRequestDTO request, out string token);
    }
}
