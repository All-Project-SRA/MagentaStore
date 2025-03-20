using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Magenta_S.Models;
using System.Data.Entity;

namespace Magenta_S.Controllers
{
    public class AccountController : Controller
    {
        private readonly magentadb _magentaDataContext = new magentadb();

        public ActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterUser(RegisterUser Model)
        {
            if (_magentaDataContext.Users.Any(x => x.Username == Model.Username))
            {
                ModelState.AddModelError("Username", " the Username is alerady in use ");
                return View();
            }

            if (ModelState.IsValid)
            {
                RegisterUser registerUser = new RegisterUser();

                User user = registerUser.registerUser(Model);

                _magentaDataContext.Users.Add(user);
                _magentaDataContext.SaveChanges();

                return RedirectToAction("index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult RegisterDealer()
        {
            return View("RegisterUser");
        }

        [HttpPost]
        public ActionResult RegisterDealer(RegisterUser Model)
        {
            if (_magentaDataContext.Users.Any(x => x.Username == Model.Username))
            {
                ModelState.AddModelError("Username", " the Username is alerady in use ");
                return View("RegisterUser");
            }

            if (string.IsNullOrEmpty(Model.CommercialRecord))
            {
                ModelState.AddModelError("CommercialRecord", " the CommercialRecord is Required ");
                return View("RegisterUser");
            }

            if (ModelState.IsValid)
            {
                RegisterUser registerUser = new RegisterUser();
                User user = registerUser.RegisterDealer(Model);

                _magentaDataContext.Users.Add(user);
                _magentaDataContext.SaveChanges();

                return RedirectToAction("index", "Home");
            }
            return View();
        }

        public ActionResult Login(string Username, string Password)
        {
            if (ModelState.IsValid)
            {
                RegisterUser registerUser = new RegisterUser();

                if (registerUser.CheckUser(Username, Password))
                {
                    int idc = registerUser.CheckTypeUser(Username, Password);

                    Session["iduser"] = registerUser.iduseesion;

                    if (idc == 2)
                    {
                        return RedirectToAction("HomeCustomer", "Home");
                    }
                    else if (idc == 3)
                    {
                        return RedirectToAction("HomeDealer", "Home");
                    }
                    else if (idc == 1)
                    {
                        return RedirectToAction("HomeAdmin", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("Username", " The Username is Not true or password ");
                    return View("RegisterUser");
                }
            }
            return View("RegisterUser");
        }

        public ActionResult UpdateAccount()
        {
            int id = (int)Session["iduser"];

            UpdateAccount updateAccount = new UpdateAccount();

            updateAccount.UpadateAccount(id);

            return View(updateAccount);
        }

        [HttpPost]
        public ActionResult UpdateAccount(UpdateAccount Model)
        {
            UpdateAccount updateAccount = new UpdateAccount();
            if (!string.IsNullOrEmpty(Model.oldPassword))
            {
                if (Model.oldPassword != Models.UpdateAccount.GetPassword(Model.Idu))
                {
                    ModelState.AddModelError("oldPassword", "The old password in not True");
                    return View();
                }
            }
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Model.Password))
                {
                    Model.Password = Models.UpdateAccount.GetPassword(Model.Idu);
                }

                User user = updateAccount.UpadateAccount(Model);

                _magentaDataContext.Entry(user).State = EntityState.Modified;
                _magentaDataContext.SaveChanges();

                if (user.Idc == 1)
                    return RedirectToAction("HomeAdmin", "Home");

                else if (user.Idc == 2)
                    return RedirectToAction("HomeCustomer", "Home");

                else
                    return RedirectToAction("HomeDealer", "Home");

            }

            return RedirectToAction("UpdateAccount");
        }

    }
}