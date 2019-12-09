using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BlockchainOnline.Models;
using Microsoft.AspNetCore.Http;

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
            HttpContext.Session.SetString("username", userModel.UserName);
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
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOut()
        {
            /*int userId = (int)Session["userID"];
            Session.Abandon();*/
            return RedirectToAction("Index", "Login");
        }
        private string sendLoginRequest(User userModel)
        {
            return "";
        }
    }
}