using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using TPS.Infrastructure;
using TPS.Infrastructure.DTO;
using TPS.Infrastructure.Models;
using TPS.Services.Interfaces;

namespace TPS.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IDBService<Employee> _data;
        private readonly ConfigurationSettings _configuration;
        private readonly ICommonService _common;

        public AuthenticationService(IDBService<Employee> data, ConfigurationSettings configuration, ICommonService common)
        {
            _data = data;
            _configuration = configuration;
            _common = common;
        }

        public DTOAuthenticationResponse Login(DTOAuthentication data)
        {
            var returnData = new DTOAuthenticationResponse();
            data.Password = _common.EncryptString(data.Password);

            var user = _data.FindOne(x => x.Username == data.Username && x.Password == data.Password);
            if (user == null)
            {
                returnData.StatusCode = System.Net.HttpStatusCode.NotFound;
                returnData.Message = "Username/Password not found";
                return returnData;
            }

            returnData.UserId = user.Id;
            returnData.Username = user.Username;
            returnData.Roles = user.Roles;

            returnData.StatusCode = System.Net.HttpStatusCode.OK;
            returnData.Message = "Login success";

            //Token Generation
            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserName", user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Roles),
                   };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration.Issuer, _configuration.Audience, claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
            returnData.Token = new JwtSecurityTokenHandler().WriteToken(token);

            return returnData;
        }

        public DTOTokenResponse VerifyToken(DTOTokenVerification data)
        {
            DTOTokenResponse returnData = new DTOTokenResponse();

            try
            {
                SecurityToken validatedToken;
                IPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(data.Token, GetValidationParameters(), out validatedToken);
                returnData.StatusCode = System.Net.HttpStatusCode.OK;
                returnData.Message = "Token valid";

                //RENEW TOKEN
                //Token Generation
                var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("UserName", data.Username.ToString()),
                   };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(_configuration.Issuer, _configuration.Audience, claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);
                returnData.Token = new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch
            {
                returnData.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                returnData.Message = "Token invalid or expired";
                return returnData;
            }
            return returnData;
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = false, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = _configuration.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.Key)) // The same key as the one that generate the token
            };
        }
    }
}
