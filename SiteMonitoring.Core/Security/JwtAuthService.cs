using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SiteMonitoring.Core.Entities.Models;

namespace SiteMonitoring.Core.Security
{
    public class JwtAuthService
    {
        public JwtSecurityToken GeToken(AuthOptions authOptions, User user)
        {
            var identity = GetIdentity(user);

            var now = DateTime.Now;

            return new JwtSecurityToken(
                    issuer: authOptions.Issuer,
                    audience: authOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(authOptions.SecurityKey, SecurityAlgorithms.HmacSha256)
                );
        }

        public string GetEncodedToken(AuthOptions authOptions, User user)
        {
            var token = GeToken(authOptions, user);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsIdentity GetIdentity(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
