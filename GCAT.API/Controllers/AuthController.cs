using GCAT.API.Contexts;
using GCAT.API.Entities;
using GCAT.API.Filters;
using GCAT.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GCAT.API.Controllers
{
    public class AuthController : Controller
    {
        private CryptoContext _context;
        private UserManager<CryptoUser> _userMgr;
        private IPasswordHasher<CryptoUser> _hasher;
        private ConfigSettings _configSettings;
        private RoleManager<IdentityRole> _roleMgr;

        public AuthController(CryptoContext context,
          SignInManager<CryptoUser> signInMgr,
          UserManager<CryptoUser> userMgr,
          IPasswordHasher<CryptoUser> hasher,
          ConfigSettings configSettings, RoleManager<IdentityRole> roleMgr)
        {
            _context = context;
            _userMgr = userMgr;
            _hasher = hasher;
            _configSettings = configSettings;
            _roleMgr = roleMgr;
        }

        [ValidateModelAttribute]
        [HttpPost("api/auth/token")]
        public async Task<IActionResult> CreateToken([FromBody] CredentialModel model)
        {
            try
            {
                var user = await _userMgr.FindByNameAsync(model.UserName);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var userClaims = await _userMgr.GetClaimsAsync(user);

                        var claims = new[]
                        {
          new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
          new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
          new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
          new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
          new Claim(JwtRegisteredClaimNames.Email, user.Email)
        }.Union(userClaims);

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configSettings.Configuration["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                          issuer: _configSettings.Configuration["Tokens:Issuer"],
                          audience: _configSettings.Configuration["Tokens:Audience"],
                          claims: claims,
                          expires: DateTime.UtcNow.AddMinutes(15),
                          signingCredentials: creds
                          );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return BadRequest("Failed to generate token");
        }
    }
}