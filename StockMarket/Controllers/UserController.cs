using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockMarket.Data;
using StockMarket.Data.Entities;
using StockMarket.Models;
using StockMarket.Models.BindingModel;
using StockMarket.Models.DTO;
using StockMarket.Models.Enums;
using System.Data.Entity.Infrastructure;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DbUpdateConcurrencyException = Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException;

namespace StockMarket.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTConfig _jwTConfig;
        private readonly AppDbContext _context;

        public UserController(ILogger<UserController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IOptions<JWTConfig> jwtConfig, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwTConfig = jwtConfig.Value;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpPost]
        [Route("RegisterUser")]
        public async Task<object> RegisterUser([FromBody] AddUpdateRegisterUserBindingModel model)
        {
            try
            {
                if (model.Roles == null)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Roles Are Missing", null));
                }
                foreach (var role in model.Roles)
                {
                    if (!await _roleManager.RoleExistsAsync(role))
                    {
                        return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Role doesnt exist", null));
                    }
                }
                var user = new AppUser() { FullName = model.FullName, Email = model.Email, UserName = model.Email, DateCreated = DateTime.Now.ToUniversalTime(), DateModified = DateTime.Now.ToUniversalTime() };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var tempUser = await _userManager.FindByEmailAsync(model.Email);
                    foreach (var role in model.Roles)
                    {
                        await _userManager.AddToRoleAsync(tempUser, role);
                    }

                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "User has been registered", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "", result.Errors.Select(x => x.Description).ToArray()));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        [HttpPost("EditFullName")]
        public async Task<object> EditFullName([FromBody] UpdateFullNameBindingModel model)
        {
            var currentUser = await _userManager.FindByEmailAsync(model.Email);
            currentUser.FullName = model.FullName;
            await _userManager.UpdateAsync(currentUser);

            return await Task.FromResult(new ResponseModel(ResponseCode.OK, "FullName edited successfully!", null));

        }


        //[Authorize(Roles = "User, Admin")]
        [HttpGet("GetCurrentUser/{email}")]
        public async Task<object> GetCurrentUser(String email)
        {
            try
            {
                UserDTO userFinal;
                var currentUser = await _userManager.FindByEmailAsync(email);
                var roles = (await _userManager.GetRolesAsync(currentUser)).ToList();

                userFinal = new UserDTO(currentUser.FullName, currentUser.Email, currentUser.UserName, currentUser.DateCreated, roles);

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", userFinal));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }


        }

        //[Authorize(Roles = "ADMINISTRATOR")]
        [HttpGet("GetAllUsers")]
        public async Task<object> GetAllUsers()
        {
            try
            {
                List<UserDTO> alluserDTO = new List<UserDTO>();
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();
                    alluserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, roles));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", alluserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        [HttpGet("GetUserByFullNameLike/{fullName}")]
        public async Task<object> GetUserByFullNameLike(string fullName)
        {
            try
            {
                List<UserDTO> alluserDTO = new List<UserDTO>();
                //var users = _userManager.Users.ToList();
                var users = _userManager.Users.Where(x => x.FullName.ToUpper().Contains(fullName.ToUpper())).ToList();
                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();
                    alluserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, roles));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", alluserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        [Authorize(Roles = "User, Admin")]
        [HttpGet("GetUserList")]
        public async Task<object> GetUserList()
        {
            try
            {
                List<UserDTO> alluserDTO = new List<UserDTO>();
                //DTO es utilizado para enviar solo unos campos, no todos los de la base de datos
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user)).ToList();
                    if (roles.Any(x => x == "User"))
                    {
                        alluserDTO.Add(new UserDTO(user.FullName, user.Email, user.UserName, user.DateCreated, roles));
                    }

                }
                //return await Task.FromResult(users);
                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", alluserDTO));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }


        [HttpGet("GetRoles")]
        public async Task<object> GetRoles()
        {
            try
            {
                var roles = _roleManager.Roles.Select(x => x.Name).ToList();

                return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", roles));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        [HttpPost("Login")]
        public async Task<object> Login([FromBody] LoginBindingModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        var appUser = await _userManager.FindByEmailAsync(model.Email);
                        var roles = (await _userManager.GetRolesAsync(appUser)).ToList();
                        var user = new UserDTO(appUser.FullName, appUser.Email, appUser.UserName, appUser.DateCreated, roles);
                        //return await Task.FromResult("Login successfully");
                        user.Token = GenerateToken(appUser, roles);
                        return await Task.FromResult(new ResponseModel(ResponseCode.OK, "", user));
                    }
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Invalid username or password", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        //[Authorize (Roles = "Admin")]
        [HttpPost("AddRole")]
        public async Task<Object> AddRole([FromBody] AddRoleBindingModel model)
        {
            try
            {
                if (model == null || model.Role == "")
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Parameters are missing", null));
                }
                if (await _roleManager.RoleExistsAsync(model.Role))
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Role already exist", null));
                }
                var role = new IdentityRole();
                role.Name = model.Role;
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return await Task.FromResult(new ResponseModel(ResponseCode.OK, "Role added succesfully", null));
                }
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, "Something went wrong, try again", null));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new ResponseModel(ResponseCode.ERROR, ex.Message, null));
            }
        }

        [HttpDelete("DeleteUserByEmail/{email}")]
        public async Task<IActionResult> DeleteUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            await _userManager.DeleteAsync(user);
            return Ok();
        }

        private string GenerateToken(AppUser user, List<string> roles)
        {
            var claims = new List<System.Security.Claims.Claim>()
            {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.NameId, user.Id),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new System.Security.Claims.Claim(ClaimTypes.Role, role));
            }

            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwTConfig.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private bool AppUsersExists(string email)
        {
            return _context.AppUsers.Any(e => e.Email == email);
        }
    }

}
