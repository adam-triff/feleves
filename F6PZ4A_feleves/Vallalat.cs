using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace F6PZ4A_feleves
{
    class Vallalat
    {
        private LancoltLista<Feladat> elvegzendoFeladatok;
        private LancoltLista<Munkaero> munkatarsak;
        private int minSzint;

        // Eseménykezelés
        public delegate void KiosztasHandler(LancoltLista<Feladat> megmaradtFeladatok, LancoltLista<Munkaero> megmaradtMunkaero);
        public event KiosztasHandler KiosztasTortent;

        public Vallalat()
        {
            elvegzendoFeladatok = new LancoltLista<Feladat>();
            munkatarsak = new LancoltLista<Munkaero>();
            minSzint = int.MaxValue;
        }

        public void FajlBeolvas(string feladatokFajlNev, string munkasokFajlNev) // adatok beolvasása fájlból
        {
            StreamReader sr = new StreamReader(feladatokFajlNev);

            // Feladatok beolvasása fájlból
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                sor.ToLower();
                string[] darabolva = sor.Split(',');

                // Feladatok: Audit, Értékesítés, Ügyfélszolgálat, Gyártás, Kiszállítás
                if (darabolva[0] == "audit")
                {
                    this.FeladatHozzaadasa(new Audit(int.Parse(darabolva[1]), darabolva[2]));
                }
                if (darabolva[0] == "ertekesites")
                {
                    this.FeladatHozzaadasa(new Ertekesites(int.Parse(darabolva[1]), darabolva[2]));
                }
                if (darabolva[0] == "ugyfelszolgalat")
                {
                    this.FeladatHozzaadasa(new Ugyfelszolgalat(int.Parse(darabolva[1]), darabolva[2]));
                }
                if (darabolva[0] == "gyartas")
                {
                    this.FeladatHozzaadasa(new Gyartas(int.Parse(darabolva[1]), darabolva[2]));
                }
                if (darabolva[0] == "kiszallitas")
                {
                    this.FeladatHozzaadasa(new Kiszallitas(int.Parse(darabolva[1]), darabolva[2]));
                }          
            }

            sr.Close();

            sr = new StreamReader(munkasokFajlNev);

            // Munkaerő beolvasása fájlból
            while (!sr.EndOfStream)
            {
                string sor = sr.ReadLine();
                sor.ToLower();
                string[] darabolva = sor.Split(',');

                // Munkások: Auditor, Sales, Szabadbölcsész, Esztergályos, Futár
                try
                {
                    this.MunkaeroHozzaadasa(darabolva[0], int.Parse(darabolva[1]), darabolva[2], int.Parse(darabolva[3]));
                }
                catch (KepzettlenMunkaeroException) // elkapja a KepzettlenMunkaeroException-t
                {
                    // kepzettseg -> minSzint-re (minimum elvárás) módosítja a fájlból beolvasott eredeti szintet
                    this.MunkaeroHozzaadasa(darabolva[0], int.Parse(darabolva[1]), darabolva[2], minSzint); 
                }
            }

            sr.Close();
        }

        public void FeladatHozzaadasa(Feladat feladat) // hozzáad egy új feladatot
        {
            elvegzendoFeladatok.ElemBeszuras(feladat); // ElemBeszuras - rendezetten adja hozzá
            if (feladat.Szint < minSzint)
            {
                minSzint = feladat.Szint;
            }
        }

        public void MunkaeroHozzaadasa(string munkaeroTipus, int munkaeroId, string nev, int kepzettseg) // hozzáad egy új munkaerőt
        {
            Munkaero munkaero;

            if (munkaeroTipus == "auditor")
            {
                munkaero = new Auditor(munkaeroId, nev, kepzettseg);
            }
            else if (munkaeroTipus == "sales")
            {
                munkaero = new Sales(munkaeroId, nev, kepzettseg);
            }
            else if (munkaeroTipus == "szabadbolcsesz")
            {
                munkaero = new Szabadbolcsesz(munkaeroId, nev, kepzettseg);
            }
            else if (munkaeroTipus == "esztergalyos")
            {
                munkaero = new Esztergalyos(munkaeroId, nev, kepzettseg);
            }
            else
            {
                munkaero = new Futar(munkaeroId, nev, kepzettseg);
            }

            if (munkaero.Kepzettseg >= minSzint) // ha a munkás képzettsége meglelelő 
            {
                munkatarsak.ElemBeszuras(munkaero); // ElemBeszuras - rendezetten adja hozzá
            }
            else
            {
                throw new KepzettlenMunkaeroException(); // ha nem elég képzett
            }
        }

        public void FeladatokKiosztasa()
        {

            // A lista csökkenő sorrendbe van rendezve
            // pl.: Feladatok nehézsége - 6, 4, 3... 
            //      Munkások képzettsége - 6, 4, 3... 

            ListaElem<Feladat> aktualisFeladat = elvegzendoFeladatok.fej; // listában első feladattal kezdünk
            Munkaero talalat;

            while (aktualisFeladat != null && (talalat = FeladatKiosztasa(aktualisFeladat.Tartalom)) != null) // addig megyünk míg a feladat végére nem érünk/elfogytak a munkások
            {
                talalat.FeladatHozzarendeles(aktualisFeladat.Tartalom);

                elvegzendoFeladatok.Torles(aktualisFeladat.Tartalom); 
                aktualisFeladat = aktualisFeladat.Kovetkezo; // a következő feladatra lépünk és azt vizsgáljuk tovább -> idővel feltöltődnek a listák
            }

            KiosztasTortent?.Invoke(elvegzendoFeladatok, munkatarsak); // Esemény elsütése 
        }

        private Munkaero FeladatKiosztasa(Feladat feladat) // van egy adott feladat
        {
            ListaElem<Munkaero> m = munkatarsak.fej;

            // Ha a feladat nagyon nehéz akkor már a lista elején lévő legképzetebb elem sem tudja elvégezni, ekkor vissza null
            if (m.Tartalom.Kepzettseg < feladat.Szint)
            {
                return null;
            }

            // Különben
            // Addig megyünk amíg nem találjuk meg azt a lista elemet aminek a .Kovetkezo-je már nem tudná elvégezni a feladatot
            while (m.Kovetkezo != null && m.Kovetkezo.Tartalom.Kepzettseg >= feladat.Szint)
            {
                m = m.Kovetkezo;
            }

            // Ekkor az m az a munkás ami még épphogy el tudja végezni a feladatot
            // Most meg kell találni a legelső ilyen képzettségű munkást akinek még van szaabad kapacitása
            return SzabadAdottKepzettseguMunkaeroElsoElofordulasa(m.Tartalom.Kepzettseg);
        }

        private Munkaero SzabadAdottKepzettseguMunkaeroElsoElofordulasa(int kepzettseg)
        {
            ListaElem<Munkaero> m = munkatarsak.fej; // kezdetben "legképzettebb" munkás

            // Ha túl nagy a képzettség elvárás vissza null
            if (m.Tartalom.Kepzettseg < kepzettseg)
            {
                return null;
            }

            while (m.Tartalom.Kepzettseg > kepzettseg)
            {
                m = m.Kovetkezo;
            }

            // Megvan az adott kepzettségszintű munkáscsoport első eleme
            // Ezekből kell a legelső akinek van szabad kapacitása
            while (m.Tartalom.Kepzettseg >= kepzettseg && m.Tartalom.Kapacitas == 0)
            {
                m = m.Kovetkezo;
            }

            // Ha van ilyen elem, akkor vissza azt
            if (m.Tartalom.Kepzettseg >= kepzettseg)
            {
                return m.Tartalom;
            }
            // Ha nincs, akkor az egyel magasabb képzettségcsoportba tartozó elemek első előfordulását adjuk vissza
            else
            {
                return SzabadAdottKepzettseguMunkaeroElsoElofordulasa(++kepzettseg);
            }
        }
    }
}
