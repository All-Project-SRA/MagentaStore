using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Magenta_S.Models;
using Magenta_S.App_Start;

namespace Magenta_S
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);



            magentadb mg = new magentadb();
            List<Category> l= mg.Categories.ToList();
            if (l.Count ==0 )
            {
                Category c = new Category();
                c.Cname = "Admin";
                c.Idc = 1;
               
                Category c2 = new Category();
                c2.Cname = "Customer";
                c2.Idc = 2;
                Category c3 = new Category();
                c3.Cname = "Dealer";
                c3.Idc = 3;
                
                mg.Categories.Add(c);
                mg.Categories.Add(c2);
                mg.Categories.Add(c3);
                mg.SaveChanges();

            }
            List<User> admin = mg.Users.Where(x=>x.Idc==1).ToList();
            if (admin.Count == 0)
            {
                User u = new User();
                u.Idc = 1;
                u.address = "admin";
                u.birthday = DateTime.Now;
                u.Fname = "ADMIN";
                u.Gender = "ADMIN";
                u.Lname = "Admin";
                u.Password = "AdminMagenta";
                u.Phone = "Admin";
                u.Username = "Admin@gmail.com";
                u.Point = 0;
                u.Hiredate = DateTime.Now;
                mg.Users.Add(u);
                mg.SaveChanges();

            }

        }
    }
}
