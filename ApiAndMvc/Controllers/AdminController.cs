using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheApi.Data;
using TheApi.DTO;
using TheApi.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TheApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminController> _logger;
        private readonly AppDbContext _context;

        public AdminController(UserManager<ApplicationUser> userManager, ILogger<AdminController> logger, AppDbContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
        }



        private IActionResult ShapeResultAsync(IQueryable<ApplicationUser> result)
        {
            var usersToReturn = new List<UserToReturnDto>();

            foreach (var item in result)
            {


                var user = new UserToReturnDto
                {

                    LastName = item.LastName,
                    FirstName = item.FirstName,
                    Email = item.Email,
                    Gender = item.Gender,
                    DateCreated = item.DateCreated,
                };


                usersToReturn.Add(user);
            }

            return Ok(usersToReturn);
        }

        // get all users
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsersAsync()
        {
            var result = _userManager.Users;
            if (result == null)
                return NotFound();

            return ShapeResultAsync(result);
        }


        //Get particular User
        [HttpGet("GetUser")]
        public async Task<IActionResult> GetActionResultAsync(UserToLoginDto model)
        {

            if (ModelState.IsValid)
            {
                var result = await _userManager.FindByEmailAsync(model.Email);

                if (result != null)
                {

                    var dto = new UserToReturnDto
                    {
                        FirstName = result.FirstName,
                        LastName = result.LastName,
                        Gender = result.Gender,
                        Email = result.Email,
                        DateCreated = result.DateCreated
                    };
                    return Ok(dto);
                }

            }

            return BadRequest("Incorrect User Input");
        }
    }
}
