using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Szabadbolcsesz : Munkaero
    {
        public Szabadbolcsesz(int munkaeroId, string nev, int kepzettseg) : base(munkaeroId, nev, kepzettseg)// base - ősosztály felé változók átadása
        {
            this.munkaeroNeve = "Szabadbolcsesz";
        }
    }
}
