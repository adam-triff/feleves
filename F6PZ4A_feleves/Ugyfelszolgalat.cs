using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Ugyfelszolgalat : Feladat
    {
        public Ugyfelszolgalat(int feladatId, string feladatLeiras) : base(feladatId, feladatLeiras) // base - ősosztály felé változók átadása
        {
            this.feladatNeve = "ügyfélszolgálat";
            this.szint = 5;
        }
    }
}
