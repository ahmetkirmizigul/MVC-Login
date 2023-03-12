using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _20210116_d1_Mvc_Giris.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [Authorize]
        public ActionResult Index()
        {

            // Mvc ile birlikte gelen sayfalar arası veri taşıma işlemleri yapmamızı sağlayan 3 farklı mekanizma vardır.
            // Bunlar;
            // 1- ViewBag
            // 2- ViewData
            // 3- TempData

            // Öncelikle ViewBag ve View Data aynı çalışam mantığına sahip olmakla beraber kullanım şekilleri farklılık gösterir. ViewBag ve ViewData sadece kendi Controller yapısından View yapısına veri taşıma işlemleri için kullanılır.
            // TempData ise çok basit bir çalışma mantığı vardır. Controller yapıları arasında veri taşına işlemleri yapmamızı sağlar.

            ViewBag.Mesaj = "Mvc Derslerine hoş geldiniz. - ViewBag";
            ViewData["Mesaj2"] = "Mvc Derslerine hoş geldiniz. - ViewData";
            TempData["Mesaj"] = "Mvc Derslerine hoş geldiniz. - TempData";

            //return RedirectToAction("Hakkimizda");

            return View();
        }

        public ActionResult Hakkimizda()
        {
            return View();
        }
    }
}