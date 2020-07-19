using System;
using System.Collections.Generic;
using System.Text;

namespace Computerladen
{
    class Dienstleistung : ProduktBase
    {
        // Vorgabe: Stundensatz 40€
        private const int stundensatz = 40;

        public Dienstleistung(string line)
        {
            string[] splittedLine = line.Split(';');
            produktID = int.Parse(splittedLine[0]);
            typ = splittedLine[1];
            name = splittedLine[2];
            rabatt = double.Parse(splittedLine[3]);
            beschreibung = splittedLine[4];
            zeitaufwand = int.Parse(splittedLine[5]);
            preis = zeitaufwand * stundensatz;
        }
    }
}
