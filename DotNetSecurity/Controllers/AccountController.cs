using DotNetSecurity.DTO;
using DotNetSecurity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace DotNetSecurity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationSettings _appSettings;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, 
            IOptions<ApplicationSettings> appSettings, 
            SignInManager<ApplicationUser> signInManager
            , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(CreateUserDto model)
        {

            var applicationUser = new ApplicationUser()
            {
                UserName = model.Msisdn,
                Msisdn = model.Msisdn,
                Email = model.Msisdn+"@aaa.com"


            };

            try
            {
                var result = await _userManager.CreateAsync(applicationUser, model.Password);
                var aaa = await _userManager.AddToRoleAsync(applicationUser, "User");
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(CreateUserDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Msisdn);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var roles = await _userManager.GetRolesAsync(user);
               


                var claims = new List<Claim>();
                claims.Add(new Claim("Msisdn", user.Msisdn));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim("Roles", string.Join(",", roles)));
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return Ok(new { token = token, refr = GetRefreshToken() });


            }
            else return BadRequest(new { message = "Username or password is incorrect." });

        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Authorize(Roles  = "Admin")]
        [HttpPost("add-role")]              
        public async Task<IActionResult> AddRole(CreateRoleDto dto)
        {
            try
            {
                if( await _roleManager.RoleExistsAsync(dto.Role) == false)
                {
                    var role = new IdentityRole();
                    role.Name = dto.Role;

                    var data =  await _roleManager.CreateAsync(role);
                    return Ok(data);
                }
            }
            catch (Exception)
            {

                throw;
            }

            return Ok(1);
        }

        private string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
