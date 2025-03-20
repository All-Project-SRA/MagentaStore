using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Magenta_S.Models
{
    public class Bay_Product
    {
        public int Idu { get; set; }
        public int Idproduct { get; set; }
        public Nullable<int> bill { get; set; }
        public int Count { get; set; }
        public bool Ok { get; set; }
        public string ColorP { get; set; }
        public string Sizee { get; set; }

        public virtual Bill Bill1 { get; set; }
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }

        public int idseesion { get; set; }

        public List<Buy> GetCart {
            get
            {
                magentadb mg = new magentadb();
                return mg.Buys.Where(x => x.Idu == Idu&&x.Ok==false).ToList();
            }
            set { }

        }


    }
}