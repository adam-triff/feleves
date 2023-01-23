using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Esztergalyos : Munkaero
    {
        public Esztergalyos(int munkaeroId, string nev, int kepzettseg) : base(munkaeroId, nev, kepzettseg) // base - ősosztály felé változók átadása
        {
            this.munkaeroNeve = "Esztergályos";
        }
    }
}
