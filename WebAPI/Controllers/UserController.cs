using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataManager.Library.DataAccess;
using DataManager.Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUserData _userData;
        private readonly ILogger<UserController> _logger;

        public UserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IUserData userData, ILogger<UserController> logger)
        {
            _context = context;
            _userManager = userManager;
            _userData = userData;
            _logger = logger;
        }

        [HttpGet]
        public UserModel GetById()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //RequestContext.Principal.Identity.GetUserId(); - From .Net Framework

            return _userData.GetUserById(userId).First();

        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllUsers")]
        public List<ApplicationUserModel> GetAllUsers()
        {
            List<ApplicationUserModel> output = new List<ApplicationUserModel>();

                var users = _context.Users.ToList();
                var userRoles = from userRolesT in _context.UserRoles
                                join rolesT in _context.Roles on userRolesT.RoleId equals rolesT.Id
                                select new { userRolesT.UserId, userRolesT.RoleId, rolesT.Name };

                //var roles = _context.Roles.ToList();

                foreach (var user in users)
                {
                    ApplicationUserModel applicationUserModel = new ApplicationUserModel
                    {
                        Id = user.Id,
                        Email = user.Email,
                    };

                applicationUserModel.Roles = userRoles.Where(x => x.UserId == applicationUserModel.Id)
                                                      .ToDictionary(key => key.RoleId, val => val.Name);
                    //foreach (var role in user.Roles)
                    //{
                    //    applicationUserModel.Roles.Add(role.RoleId, roles.Where(x => x.Id == role.RoleId).First().Name);
                    //}

                    output.Add(applicationUserModel);
                }

            return output;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("Admin/GetAllRoles")]
        public Dictionary<string, string> GetAllRoles()
        {
                var roles = _context.Roles.ToDictionary(x => x.Id, x => x.Name);

                return roles;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/AddRoleToUser")]
        public async Task AddRoleToUser(UserRolePairModel userRolePair)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);   //Getting this info for logging

            var user = await _userManager.FindByIdAsync(userRolePair.UserId);

            _logger.LogInformation("Admin {Admin} added user {User} to role {Role}", loggedInUserId, user.Id, userRolePair.RoleName);

           await _userManager.AddToRoleAsync(user, userRolePair.RoleName);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("Admin/RemoveRoleFromUser")]
        public async Task RemoveRoleFromUser(UserRolePairModel userRolePair)
        {
            string loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);   //Getting this info for logging

            var user = await _userManager.FindByIdAsync(userRolePair.UserId);

            _logger.LogInformation("Admin {Admin} removed user {User} from role {Role}", loggedInUserId, user.Id, userRolePair.RoleName);

            await _userManager.RemoveFromRoleAsync(user, userRolePair.RoleName);
        }
    }
}
