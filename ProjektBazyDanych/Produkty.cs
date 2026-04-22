using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBazyDanych
{
    public class Produkty
    {
        public int idProduktu 
        { get;
            set;
        }
        public int idKategorii
        { get;
            set;
        }
        public string nazwa
        {
            get;
            set;
        }
        public string opis
        { get; set; }

    }
}
