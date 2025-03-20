using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    public class UpdateAccount : RegisterUser
    {
         
        public string oldPassword { get; set; }
        public void UpadateAccount(int id)
        {
            magentadb mg = new magentadb();
            User u = mg.Users.Where(x => x.Idu == id).Single();
            Fname = u.Fname;
            Lname = u.Lname;
            Username = u.Username;
            Password = u.Password;
            address = u.address;
            Gender = u.Gender;
            Phone = u.Phone;
            Idc = u.Idc;
            Idu = u.Idu;
            birthday = u.birthday;

        }
        public User UpadateAccount(UpdateAccount UserR)
        {
            magentadb mg = new magentadb();
            User u = mg.Users.Where(x => x.Idu == UserR.Idu).Single();

            User user = new User();
            user.Idc = 2;
            user.Idu = u.Idu;
            user.Fname = UserR.Fname;
            user.Lname = UserR.Lname;
            user.Username = u.Username;
            user.Password = UserR.Password;
            user.address = UserR.address;
            user.Hiredate = u.Hiredate;
            user.Gender = UserR.Gender;
            user.Phone = UserR.Phone;
            user.birthday = UserR.birthday;
            return user;
        }

        public static string togetpassword(int id) {

            magentadb mg = new magentadb();
            User u = mg.Users.Where(x => x.Idu == id).Single();
            return u.Password;
        }

    }
}