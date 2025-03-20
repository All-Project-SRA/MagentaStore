using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    public class UpdateAccount
    {
        private readonly magentadb _magentaDataContext = new magentadb();
        public int iduseesion { get; set; }

        public int Idu { get; set; }

        public int Idc { get; set; }

        [Required]
        public string Fname { get; set; }

        [Required]
        public string Lname { get; set; }

        public string Gender { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Username { get; set; }

        public DateTime Hiredate { get; set; }

        [Display(Name = "Password")]
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

        public string oldPassword { get; set; }

        //  user ميقود لجلب داتا من نوع 
        public void UpadateAccount(int id)
        {
            User user = _magentaDataContext.Users.Where(x => x.Idu == id).Single();

            Fname = user.Fname;
            Lname = user.Lname;
            Username = user.Username;
            Password = user.Password;
            address = user.address;
            Gender = user.Gender;
            Phone = user.Phone;
            Idc = user.Idc;
            Idu = user.Idu;
            birthday = user.birthday;

        }

        public User UpadateAccount(UpdateAccount UserR)
        {
            User _user = _magentaDataContext.Users.FirstOrDefault(x => x.Idu == UserR.Idu);

            User user = new User();

            user.Idc = _user.Idc;
            user.Idu = _user.Idu;
            user.Fname = UserR.Fname;
            user.Lname = UserR.Lname;
            user.Username = _user.Username;
            user.Password = UserR.Password;
            user.address = UserR.address;
            user.Hiredate = _user.Hiredate;
            user.Gender = _user.Gender;
            user.Phone = UserR.Phone;
            user.birthday = UserR.birthday;
            user.Point = _user.Point;
            user.CommercialRecord = _user.CommercialRecord;

            return user;
        }

        public static string GetPassword(int ID)
        {
            magentadb _magentaDataContext = new magentadb();

            User user = _magentaDataContext.Users.FirstOrDefault(x => x.Idu == ID);

            return user.Password;
        }

    }
}