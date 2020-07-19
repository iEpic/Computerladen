using System;
using System.Collections.Generic;
using System.Text;

namespace Computerladen
{
    class Kunde
    {
        public int id;
        public string vorname;
        public string nachname;
        public string email;
        public string geschlecht;
        public string stadt;
        public string plz;
        public string straße;
        public string hausnummer;
        public string tel;
        public decimal guthaben;
        public DateTime registriert;

        public Kunde(int id, string vorname, string nachname, string email, string geschlecht, string stadt, string plz, string straße, string hausnummer, string tel, decimal guthaben, DateTime registriert)
        {
            this.id = id;
            this.vorname = vorname;
            this.nachname = nachname;
            this.email = email;
            this.geschlecht = geschlecht;
            this.stadt = stadt;
            this.plz = plz;
            this.straße = straße;
            this.hausnummer = hausnummer;
            this.tel = tel;
            this.guthaben = guthaben;
            this.registriert = registriert;
        }

        public string fullName
        {
            get { return vorname + " " + nachname; }
        }


    }
}
