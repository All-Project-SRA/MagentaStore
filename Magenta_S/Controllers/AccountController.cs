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
        // GET: Account
        public ActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterUser(RegisterUser Model)
        {
            if (string.IsNullOrEmpty(Model.Username))
            {

                ModelState.AddModelError("Username", " the Username is Required ");

            }
            if (ModelState.IsValid)
            {
                magentadb mg = new magentadb();
                RegisterUser registerUser = new Models.RegisterUser();
                User user = registerUser.registerUser(Model);
                mg.Users.Add(user);
                mg.SaveChanges();
                return View();
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
            if (string.IsNullOrEmpty(Model.Username))
            {

                ModelState.AddModelError("Username", " the Username is Required ");

            }
            if (ModelState.IsValid)
            {
                magentadb mg = new magentadb();
                RegisterUser registerUser = new Models.RegisterUser();
                User user = registerUser.RegisterDealer(Model);
                mg.Users.Add(user);
                mg.SaveChanges();
                return View();
            }
            return View();
        }
        public string Login(string Username,string Password)
        {
            if (ModelState.IsValid)
            {
                magentadb mg = new magentadb();
                RegisterUser registerUser = new Models.RegisterUser();
                if (registerUser.Logine(Username,Password))
                    return "is login";
                else
                return "";
            }
            return "";
        }
        public ActionResult UpdateAccount(int id)
        {
            magentadb mg = new magentadb();
            User user = mg.Users.Where(x => x.Idu ==id).Single();
            UpdateAccount up = new Models.UpdateAccount();
            up.UpadateAccount(id);
            return View(up);
        }
        [HttpPost]
        public ActionResult UpdateAccount(UpdateAccount Model)
        {
                magentadb mg = new magentadb();
            UpdateAccount UP = new Models.UpdateAccount();
            if (!string.IsNullOrEmpty(Model.oldPassword)) { 
            if (Model.oldPassword != Models.UpdateAccount.togetpassword(Model.Idu))
            {
                ModelState.AddModelError("oldPassword", " the old in not True");
                return View();
            }
            }
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(Model.Password))
                { Model.Password = Models.UpdateAccount.togetpassword(Model.Idu); }
                User user = UP.UpadateAccount(Model);
               
                mg.Entry(user).State = EntityState.Modified;
                mg.SaveChanges();
                return RedirectToAction("RegisterUser");
            }
            return RedirectToAction("RegisterUser");
        }


    }
}