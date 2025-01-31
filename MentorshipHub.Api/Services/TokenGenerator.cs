using MentorshipHub.Api.DTOHelpers;
using MentorshipHub.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace MentorshipHub.Api.Services
{
    public class TokenGenerator
    {
        private readonly UserManager<AppUser> userManager;
        private readonly Jwt _jwt;

        public TokenGenerator(UserManager<AppUser> userManager, IOptions<Jwt> jwt)
        {
            this.userManager = userManager;
            this._jwt = jwt.Value;
        }

        public async Task<string> CreateToken(AppUser user)
        {

            var userClaims = await userManager.GetClaimsAsync(user);
            var roles = await userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
             new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
             new Claim(JwtRegisteredClaimNames.Email, user.Email!)
            };

            claims.AddRange(roles.Select(role => new Claim("role", role)));
            claims.AddRange(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwt.DurationInDay),
                SigningCredentials = creds,
                Issuer = _jwt.Issuer,
                Audience = _jwt.Audience
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);

        }

        public RefreshToken GeneratorRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.UtcNow.AddDays(10),
                Created = DateTime.UtcNow
            };

        }
    }
}
