using System;
using System.Collections.Generic;
using System.Diagnostics;
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

using TheMvc.Models;
using TheMvc.ViewModel;

namespace TheMvc.Controllers
{
   // [ApiController]
    //[Route("[controller]/action")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _config;
        private readonly string apiBaseUrl;

        public AccountController(ILogger<AccountController> logger,  IConfiguration config)
        {
            _logger = logger;
            _config = config;
            apiBaseUrl = _config.GetValue<string>("WebAPIBaseUrl");

        }

       
     
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        //show the signup page
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpViewModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            
                using (var Response = await client.PostAsync("http://localhost:5000/api/account/register", content))
                {
                    if (Response.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        return RedirectToAction("signin", "Account");
                    }
                    else if (Response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        ModelState.Clear();
                        ModelState.AddModelError("Username", "Username Already Exist");
                        return View();
                    }
                    else
                    {
                        return View();
                    }
                }

            }
            
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Signin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var client = new HttpClient();

                HttpRequestMessage message = new HttpRequestMessage();

                message.Method = HttpMethod.Post;

                var json = System.Text.Json.JsonSerializer.Serialize(model);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                string url = "http://localhost:5000/api/account/login";
                message.Content = content;
                message.RequestUri = new Uri(url);



                var response = await client.SendAsync(message);
                var responsebody = await response.Content.ReadAsStringAsync();

                if (responsebody == "Incorrect Credentials")
                {
                    ViewBag.Error = responsebody;
                    return View();
                }
                var res = System.Text.Json.JsonSerializer.Deserialize<UserToReturnDto>(responsebody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                
                if (res.Role.FirstOrDefault() == "Admin")
                {
                    return RedirectToAction("Dashboard", "Admin", res);
                }

                return RedirectToAction("Dashboard", "User", res);
            }

            return View(model);
                

          
        }
       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
