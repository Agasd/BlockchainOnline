using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlockchainOnline.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BlockchainOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ModelState.Clear();
            if (HttpContext.Session.GetString("token") != null)
            {
                HomeViewModel hvm = new HomeViewModel();
                hvm.userInfo = JsonConvert.DeserializeObject<UserInfo>(HttpContext.Session.GetString("userInfo"));
                //hvm.userInfo = UserController.getUserInfoByToken(HttpContext.Session.GetString("token"));
                hvm.token = HttpContext.Session.GetString("token");
                //return RedirectToAction("Index", "Login");
                return View(hvm);
            }
            return RedirectToAction("Index", "Login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
