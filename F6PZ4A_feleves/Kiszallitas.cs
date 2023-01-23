using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Kiszallitas : Feladat
    {
        public Kiszallitas(int feladatId, string feladatLeiras) : base(feladatId, feladatLeiras) // base - ősosztály felé változók átadása
        {
            this.feladatNeve = "kiszállítás";
            this.szint = 2;
        }
    }
}
