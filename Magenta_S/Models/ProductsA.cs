using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    public class ProductsA
    {
        public int Idproduct { get; set; }
        public int Idu { get; set; }
        public string Type { get; set; }
        public string Genderoftype { get; set; }
        public string Quality { get; set; }
        public string Season { get; set; }
        public string Company { get; set; }
        public string Description { get; set; }
        public byte[] Photo { get; set; }
        public string Meadin { get; set; }
        public Nullable<decimal> Price { get; set; }
        public int count { get; set; }
        public bool Ok { get; set; }
    }
}