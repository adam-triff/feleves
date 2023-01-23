using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Audit : Feladat
    {
        public Audit(int feladatId, string feladatLeiras) : base(feladatId, feladatLeiras) // base - ősosztály felé változók átadása
        {
            this.feladatNeve = "audit";
            this.szint = 10;
        }
    }
}
