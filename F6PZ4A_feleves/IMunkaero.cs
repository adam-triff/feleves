using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    interface IMunkaero
    {
        public string MunkaeroNeve { get; set; }
        public int Kepzettseg { get; set; }
        public LancoltLista<Feladat> Feladatok { get; } // az adott munkás feladatai itt vannak eltárolva

        // Kapacitas - egy munkás csak 5 feladatot tud elvégezni
        // nyílvántartjuk, hogy hány feladata van jelenleg
        public int Kapacitas { get; set; } 
        public void FeladatHozzarendeles(Feladat feladat);
    }
}
