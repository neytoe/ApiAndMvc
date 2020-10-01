using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using TheApi.DTO;
using TheApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(ILogger<AccountController> logger, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
          //  _config = config;
            _userManager = userManager;
        }


        // register user
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserToRegisterDto model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var userToAdd = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);

            if (userToAdd != null)
                return BadRequest("Email already exist");

            var user = new ApplicationUser
            {
                UserName = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
                Gender= model.Gender,
                DateCreated = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
                return BadRequest(ModelState);
            }

            await _userManager.AddToRoleAsync(user, "Customer");

            return Ok("User added!");

        }

        // login user
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserToLoginDto model)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            var user = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);

            if (user == null)
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password,isPersistent: model.RememberMe, false);

            if (!result.Succeeded)
                return Unauthorized("Invalid credentials");

            if (result.Succeeded)
            {
                var role = await _userManager.GetRolesAsync(user);

                var userReturn = new UserToReturnDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    DateCreated = user.DateCreated,
                    Role = role.ToList(),
                    Gender = user.Gender,
                   
                };
                return Ok(userReturn);
            }
            return BadRequest();

        }



    }
}
