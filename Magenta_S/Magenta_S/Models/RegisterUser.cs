using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    public class RegisterUser
    {
        private readonly magentadb _magentaDataContext = new magentadb();
        public int iduseesion { get; set; }

        public int Idu { get; set; }

        public int Idc { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid First Name")]
        public string Fname { get; set; }

        [Required]
        [RegularExpression(@"^([a-zA-Z0-9 \.\&\'\-]+)$", ErrorMessage = "Invalid Last Name")]
        public string Lname { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string address { get; set; }

        [Required]
        [Display(Name = "Email")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Username { get; set; }

        public DateTime Hiredate { get; set; }

        [Required]
        [Display(Name = "Password")]
        [MinLength(8, ErrorMessage = "Name cannot be younger than 8 characters.")]
        public string Password { get; set; }

        [Required]
        public System.DateTime birthday { get; set; }
        public Nullable<int> Point { get; set; }
        public string CommercialRecord { get; set; }
        public Nullable<int> CountProduct { get; set; }

        [Required(ErrorMessage = "Phone Number Required!")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
                   ErrorMessage = "Entered phone format is not valid.")]
        public string Phone { get; set; }

        [System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [MinLength(8, ErrorMessage = "Name cannot be younger than 8 characters.")]
        public string ConfirmPassword { get; set; }


        //ميثود خاصة بأنشاء حساب كزبون
        public User registerUser(RegisterUser UserR)
        {
            User user = new User();

            user.Point = 0;
            user.Idc = 2;
            user.Fname = UserR.Fname;
            user.Lname = UserR.Lname;
            user.Password = UserR.Password;
            user.Username = UserR.Username;
            user.address = UserR.address;
            user.Gender = UserR.Gender;
            user.Phone = UserR.Phone;
            user.birthday = UserR.birthday;
            user.Hiredate = DateTime.UtcNow;

            return user;
        }
        //ميثود خاصة بأنشاء حساب كتاجر

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
        //للتحقق بأن كلمة السر والأيميل متوافقين في الداتا بيز
        public bool CheckUser(string Username, string Password)
        {
            return _magentaDataContext.Users.Where(x => x.Username == Username && x.Password == Password && x.Point != null)
                                            .Any();
        }
        //ميثود لمعرفة بأن المسجل هو تاجر او زبون
        public int CheckTypeUser(string Username, string Password)
        {
            User user = _magentaDataContext.Users.Where(x => x.Username == Username && x.Password == Password)
                                              .Single();
            iduseesion = user.Idu;
            return user.Idc;
        }

    }
}