using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Sales : Munkaero
    {
        public Sales(int munkaeroId, string nev, int kepzettseg) : base(munkaeroId, nev, kepzettseg) // base - ősosztály felé változók átadása
        {
            this.munkaeroNeve = "Sales";
        }
    }
}
