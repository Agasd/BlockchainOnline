using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlockchainOnline.DTOs;
using BlockchainOnline.Models;
using Microsoft.AspNetCore.Mvc;
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

        public static long getWalletBalance(String address)
        {
            HttpClient http = new HttpClient();
            string path = "/Wallets/"+address;
            string url = Config.basicUrl + path;

            HttpResponseMessage response = http.GetAsync(new Uri(url)).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if (response.StatusCode.ToString() == "OK")
            {
                return Convert.ToInt64(responseBody);
            }
            return -1;

        }
    }
}