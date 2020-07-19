using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Computerladen
{
    abstract class ProduktBase
    {
        public int produktID { get; set; }
        public string typ { get; set; }
        public string name { get; set; }
        public decimal preis { get; set; }
        public double rabatt { get; set; }
        public int zeitaufwand { get; set; }
        public string beschreibung { get; set; }
    }
}
