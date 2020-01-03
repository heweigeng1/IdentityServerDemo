using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebClient.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [HttpGet]
        public  IActionResult Index()
        {
            var client = new HttpClient();
            var disco = client.GetAsync("https://demo.identityserver.io");
            return new JsonResult(disco.Id);
        }
    }
}