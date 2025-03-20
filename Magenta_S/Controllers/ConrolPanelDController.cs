using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magenta_S.Models;
using System.Data.Entity;

namespace Magenta_S.Controllers
{
    public class ConrolPanelDController : Controller
    {
        // GET: ConrolPanelD
        public ActionResult ConrolPanelD()
        {
            return View();
        }
        public ActionResult Add_Admin()
        {
            return View();
        }
        public ActionResult Dealers_Reviews()
        {
            magentadb mg = new magentadb();
            List<User> user=  mg.Users.ToList();
            return View(user);
        }

        [HttpPost]
        public ActionResult Dealers_Delete(int id)
        {
            magentadb mg = new magentadb();
           User user=  mg.Users.Where(x => x.Idu == id).Single();
            mg.Users.Remove(user);
            mg.SaveChanges();
            return RedirectToAction("Dealers_Reviews");
        }
        [HttpPost]
        public ActionResult product_Bay(int id)
        {


            return RedirectToAction("Dealers_Reviews");
        }

       


    }
}