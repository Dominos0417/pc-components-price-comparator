using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjektBazyDanych
{
    public class Ceny
    {
        public int idProduktu
        { get; set; }
        public int idSklepu
        { get; set; }
        public decimal cena
        { get; set; }
        public bool dostepnosc
        { get; set; }
        public DateTime dataAktualizacji
        { get; set; } = DateTime.Today;
    }
}
