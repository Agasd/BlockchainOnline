using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlockchainOnline.DTOs;
using BlockchainOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;

namespace BlockchainOnline.Controllers
{
    public class WalletController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public IActionResult walletBalance(String id)
        {
            WalletBalanceDTO wbd = new WalletBalanceDTO();
            wbd.balance = getWalletBalance(id);

            return Json(wbd);
        }

        public static string getWalletBalance(String address)
        {
            HttpClient http = new HttpClient();
            string path = "/Wallets/"+address;
            string url = Config.basicUrl + path;

            HttpResponseMessage response = http.GetAsync(new Uri(url)).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode.ToString() == "OK")
            {
                return responseBody;
            }
            return "-1";

        }
        public Microsoft.AspNetCore.Mvc.ActionResult NewWallet(String address) {
            var json = new JavaScriptSerializer().Serialize("");

            HttpClient http = new HttpClient();
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            string path = "/Wallets";
            string url = Config.basicUrl + path;
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

            HttpResponseMessage response = http.PostAsync(new Uri(url), content).Result;

            var userInfoJson = new JavaScriptSerializer().Serialize(UserController.getUserInfoByToken(HttpContext.Session.GetString("token")));

            HttpContext.Session.SetString("userInfo", userInfoJson);

            return RedirectToAction("Index", "Home");

        }
    }
}