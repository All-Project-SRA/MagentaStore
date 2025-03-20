using Magenta_S.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Magenta_S.Controllers
{
    public class HomeController : Controller
    {
        private readonly magentadb _magentaDataContext = new magentadb();

        public async Task<ActionResult> Index()
        {
            ProductA product = new ProductA();
            return View(product);
        }

        public ActionResult HomeAdmin()
        {
            ProductA product = new ProductA();
            product.Idu = (int)Session["iduser"];

            return View(product);
        }

        public ActionResult HomeDealer()
        {
            ProductA product = new ProductA();
            product.Idu = (int)Session["iduser"];

            return View(product);
        }

        [HttpPost]
        public ActionResult HomeDealer(int Idproduct, string selectc, string selects, int countp, int counttheproduct)
        {
            int iduser = (int)Session["iduser"];

            if (counttheproduct < countp)
            {
                ModelState.AddModelError("lll", " The Count of product is Not Available");

                ProductA product = new ProductA();
                product.Idu = (int)Session["iduser"];

                return View(product);
            }
            int ch = 0;
            try
            {
                ch = _magentaDataContext.Buys.Where(x => x.Idu == iduser && x.Idproduct == Idproduct)
                                             .Max(x => x.checkp);
            }

            catch
            {
                ch = 0;

            }
            Buy buy = new Buy();

            buy.Idproduct = Idproduct;
            buy.bill = null;
            buy.ColorP = selectc;
            buy.Sizee = selects;
            buy.Count = countp;
            buy.Idu = (int)Session["iduser"];
            buy.checkp = ch + 1;

            try
            {
                _magentaDataContext.Buys.Add(buy);
                _magentaDataContext.SaveChanges();
            }
            catch
            {

                ModelState.AddModelError("count", " the bay aleardy exit in Carta ");

                ProductA product = new ProductA();
                product.Idu = (int)Session["iduser"];

                return View(product);
            }
            return RedirectToAction("HomeDealer");
        }

        public async Task<ActionResult> HomeCustomer()
        {
            ProductA product = new ProductA();

            product.Idu = (int)Session["iduser"];
            product.ProductFavirote = await product.GetSuggestedProducts();

            return View(product);
        }

        [HttpPost]
        public ActionResult HomeCustomer(int Idproduct, string selectc, string selects, int countp, int counttheproduct)
        {
            int iduser = (int)Session["iduser"];

            if (counttheproduct < countp)
            {
                ModelState.AddModelError("lll", " The Count of product is Not Available");

                ProductA product = new ProductA();
                product.Idu = (int)Session["iduser"];

                return View(product);
            }

            int ch = 0;

            try
            {
                ch = _magentaDataContext.Buys.Where(x => x.Idu == iduser && x.Idproduct == Idproduct).Max(x => x.checkp);
            }

            catch
            {
                ch = 0;

            }

            Buy buy = new Buy();

            buy.Idproduct = Idproduct;
            buy.bill = null;
            buy.ColorP = selectc;
            buy.Sizee = selects;
            buy.Count = countp;
            buy.checkp = ch + 1;
            buy.Idu = (int)Session["iduser"];

            buy.Ok = false;

            try
            {
                _magentaDataContext.Buys.Add(buy);
                _magentaDataContext.SaveChanges();
            }
            catch
            {

                ModelState.AddModelError("count", " the bay aleardy exit in Carta ");
                ProductA product = new ProductA();
                product.Idu = (int)Session["iduser"];

                return View(product);

            }
            return RedirectToAction("HomeCustomer");
        }

    }
}