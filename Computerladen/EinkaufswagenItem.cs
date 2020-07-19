using System;
using System.Collections.Generic;
using System.Text;

namespace Computerladen
{
    class EinkaufswagenItem : ProduktBase
    {
        public int anzahl;

        public EinkaufswagenItem(int produktID, string name, decimal preis, int anzahl, double rabatt, string typ = "")
        {
            this.produktID = produktID;
            this.name = name;
            if(rabatt > .00)
            {
                this.preis = preis * anzahl - (Convert.ToDecimal(rabatt) * preis);
            } 
            else
            {
                this.preis = preis * anzahl;
            }
            this.anzahl = anzahl;
            this.typ = typ;
            this.rabatt = rabatt;
        }
    }
}
