using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Computerladen
{
    class Einkaufswagen
    {
        private readonly int einkaufswagenID;
        private List<EinkaufswagenItem> einkaufsListe;

        public Einkaufswagen()
        {
            einkaufswagenID = ++einkaufswagenID;
            einkaufsListe = new List<EinkaufswagenItem>();
        }
        
        public void updateEinkaufswagen(EinkaufswagenItem einkaufswagenItem)
        {
            // Produkt bereits im Einkaufswagen?
            if(!einkaufsListe.Any(i => i.produktID == einkaufswagenItem.produktID))
            {
                // All in One Service bestellt? --> Lösche alle anderen Services, da in dem Service bereits inkludiert
                if (einkaufswagenItem.produktID == 11)
                {
                    einkaufsListe.RemoveAll(j => j.produktID != 11 && j.typ == "Dienstleistung");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("Der All in One Service wurde ausgewählt. Die restlichen Dienstleistungen wurden wieder aus dem Einkaufswagen entfernt!");
                    Console.ResetColor();
                }

                // Prüfe vorm Hinzufügen eines Services, ob All in One Service bereits im Einkaufswagen
                if(!einkaufsListe.Any(i => i.produktID == 11))
                {
                    einkaufsListe.Add(einkaufswagenItem);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine(einkaufswagenItem.anzahl + "x" + einkaufswagenItem.name + " zum Einkaufswagen hinzugefügt!");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Sie können keine weiteren Dienstleistungen in den Warenkorb aufnehmen!");
                    Console.ResetColor();
                }
            } 
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(einkaufswagenItem.name + " bereits im Einkaufswagen vorhanden!");
                Console.ResetColor();
            }
        }

        public void checkOut()
        {
            Console.WriteLine("******************************************************Warenkorb*********************************************************");
            Console.WriteLine(String.Format("|{0,5}|{1,63}|{2,15}|", "Anzahl", "Produktbezeichnung", "Preis"));
            einkaufsListe.ForEach(i => Console.WriteLine(String.Format("|{0,6}|{1,63}|{2,10} Euro|", i.anzahl, i.name, i.preis)));
            Console.WriteLine();
            if(einkaufsListe.Any(j => j.rabatt > .00))
            {
                Console.WriteLine("Der Rabatt für das Paket wurde abgezogen :)");
            }
            Console.WriteLine("Steuern: 16%");
            // Versand wurde nicht berücksichtigt
            Console.WriteLine("Bruttopreis: " + getTotal());
        }

        private string getTotal()
        {
            decimal total = 0;
            // hardcoded tax, 16%
            decimal percentage = 1.16m;
            einkaufsListe.ForEach(i => total += decimal.Multiply(i.preis,percentage));
            return String.Format("{0:#.00} Euro", total);
        }

    }
}
