using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using PagedList;
using _20210116_d1_Mvc_Giris.Metodlar;
using _20210116_d1_Mvc_Giris.Models;

namespace _20210116_d1_Mvc_Giris.Controllers
{
    public class UrunController : Controller
    {
        Entities db = new Entities();

        // GET: Urun
        public ActionResult Index(int? SayfaNo, string urunAdi, int? kategori, int? tedarikci, string sort, string sortType)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var urunler = db.Products.ToList();

            if (!string.IsNullOrEmpty(urunAdi))
            {
                urunler = urunler.Where(x => x.ProductName.ToLower().Contains(urunAdi.ToLower())).ToList();
            }

            if (kategori.ToInt() > 0)
            {
                urunler = urunler.Where(x => x.CategoryID == kategori).ToList();
            }

            if (tedarikci.ToInt() > 0)
            {
                urunler = urunler.Where(x => x.SupplierID == tedarikci).ToList();
            }

            switch (sort)
            {
                case "ProductName":
                    if (sortType == "ASC")
                        urunler = urunler.OrderBy(x => x.ProductName).ToList();
                    else if(sortType == "DESC")
                        urunler = urunler.OrderByDescending(x => x.ProductName).ToList();
                    break;
                case "CategoryID":
                    if (sortType == "ASC")
                        urunler = urunler.OrderBy(x => x.Categories.CategoryName).ToList();
                    else if (sortType == "DESC")
                        urunler = urunler.OrderByDescending(x => x.Categories.CategoryName).ToList();
                    break;
                case "SupplierID":
                    if (sortType == "ASC")
                        urunler = urunler.OrderBy(x => x.Suppliers.CompanyName).ToList();
                    else if (sortType == "DESC")
                        urunler = urunler.OrderByDescending(x => x.Suppliers.CompanyName).ToList();
                    break;
                default:
                    urunler = urunler.OrderByDescending(x => x.ProductID).ToList();
                    break;
            }


            var urunList = urunler.ToPagedList(_sayfaNo, 10);

            ViewBag.Kategori = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.Tedarikci = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View(urunList);
        }

        public ActionResult Ekle()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Products products)
        {
            db.Products.Add(products);
            try
            {
                db.SaveChanges();
                TempData["Mesaj"] = Metod.Alert("Ekleme işlemi başarı ile tamamlanmıştır.", AlertTypes.Success);
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", products.CategoryID);
                ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", products.SupplierID);
                TempData["Mesaj"] = Metod.Alert("Ekleme işlemi esnasında bir hata meydana geldi.", AlertTypes.Success);
                return View();
            }
        }

        public ActionResult Duzenle(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            var urun = db.Products.Find(id);
            if (urun == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Gönderilen istek bilgisi bulunamadı.");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", urun.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", urun.SupplierID);

            return View(urun);
        }

        [HttpPost]
        public ActionResult Duzenle(Products urun)
        {
            var dbUrun = db.Products.Find(urun.ProductID);
            if (dbUrun == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dbUrun.ProductName = urun.ProductName;
            dbUrun.CategoryID = urun.CategoryID;
            dbUrun.SupplierID = urun.SupplierID;
            dbUrun.QuantityPerUnit = urun.QuantityPerUnit;
            dbUrun.UnitPrice = urun.UnitPrice;
            dbUrun.UnitsInStock = urun.UnitsInStock;
            dbUrun.Discontinued = urun.Discontinued;

            try
            {
                db.SaveChanges();
                TempData["Mesaj"] = Metod.Alert("Ürün kayıt işlemi başarı ile tamamlanmıştır.", AlertTypes.Success);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["Mesaj"] =
                    Metod.Alert("Ürün kayıt işlemi esnasında bir hata meydana geldi.", AlertTypes.Danger);

                ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", urun.CategoryID);
                ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", urun.SupplierID);
                return View(urun);
            }
        }


        public ActionResult Sil(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var urun = db.Products.Find(id);
            if (urun == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            db.Products.Remove(urun);
            try
            {
                db.SaveChanges();
                TempData["Mesaj"] = Metod.Alert("Silme işlemi başarı ile gerçekleşti.", AlertTypes.Success);
            }
            catch (Exception e)
            {
                TempData["Mesaj"] = Metod.Alert("Silme işlemi esnasında bir hata meydana geldi.", AlertTypes.Danger);
            }

            return RedirectToAction("Index");
        }
    }
}