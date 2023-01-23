using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class Ertekesites : Feladat
    {
        public Ertekesites(int feladatId, string feladatLeiras) : base(feladatId, feladatLeiras) // base - ősosztály felé változók átadása
        {
            this.feladatNeve = "értékesítés";
            this.szint = 5;
        }
    }
}
