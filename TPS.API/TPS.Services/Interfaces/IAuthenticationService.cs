using System;
using System.Collections.Generic;
using System.Text;
using TPS.Infrastructure.DTO;

namespace TPS.Services.Interfaces
{
    public interface IAuthenticationService
    {
        DTOAuthenticationResponse Login(DTOAuthentication data);
        DTOTokenResponse VerifyToken(DTOTokenVerification data);
    }
}
