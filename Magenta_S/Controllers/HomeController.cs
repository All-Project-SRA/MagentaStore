using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magenta_S.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomeDealer()
        {
            return View();
        }

        public ActionResult HomeCustomer()
        {
            return View();
        }
        
    }
}