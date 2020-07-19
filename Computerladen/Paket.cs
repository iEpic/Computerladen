using System;
using System.Collections.Generic;
using System.Text;

namespace Computerladen
{
    class Paket : ProduktBase
    {
        public Paket(string name)
        {
            produktID = ++produktID;
            this.name = name;
            // Vorgabe 10%
            rabatt = 0.10;
        }

        public void add(decimal preis, int zeitaufwand)
        {
            this.preis += preis;
            this.zeitaufwand += zeitaufwand;
        }
    }
}
