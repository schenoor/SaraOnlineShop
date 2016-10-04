using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyMVCProject.Models;

namespace MyMVCProject.Controllers
{
    public class HomeController : Controller
    {
        ShopDBEntities featured = new ShopDBEntities();
        public ActionResult Index()
        {
            var products = featured.ProductsDatas.ToList();
            ViewData["featured"]= products[new Random().Next(products.Count)];
            return View(products);
        }        

        public ActionResult About()
        {
            ViewBag.Message = "A little bit about RackTaka.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}