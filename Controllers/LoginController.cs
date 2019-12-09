using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlockchainOnline.Models;
using Microsoft.AspNetCore.Http;
using System.Net.Http;
using Nancy.Json;
using BlockchainOnline.DTOs;
using Newtonsoft.Json;
using BlockchainOnline;
using System.Text;

namespace MVCLogin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(User userModel)
        {
            //HttpContext.Session.SetString("username", userModel.UserName);
            /*ar userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", userModel);
                }
                else
                {
                HttpContext.Current.Sessio["userID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("Index", "Home");
                }
            }*/

            UserLoginDTO requestBodyUser = new UserLoginDTO();
            requestBodyUser.username = userModel.UserName;
            requestBodyUser.password = userModel.Password;
            var json = new JavaScriptSerializer().Serialize(requestBodyUser);

            HttpClient http = new HttpClient();
            HttpContent content = new StringContent(json, Encoding.UTF8,"application/json");

            string path = "/auth/login";
            string url = Config.basicUrl + path;

            HttpResponseMessage response = http.PostAsync(new Uri(url), content).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            if(response.StatusCode.ToString() == "OK")
            {
                TokenDTO token = JsonConvert.DeserializeObject<TokenDTO>(responseBody);

                HttpContext.Session.SetString("username", userModel.UserName);
                HttpContext.Session.SetString("token", token.tokenString);

                return RedirectToAction("Index", "Home");
            }
            userModel.LoginErrorMessage = "Wrong username or password.";
            return View("Index", userModel);

        }

        public ActionResult Logout()
        {
            HttpContext.Session.Remove("token");
            return RedirectToAction("Index", "Login");
        }
        private string sendLoginRequest(User userModel)
        {
            return "";
        }
    }
}