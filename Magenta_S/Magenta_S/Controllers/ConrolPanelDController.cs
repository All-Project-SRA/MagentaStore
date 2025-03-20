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
        private readonly magentadb _magentaDataContext = new magentadb();

        private User GetUserByID(int ID)
        {
            return _magentaDataContext.Users.FirstOrDefault(x => x.Idu == ID);
        }

        public ActionResult ConrolPanelD()
        {
            return View();
        }

        public ActionResult Dealers_Reviews()
        {
            List<User> user = _magentaDataContext.Users.Where(x => x.Idc == 3 && x.Password != "unlockmagenta")
                                                       .ToList();
            return View(user);
        }

        [HttpPost]
        public ActionResult Dealers_Reviews(int id, string but)
        {
            if (but == "delete")
            {
                User user = GetUserByID(id);
                try
                {
                    _magentaDataContext.Users.Remove(user);
                    _magentaDataContext.SaveChanges();

                }
                catch
                {
                    user.Password = "unlockmagenta";

                    _magentaDataContext.Entry(user).State = EntityState.Modified;
                    _magentaDataContext.SaveChanges();

                    try
                    {
                        List<Product> products = _magentaDataContext.Products.Where(x => x.Idu == user.Idu)
                                                                      .ToList();
                        foreach (var item in products)
                        {
                            item.Ok = false;
                            item.count = 0;

                            _magentaDataContext.Entry(item).State = EntityState.Modified;
                            _magentaDataContext.SaveChanges();

                        }
                    }
                    catch { }
                }

                return RedirectToAction("Dealers_Reviews");
            }
            else
            {
                return RedirectToAction("My_Buy", "Bays", new { @id = id });
            }
        }

        public ActionResult All_Customer()
        {
            List<User> users = _magentaDataContext.Users.Where(x => x.Idc == 2 && x.Password != "unlockmagenta")
                                                        .ToList();
            return View(users);
        }

        [HttpPost]
        public ActionResult All_Customer(int id, string but)
        {
            if (but == "delete")
            {
                User user = GetUserByID(id);

                try
                {
                    _magentaDataContext.Users.Remove(user);
                    _magentaDataContext.SaveChanges();

                    return RedirectToAction("All_Customer");
                }
                catch
                {
                    user.Password = "unlockmagenta";

                    _magentaDataContext.Entry(user).State = EntityState.Modified;
                    _magentaDataContext.SaveChanges();

                    return RedirectToAction("All_Customer");
                }

            }
            else
            {
                return RedirectToAction("My_Buy", "Bays", new { @id = id });
            }

        }
    }
}