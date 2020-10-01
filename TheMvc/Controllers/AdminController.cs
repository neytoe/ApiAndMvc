using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TheApi.DTO;
using TheMvc.ViewModel;

namespace TheMvc.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IConfiguration _config;
        private readonly string apiBaseUrl;

        public AdminController(ILogger<AdminController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
          

        }


        public async Task<ActionResult> DashBoardAsync(UserToReturnDto model)
        {

            HttpClient client = new HttpClient();
            
            //  string endpoint = apiBaseUrl + "api/account/register";
            var Response = await client.GetAsync("http://localhost:5000/api/admin/getusers");
                
            var responseBody = await Response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<IEnumerable<UserToReturnDto>>(responseBody);

            ViewBag.female = responseObject.Where(x => x.Gender.ToLower() == "female").Count();
            ViewBag.male = responseObject.Where(x => x.Gender.ToLower() == "male").Count();

            var res = new UserList
            {
                AllUsers = responseObject,
                User = model
            };

            var user = responseObject.ToList();

            ViewBag.IsLoggedIn = true;
            return View(res);

                

            

        }

    }
}
