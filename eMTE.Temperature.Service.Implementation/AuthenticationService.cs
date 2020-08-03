﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using eMTE.Common.Authentication;
using eMTE.Common.Repository.Contracts;
using eMTE.Temperature.Domain;
using eMTE.Temperature.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace eMTE.Temperature.Service.Implementation
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IAuthenticator _authenticator;
        public AuthenticationService(
            IAuthenticator authenticator,
            IRepository<User> userRepository)
        {
            _authenticator = authenticator;
            _userRepository = userRepository;
        }

        public async Task<string> Login(string email, string password, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Set.SingleAsync(user => user.Email == email, cancellationToken);
            if(user == null)
            {
                throw new AuthenticationException("User not found");
            }

            if(_authenticator.Verify(password, user.Hash))
            {
                throw new AuthenticationException("Incorrect Password");
            }

            return generateJwtToken(user.Id, user.OrganizationId);
        }

        private string generateJwtToken(Guid userId, Guid organizationId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("my temperature security key"); // this should be a long string due to limitation of SymmetricSecurityKey Defaults.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim("OrganizationId", organizationId.ToString())
            };
            var signInCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = signInCredentials
            };
            var token = new JwtSecurityToken(claims: claims, signingCredentials: signInCredentials);
            return tokenHandler.WriteToken(token);
        }

    }
}
