using System;
using System.Collections.Generic;
using System.Text;

namespace Computerladen
{
    class Computerteil : ProduktBase
    {
        public string kategorie { get; set; }
        public bool verfügbarkeit { get; set; }
        public string ean { get; set; }

        public Computerteil(string line)
        {
            string[] splittedLine = line.Split(';');
            produktID = int.Parse(splittedLine[0]);
            typ = splittedLine[1];
            name = splittedLine[2];
            kategorie = splittedLine[3];
            preis = decimal.Parse(splittedLine[4]);
            rabatt = double.Parse(splittedLine[5]);
            beschreibung = splittedLine[6];
            verfügbarkeit = bool.Parse(splittedLine[7]);
            ean = splittedLine[8];
            // Vorgabe: Zeitaufwand bei Computerteilen 0
            zeitaufwand = 0;
        }
    }
}
