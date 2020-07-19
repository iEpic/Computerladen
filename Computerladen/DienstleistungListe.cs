using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Computerladen
{
    class DienstleistungListe : Liste<Dienstleistung>
    {
        
        public override List<Dienstleistung> generate()
        {
            try
            {
                List<Dienstleistung> dienstleistungen = File.ReadAllLines("../../../DienstleistungListe.csv")
                                                            .Skip(1)
                                                            .Select(line => new Dienstleistung(line))
                                                            .ToList();
                return dienstleistungen;
            } catch (FileNotFoundException e)
            {
                Console.WriteLine("Dienstleistungsliste wurde nicht gefunden!");
                Console.WriteLine(e);
                return null;
            }
        }
    }
}