using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using _20210116_d1_Mvc_Giris.Metodlar;
using _20210116_d1_Mvc_Giris.Models;

namespace _20210116_d1_Mvc_Giris.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        Entities db = new Entities();

        [Authorize]
        // GET: Kategori
        public ActionResult Index()
        {
            ViewBag.Kategoriler = db.Categories.ToList();
            return View();
        }

        public ActionResult Listele()
        {
            var kategoriler = db.Categories.ToList();
            return View(kategoriler);
        }

        //[HttpGet] 
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(string CategoryName, string Description)
        {
            Categories cat = new Categories();
            cat.CategoryName = CategoryName;
            cat.Description = Description;
            db.Categories.Add(cat);
            try
            {
                db.SaveChanges();
                TempData["Mesaj"] =
                    Metod.Alert("Kategori ekleme işlemi başarı ile tamamlanmıştır.", AlertTypes.Success);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Mesaj"] =
                    Metod.Alert("Kategori ekleme işlemi esnasında bir hata meydana geldi.", AlertTypes.Danger);
                return View();
            }

        }

        public ActionResult Duzenle(int id)
        {
            var kategori = db.Categories.FirstOrDefault(x => x.CategoryID == id);
            return View(kategori);
        }

        [HttpPost]
        public ActionResult Duzenle(Categories cat)
        {
            var dCat = db.Categories.Find(cat.CategoryID);
            dCat.CategoryName = cat.CategoryName;
            dCat.Description = cat.Description;

            try
            {
                db.SaveChanges();
                TempData["Mesaj"] = Metod.Alert("Kategori güncelleme işlemi başarı ile tamamlanmıştır.",
                    AlertTypes.Success);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Mesaj"] =
                    Metod.Alert("Kategori güncelleme işlemi esnasında bir hata meydana geldi. Lütfen tekrar deneyiniz.",
                        AlertTypes.Danger);
                return View();
            }
        }

        public ActionResult Sil(int id)
        {
            var cat = db.Categories.Find(id);
            if (cat != null)
            {
                db.Categories.Remove(cat);
                db.SaveChanges();
                TempData["Mesaj"] = Metod.Alert("Silme işlemi başarı ile tamamlanmıştır.", AlertTypes.Success);
            }
            else
            {
                TempData["Mesaj"] = Metod.Alert("Silme işlemi esnasında bir hata meydana geldi.", AlertTypes.Danger);
            }

            return RedirectToAction("Index");
        }

    }
}