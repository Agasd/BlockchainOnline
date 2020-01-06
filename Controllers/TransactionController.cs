using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BlockchainOnline.DTOs;
using BlockchainOnline.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nancy.Json;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlockchainOnline.Controllers
{
    public class TransactionController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                String token = HttpContext.Session.GetString("token");

                var json = new JavaScriptSerializer().Serialize("");
                TransactionViewModel tvm = new TransactionViewModel();

                HttpClient http = new HttpClient();
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                string path = "/Transactions/getall";
                string url = Config.basicUrl + path;
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));

                HttpResponseMessage response = http.GetAsync(new Uri(url)).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;



                tvm.transactions = new LinkedList<Transaction>(JsonConvert.DeserializeObject<Transaction[]>(responseBody));

                return View(tvm);
            }
            return RedirectToAction("Index", "Login");
        }
        public IActionResult ShowSend()
        {
            TransactionViewModel tvm = new TransactionViewModel();
            if (HttpContext.Session.GetString("token") != null)
            {
                return View("Send",tvm);
            }
            return RedirectToAction("Index", "Login");

        }
        [HttpPost]

        public IActionResult Send(TransactionViewModel tvm)
        {
            if (HttpContext.Session.GetString("token") != null)
            {
                ExecuteTransactionDTO requestTransaction = new ExecuteTransactionDTO();
                requestTransaction.Sender_publicKey = tvm.sender_key;
                requestTransaction.Recepient_publicKey = tvm.recepient_key;
                requestTransaction.Ether_Amount = Convert.ToInt64(tvm.amount);
                var json = new JavaScriptSerializer().Serialize(requestTransaction);

                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", HttpContext.Session.GetString("token"));
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                string path = "/transactions";
                string url = Config.basicUrl + path;

                HttpResponseMessage response = http.PutAsync(new Uri(url), content).Result;
                string responseBody = response.Content.ReadAsStringAsync().Result;
                if (response.StatusCode.ToString() == "OK")
                {
                    tvm.result = "Success";
                    return View("Send", tvm);
                }
                tvm.result = "Something went wrong";
                return View("Send", tvm);

            }
            return RedirectToAction("Index", "Login");


        }


    }
}
