using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Futar : Munkaero
    {
        public Futar(int munkaeroId, string nev, int kepzettseg) : base(munkaeroId, nev, kepzettseg) // base - ősosztály felé változók átadása
        {
            this.munkaeroNeve = "Futár";
        }
    }
}
