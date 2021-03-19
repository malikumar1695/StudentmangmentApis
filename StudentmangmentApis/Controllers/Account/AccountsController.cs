using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudentMangment.Common.Data;
using StudentMangment.Common.Data.Models;
using StudentMangment.Common.DTOs;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StudentmangmentApis.Controllers.Account
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        public AccountsController(ApplicationDbContext context
            , UserManager<ApplicationUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IConfiguration configuration)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserAccountDTO userAccountDTO)
        {
            var userExist = await userManager.FindByNameAsync(userAccountDTO.UserName);
            if (userExist != null)
                return BadRequest("User is already exist");
            var AppUser = new ApplicationUser()
            {
                UserName = userAccountDTO.UserName,
                Email = userAccountDTO.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await userManager.CreateAsync(AppUser, userAccountDTO.Password);
            if (!result.Succeeded)
                return BadRequest("user creation failed"+string.Join("<br />",result.Errors.Select(x=> x.Description)));

            return Ok("User Successfully Created");
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserAccountDTO userAccountDTO)
        {
            var userExist = await userManager.FindByNameAsync(userAccountDTO.UserName);
            if (userExist != null && await userManager.CheckPasswordAsync(userExist, userAccountDTO.Password))
            {
                var userRoles = await userManager.GetRolesAsync(userExist);
                var authClaims = new List<Claim>{
                new Claim(ClaimTypes.Name , userExist.UserName),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };
                foreach (var role in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Key")));

                var token = new JwtSecurityToken(
                    issuer: configuration.GetValue<string>("JWT:ValidIssuer"),
                    audience: configuration.GetValue<string>("JWT:ValidAudience"),
                    expires: DateTime.Now.AddHours(5),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new UserAccountDTO()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserName = userAccountDTO.UserName,
                    Email = userAccountDTO.Email
                });
            }

            return Unauthorized();
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Get Request Successful");
        }
    }
}
