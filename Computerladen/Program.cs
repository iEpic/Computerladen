using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Linq;

namespace Computerladen
{
    class Program
    {
        private static Random _random = new Random();
        
        static void Main(string[] args)
        {
            initApp();
            Console.WriteLine("Klicke irgendeine Taste, um das Programm zu beenden!");
            Console.ReadKey();
        }

        private static void initApp()
        {
            // hardcoded customer
            Kunde k1 = new Kunde(1, "Max", "Mustermann", "max@mustermann.de", "m", "Frankfurt", "65929", "Leibnizstraße", "21", "+4915784137979", Convert.ToDecimal(590.85), new DateTime().Date);

            // generiere Listen
            List<Dienstleistung> dListe = new DienstleistungListe().generate();
            List<Computerteil> ctListe = new ComputerTeileListe().generate();

            // generiere ein All-In-One-Paket: funktionierender PC + All-In-One Service (ID: 11)
            Paket paket = new Paket("All-In-One Special");
            ctListe.ForEach(i => paket.add(i.preis, i.zeitaufwand));
            Dienstleistung superService = dListe.Find(j => j.produktID == 11);
            paket.add(superService.preis, superService.zeitaufwand);

            // generiere shopping chart
            Einkaufswagen einkaufswagen = new Einkaufswagen();

            // print welcome msg
            Console.WriteLine("************************************************************************************************************************");
            Console.WriteLine("Hallo " + k1.fullName + " und willkommen im Computerladen von RGN!");
            Console.WriteLine();
            Console.WriteLine("In unserem Shop findest du eine Vielzahl von Computerteilen und Dienstleistungen zu Spottpreisen!");
            Console.WriteLine("Sichere dir Rabatte von 10% beim Kauf einer unserer heissbegehrten Pakete.");
            Console.WriteLine("************************************************************************************************************************");
            Console.WriteLine();

            // print computer parts
            Console.WriteLine("******************************************************Computerteile*****************************************************");
            Console.WriteLine();
            Console.WriteLine(String.Format("|{0,5}|{1,63}|{2,15}|{3,10}|{4,10}|", "ID", "Produktbezeichnung", "Preis", "Aufwand", "Verfügbar"));
            ctListe.ForEach(i => Console.WriteLine(
                String.Format("|{0,5}|{1,63}|{2,10} Euro|{3,8} h|{4,10}|", i.produktID, i.name, i.preis, i.zeitaufwand, i.verfügbarkeit == true ? "ja" : "nein")));

            // print services
            Console.WriteLine("******************************************************Dienstleistungen**************************************************");
            Console.WriteLine();
            Console.WriteLine(String.Format("|{0,5}|{1,63}|{2,15}|{3,10}|", "ID", "Produktbezeichnung", "Preis", "Aufwand"));
            dListe.ForEach(i => Console.WriteLine(
                String.Format("|{0,5}|{1,63}|{2,10} Euro|{3,8} h|", i.produktID, i.name, i.preis, i.zeitaufwand)));

            // print package(s)
            Console.WriteLine("***********************************************************Pakete*******************************************************");
            Console.WriteLine();
            Console.WriteLine(String.Format("|{0,5}|{1,63}|{2,15}|{3,10}|{4,10}|", "ID", "Produktbezeichnung", "Preis", "Aufwand", "Rabatt"));
            Console.WriteLine(String.Format("|{0,5}|{1,63}|{2,10} Euro|{3,8} h|{4,10:-0%}|", paket.produktID, paket.name, paket.preis, paket.zeitaufwand, paket.rabatt));

            // print and start Simulation
            Console.WriteLine("*******************************************************Kaufsimulation***************************************************");
            starteUserDialog(dListe, ctListe, paket, einkaufswagen);
        }

        // Da keine Konsoleneingaben gefordert sind, werden die Konsoleneingaben per Zufall durch generateRandomNumber() simuliert
        // Annahme: Es handelt sich immer um eine Session d.h. pro Session 1 Bestellung. Aus diesem Grund kann pro Bestellung nur jeweils eins der Services ausgewählt werden ODER 
        // der allumfängliche Montage-, und Funktionstest-Service, der alle angebotenen Services beinhaltet und die Auswahl von weiteren Services obselet macht.
        // Annahme2: Bei Computerteilen besteht diese Beschränkung nicht. Der User kann z.B. pro Bestellung 5x die gleiche Grafikkarte kaufen.
        private static void starteUserDialog(List<Dienstleistung> dListe, List<Computerteil> ctListe, Paket paket, Einkaufswagen einkaufswagen)
        {
            // in 33% der Fälle wählt der User einfach ein Paket aus, weil der Rabatt ihn überzeugt hat
            int auswahlPaket = generateRandomNumber(0, 3);

            if (auswahlPaket != 1)
            {
                // Wie lange soll der Dialog laufen? Hier: 12x
                for (int i = 0; i < 12; i++)
                {
                    // Simulation: Wahl Computerteil oder Service per Zufall
                    int randomProduktID = generateRandomNumber(1, dListe.Count + ctListe.Count);
                    // Produkt Computerteil?
                    if (randomProduktID <= ctListe.Count)
                    {
                        Computerteil computerteil = ctListe.Find(i => i.produktID == randomProduktID);
                        // Anzahl pro Computerteil pro Bestellung? hier: max 3x 
                        int anzahl = generateRandomNumber(1, 4);
                        EinkaufswagenItem item = new EinkaufswagenItem(computerteil.produktID, computerteil.name, computerteil.preis, anzahl, computerteil.rabatt, computerteil.typ);
                        einkaufswagen.updateEinkaufswagen(item);
                    }
                    // Ansonsten ist es ein Service
                    else
                    {
                        // Jeder Service kann pro Bestellung nur 1x bestellt werden
                        int anzahl = 1;
                        Dienstleistung dienstleistung = dListe.Find(i => i.produktID == randomProduktID);
                        EinkaufswagenItem item = new EinkaufswagenItem(dienstleistung.produktID, dienstleistung.name, dienstleistung.preis, anzahl, dienstleistung.rabatt, dienstleistung.typ);
                        einkaufswagen.updateEinkaufswagen(item);
                    }
                }
            }
            // User hat Paket ausgewählt, update Einkaufswagen und beende Dialog
            else
            {
                int anzahl = 1;
                EinkaufswagenItem item = new EinkaufswagenItem(paket.produktID, paket.name, paket.preis, anzahl, paket.rabatt);
                einkaufswagen.updateEinkaufswagen(item);
            }
                // print Warenkorb
                Console.WriteLine();
                einkaufswagen.checkOut();
                Console.WriteLine();
        }
        
        private static int generateRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
