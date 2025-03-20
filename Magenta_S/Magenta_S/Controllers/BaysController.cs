using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magenta_S.Models;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Magenta_S.Controllers
{
    public class BaysController : Controller
    {
        private readonly magentadb _magentaDataContext = new magentadb();

        public ActionResult Carta()
        {
            Bay_Product product = new Bay_Product();

            product.Idu = (int)Session["iduser"];

            User user = _magentaDataContext.Users.FirstOrDefault(x => x.Idu == product.Idu);
            product.idseesion = user.Idc;

            return View(product);
        }

        [HttpPost]
        public ActionResult Carta(string Sizee, string ColorP, int Count, int Idproduct, bool Ok, int Idu, int checkp)
        {
            Buy buy = new Buy();

            buy.Idproduct = Idproduct;
            buy.Idu = Idu;
            buy.Ok = Ok;
            buy.ColorP = ColorP;
            buy.Sizee = Sizee;
            buy.bill = null;
            buy.Count = Count;
            buy.checkp = checkp;

            _magentaDataContext.Entry(buy).State = EntityState.Deleted;
            _magentaDataContext.SaveChanges();

            return RedirectToAction("Carta");
        }

        [HttpPost]
        public ActionResult buy(string phone, string address, int Idu)
        {
            Bill bill = new Bill();

            bill.Idu = Idu;
            bill.Newphone = phone;
            bill.Newplace = address;
            bill.date = DateTime.Now;

            _magentaDataContext.Bills.Add(bill);
            _magentaDataContext.SaveChanges();

            List<Buy> listbue = _magentaDataContext.Buys.Where(x => x.Idu == Idu && x.Ok == false)
                                                        .ToList();

            foreach (var item in listbue)
            {
                item.Ok = true;
                item.Bill1 = bill;

                _magentaDataContext.Entry(item).State = EntityState.Modified;

            }

            _magentaDataContext.SaveChanges();

            List<Buy> listbuy = _magentaDataContext.Buys.Where(x => x.bill == bill.Idbill).ToList();

            double totalprice = 0;
            int count = 0;

            foreach (var item in listbuy)
            {
                totalprice += item.Product.Price * item.Count * 1.05;
                count += item.Count;

            }

            bill.Totalprice = (int)totalprice;

            _magentaDataContext.Entry(bill).State = EntityState.Modified;
            _magentaDataContext.SaveChanges();

            User user = _magentaDataContext.Users.Where(x => x.Idu == Idu).Single();
            user.CountProduct = count;

            _magentaDataContext.Entry(user).State = EntityState.Modified;
            _magentaDataContext.SaveChanges();

            foreach (var item in listbuy)
            {
                Product product = item.Product;
                product.count = product.count - item.Count;

                _magentaDataContext.Entry(product).State = EntityState.Modified;

            }

            _magentaDataContext.SaveChanges();

            if (user.Idc == 2)
            {
                return RedirectToAction("HomeCustomer", "Home");
            }

            return RedirectToAction("HomeDealer", "Home");
        }

        public ActionResult My_Buy(int id)
        {
            int iduser = (int)Session["iduser"];

            User user = _magentaDataContext.Users.Where(x => x.Idu == id).Single();
            my_buy my_Buy = new my_buy();

            my_Buy.idu = id;
            my_Buy.idseesion = user.Idc;

            return View(my_Buy);
        }

        public async Task<ActionResult> AI_Suggestions()
        {
            int iduser = (int)Session["iduser"];

            User user = _magentaDataContext.Users.FirstOrDefault(x => x.Idu == iduser);

            my_buy my_Buy = new my_buy();

            my_Buy.idseesion = user.Idc;
            my_Buy.TextAPI = await my_Buy.GetProductSuggestionsForSeller();

            return View(my_Buy);
        }
    }
}