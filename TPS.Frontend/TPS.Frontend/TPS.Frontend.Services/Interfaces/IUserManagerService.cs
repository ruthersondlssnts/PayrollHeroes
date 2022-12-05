using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TPS.Frontend.Infrastructure;
using TPS.Frontend.Infrastructure.DTO;
using TPS.Frontend.Infrastructure.Models;

namespace TPS.Frontend.Services.Interfaces
{
    public interface IUserManagerService
    {
        Task<DTOAuthenticationResponse> LoginAsync(CancellationToken cancellationToken, LoginModel model);
        Task<DTOTokenResponse> VerifyTokenAsync(CancellationToken cancellationToken, DTOTokenVerification model);
    }
}
