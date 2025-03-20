using Magenta_S.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Magenta_S.Controllers
{
    public class RequistController : Controller
    {

        private readonly magentadb _magentaDataContext = new magentadb();

        private Product GetProductByID(int ID)
        {
            return _magentaDataContext.Products.FirstOrDefault(x => x.Idproduct == ID);
        }

        private User GetUserByID(int ID)
        {
            return _magentaDataContext.Users.FirstOrDefault(x => x.Idu == ID);
        }

        public ActionResult Requist()
        {
            RequistAdmin requistAdmin = new RequistAdmin();

            return View(requistAdmin);
        }

        [HttpPost]
        public ActionResult accept_product(int Idproduct, string submit)
        {
            Product product = GetProductByID(Idproduct);

            if (submit == "send")
            {
                product.Ok = true;

                _magentaDataContext.Entry(product).State = EntityState.Modified;
                _magentaDataContext.SaveChanges();

                return RedirectToAction("Requist");
            }

            else if (submit == "delete")
            {
                _magentaDataContext.Products.Remove(product);
                _magentaDataContext.SaveChanges();

                return RedirectToAction("Requist");
            }
            else
                return View();
        }

        [HttpPost]
        public ActionResult accept_account(int idu, string submit)
        {
            User user = GetUserByID(idu);

            if (submit == "Done")
            {
                user.Point = 0;

                _magentaDataContext.Entry(user).State = EntityState.Modified;
                _magentaDataContext.SaveChanges();

                return RedirectToAction("Requist");
            }

            else
            {
                _magentaDataContext.Entry(user).State = EntityState.Deleted;
                _magentaDataContext.SaveChanges();

                return RedirectToAction("Requist");
            }
        }

        public ActionResult bay(int idbill, string submit)
        {
            if (submit == "donep")
            {
                Bill bill = _magentaDataContext.Bills.Where(x => x.Idbill == idbill).Single();

                bill.ok = true;

                _magentaDataContext.Entry(bill).State = EntityState.Modified;
                _magentaDataContext.SaveChanges();

                return RedirectToAction("Requist");

            }

            return RedirectToAction("Requist");
        }

    }
}