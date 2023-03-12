using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using _20210116_d1_Mvc_Giris.Metodlar;
using _20210116_d1_Mvc_Giris.Models;

namespace _20210116_d1_Mvc_Giris.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(Users user)
        {
            Entities db = new Entities();
            var dbUser = db.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (dbUser != null)
            {
                FormsAuthentication.SetAuthCookie(dbUser.UserName, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Mesaj"] = Metod.Alert("Kullanıcı adı veya parola bilgisini hatalı girdiniz.",
                    AlertTypes.Danger);
                return View(user);
            }
        }

        public ActionResult OturumuKapat()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}