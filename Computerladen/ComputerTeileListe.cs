using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Computerladen
{
    class ComputerTeileListe : Liste<Computerteil>
    {
        public override List<Computerteil> generate()
        {
            try
            {
                List<Computerteil> computerteile = File.ReadAllLines("../../../ComputerTeileListe.csv")
                                                            .Skip(1)
                                                            .Select(line => new Computerteil(line))
                                                            .ToList();
                return computerteile;
            }
            // Sicherlich könnte man an der Stelle noch mehr Error Handling betreiben, das gleiche siehe DienstleistungListe.cs
            catch (FileNotFoundException e)
            {
                Console.WriteLine("ComputerTeileListe wurde nicht gefunden!");
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
