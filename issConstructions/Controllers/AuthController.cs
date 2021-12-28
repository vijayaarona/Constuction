using issConstructions.Models;
using issDomain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace issConstructions.Controllers
{
    public class AuthController : Controller
    {
        private issDB db = new issDB();

        // GET: Auth
        //public ActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult Index(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.Error = "";
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginView loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var enUser = db.users.FirstOrDefault(x => x.Username == loginView.username && x.Password == loginView.password);
                if (enUser != null)
                {
                    //var enEmployee = db.users.FirstOrDefault(x => x.Username == enUser.ID);
                    CustomSerializeModel userModel = new CustomSerializeModel()
                    {
                        id = enUser.ID,
                        name = enUser.Username,
                        //email = enEmployee != null ? enEmployee.name : string.Empty,
                        role = enUser.SelectUserType,
                        //companyId = enUser.companyId,
                        //EId = enEmployee != null ? enEmployee.id : 0
                    };
                    string userData = JsonConvert.SerializeObject(userModel);
                    FormsAuthenticationTicket authTicket =
                        new FormsAuthenticationTicket(1, loginView.username, DateTime.Now, DateTime.Now.AddMinutes(30), false, userData);

                    string enTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie("issContrucations", enTicket);
                    Response.Cookies.Add(faCookie);
                }
                if (enUser == null)
                {
                    ViewBag.Error = (".....Something Wrong : Username or Password invalid ^_^..... ");
                    return View(loginView);
                }
                if (Url.IsLocalUrl(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            //ModelState.AddModelError("Something Wrong : Username or Password invalid ^_^ ");
            return View(loginView);
        }

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("issContrucations", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Auth", null);
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}