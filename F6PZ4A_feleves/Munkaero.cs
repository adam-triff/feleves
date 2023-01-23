using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    // abstract osztály - nem példányosítható, mert csak összefoglalja a leszármazottak közös tulajdonságait -> leszármazottban muszáj példányosítani
    abstract class Munkaero : IMunkaero, IComparable
    {
        private int munkaeroId;
        private string nev;

        protected string munkaeroNeve;
        protected int kepzettseg;

        private LancoltLista<Feladat> feladatok;
        private int kapacitas;

        protected Munkaero(int munkaeroId, string nev, int kepzettseg)
        {
            this.munkaeroId = munkaeroId;
            this.nev = nev;
            feladatok = new LancoltLista<Feladat>();
            kapacitas = 5;
            this.kepzettseg = kepzettseg;
        }

        public string MunkaeroNeve { get { return munkaeroNeve; } set { this.munkaeroNeve = value; } }
        public int Kepzettseg { get { return kepzettseg; } set { this.kepzettseg = value; } }
        public LancoltLista<Feladat> Feladatok { get {return feladatok; } }
        public int Kapacitas { get { return kapacitas; } set { this.kapacitas = value; } }

        public int CompareTo(object obj)
        {
            IMunkaero munkaero = obj as IMunkaero;
            if (munkaero != null)
            {
                return this.kepzettseg - munkaero.Kepzettseg; // egy adott munkás képzettségét hasonlítja össze egy másikkal
            }
            throw new NotImplementedException();
        }

        // hozzárendelünk egy feladot egy munkáshoz
        public void FeladatHozzarendeles(Feladat feladat)
        {
            kapacitas--; // a munkás kapacitását csökkenjük 
            feladatok.ElemBeszuras(feladat); // a feladatok láncolt listába beszúrjuk az új feladatot
        }

        // ToString() metódus felüldefiniálása
        public override string ToString()
        {
            string text = string.Format("Munkás: {0}, képzettsége: {1}\n", munkaeroNeve, kepzettseg);

            foreach (var item in feladatok)
            {
                text += "\t-" + item.ToString() + "\n";
            }
            return text;
        }
    }
}
