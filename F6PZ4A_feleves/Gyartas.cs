using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Gyartas : Feladat
    {
        public Gyartas(int feladatId, string feladatLeiras) : base(feladatId, feladatLeiras) // base - ősosztály felé változók átadása
        {
            this.feladatNeve = "gyártás";
            this.szint = 3;
        }
    }
}
