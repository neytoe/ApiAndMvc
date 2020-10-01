using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheApi.DTO;
using TheMvc.ViewModel;

namespace TheMvc.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        //public ActionResult Dashboard(UserToReturnDto model)
        //{

        //    ViewBag.IsLoggedIn = true;
        //    return View(model);
        //}

        public async Task<ActionResult> DashBoardAsync(UserToReturnDto model)
        {

            HttpClient client = new HttpClient();

            //  string endpoint = apiBaseUrl + "api/account/register";
            var Response = await client.GetAsync("http://localhost:5000/api/admin/getusers");

            var responseBody = await Response.Content.ReadAsStringAsync();

            var responseObject = JsonConvert.DeserializeObject<IEnumerable<UserToReturnDto>>(responseBody);

            var res = new UserList
            {
                
                User = model
            };

            ViewBag.IsLoggedIn = true;
            return View(res);
        }


    }
}
