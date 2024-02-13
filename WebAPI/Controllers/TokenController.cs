using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data;

namespace WebAPI.Controllers
{
    public class TokenController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TokenController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Create(string username, string password, string grant_type)
        {
            if (await IsValidUser(username, password))
            {
                return new ObjectResult(await GenerateToken(username)); //ObjectResult returns object instead of Pages (like we used to in MVC)
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<bool> IsValidUser(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);

            return await _userManager.CheckPasswordAsync(user, password);
        }

        private async Task<dynamic> GenerateToken (string username)
        {
            var user = await _userManager.FindByEmailAsync(username);

            var userRoles = from userRolesT in _context.UserRoles
                            join rolesT in _context.Roles on userRolesT.RoleId equals rolesT.Id
                            where userRolesT.UserId == user.Id
                            select new { userRolesT.UserId, userRolesT.RoleId, rolesT.Name };

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),  //Nbf - not Before (token isnt valid before certain time)
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())   //Exp - exporation (when does token expire)
            };

            foreach(var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey")), SecurityAlgorithms.HmacSha256)), //Signing (not ecrypting) with HmacSha256 algo ("SecretKey is coded into Bytes) and uses those bytes as key to sign token
                new JwtPayload(claims));

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = username
            };

            return output;
        }
    }
}
