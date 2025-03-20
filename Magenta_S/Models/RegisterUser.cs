using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    //[MetadataType(typeof(RegisterUser))]
    //public partial class User
    //{
    //    //public string ConfirmPassword { get; set; }

    //}

    public class RegisterUser
    {

        public int Idu { get; set; }
        public int Idc { get; set; }
        [Required]
        public string Fname { get; set; }
        [Required]
        public string Lname { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string Username { get; set; }

        public DateTime Hiredate { get; set; }
        public string Password { get; set; }
       
        [Required]
        public System.DateTime birthday { get; set; }
        public Nullable<int> Point { get; set; }
        public string CommercialRecord { get; set; }
        public Nullable<int> CountProduct { get; set; }
        [Required]
        public string Phone { get; set; }

        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }


        public User registerUser(RegisterUser UserR) {
            User user = new User();
            user.Idc = 2;
            user.Fname =UserR.Fname;
            user.Lname =UserR.Lname;
            user.Password =UserR.Password;
            user.Username =UserR.Username;
            user.address =UserR.address;
            user.Gender =UserR.Gender;
            user.Phone =UserR.Phone;
            user.birthday = UserR.birthday;
            user.Hiredate = DateTime.UtcNow;
            return user;
        }
        public User RegisterDealer(RegisterUser UserR)
        {
            User user = new User();
            user.Idc = 3;
            user.Fname = UserR.Fname;
            user.Lname = UserR.Lname;
            user.Password = UserR.Password;
            user.Username = UserR.Username;
            user.address = UserR.address;
            user.Gender = UserR.Gender;
            user.Phone = UserR.Phone;
            user.birthday = UserR.birthday;
            user.CommercialRecord = UserR.CommercialRecord;
            user.Hiredate = DateTime.UtcNow;
            return user;
        }
        public bool Logine(string Username, string Password)
        {

            magentadb Context = new magentadb();
            return Context.Users.Where(x => x.Username == Username && x.Password == Password).Any();

        }

    }
}