using Library_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Library_Project.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        MVC_PROJECTSEntities db = new MVC_PROJECTSEntities();
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserDetail user)
        {
            //Session["Username"] = user.UserName;
            //Session["Password"] = user.Password;
            var userinfo = db.UserDetails.Where(u => u.UserName == user.UserName && u.Password == user.Password).SingleOrDefault();
            if (userinfo != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserId.ToString(),false);
                return RedirectToAction("GetBooks", "Library");
            }
            else
            {
                return View();
            }
        }

    }
}
