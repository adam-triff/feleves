using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    // abstract osztály - nem példányosítható, mert csak összefoglalja a leszármazottak közös tulajdonságait -> leszármazottban muszáj példányosítani
    abstract class Feladat : IFeladat, IComparable // IComparable interfész -> a feladatok összehasonlíthatóak legyenek 
    {
        // private - az osztály belső változói, kívülről vagy leszármazottból nem elérhető
        
        private int feladatId; // ha egy feladattípusból több is van -> pl.: több Audit feladat -> megkülönböztetik melyik, melyik
        private string feladatLeiras;

        protected string feladatNeve;
        protected int szint;

        // Protected konstruktor - az osztály gyerekeiből elérhető lesz, kívülről nem
        protected Feladat(int feladatId, string feladatLeiras)
        {
            this.feladatId = feladatId;
            this.feladatLeiras = feladatLeiras;
        }

        // Feladat tulajdonságainak beállítása get; set;
        public string FeladatNeve
        {
            get { return this.feladatNeve; } // visszaadja az adott feladat nevét
            set { this.feladatNeve = value; } // beállítja az adott feladat nevét - value
        }
        // Adott feladat szintjének megadása
        public int Szint
        {
            get { return this.szint; }
            set { this.szint = value; }
        }

        // Feladatok összehasonlítása 
        public int CompareTo(object obj)
        {
            // a különféle feladatokat IFeladat-ként hasonlítjuk össze, mert mindegyik feladatnak ez az interfész
            // írja elő az osztállyal szemben támasztott követelményeket (tulajdonságok, szignatúra)
            
            IFeladat feladat = obj as IFeladat; 
            if (feladat != null) // ha van összehasonlítandó feladat
            {
                return this.Szint - feladat.Szint; //a nehezebb feladat fog előrébb kerülni
            }
            throw new NotImplementedException();
        }

        // ToString() metódus felüldefiniálása
        // Console.WriteLine ilyen formán fogja kiírni a konzolra
        public override string ToString()
        {
            return string.Format("Feladat: {0}, szint: {1}", feladatNeve, szint);
        }
    }
}
