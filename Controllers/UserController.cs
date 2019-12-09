using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BlockchainOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BlockchainOnline.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public static UserInfo getUserInfoByToken(String token)
        {
            HttpClient http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            string path = "/Users";
            string url = Config.basicUrl + path;

            HttpResponseMessage response = http.GetAsync(new Uri(url)).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode.ToString() == "OK")
            {
                UserInfo userInfo = JsonConvert.DeserializeObject<UserInfo>(responseBody);
                return userInfo;
            }
            return null;

        }
    }
}