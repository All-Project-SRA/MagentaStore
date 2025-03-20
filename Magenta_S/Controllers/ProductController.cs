using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magenta_S.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Add_Product()
        {
            return View();
        }

        public ActionResult All_Product()
        {
            return View();
        }
        public ActionResult My_Product()
        {
            return View();
        }

        public ActionResult Products_For_Admin()
        {
            return View();
        }
        
    }
}