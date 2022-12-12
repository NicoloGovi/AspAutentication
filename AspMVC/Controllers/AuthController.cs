using AspMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AspMVC.Controllers
{
    public class AuthController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login( UserCredentialsDto credentials)
        {
            using(var context = new UsersContext())
            {
                bool ok = context.Users.Where(q => q.Username.ToLower() == credentials.Username.ToLower() && q.Password == credentials.Password).Count() == 1;
                if (ok)
                {
                    FormsAuthentication.SetAuthCookie(credentials.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                TempData["Error"] = "Username o password errati!";
                return RedirectToAction("Login");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}