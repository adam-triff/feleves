using System;

namespace F6PZ4A_feleves
{
    class Program
    {
        static void Main(string[] args)
        {
            Vallalat peldaVallalat = new Vallalat();
            peldaVallalat.FajlBeolvas("feladatok.txt", "munkasok.txt");
            peldaVallalat.KiosztasTortent += Kiir; // Eseményhez metódus hozzárendelése

            peldaVallalat.FeladatokKiosztasa();   
        }

        // Eseménykezelés "outputja"
        private static void Kiir(LancoltLista<Feladat> megmaradtFeladatok, LancoltLista<Munkaero> megmaradtMunkaero)
        {
            Console.WriteLine("Megmaradt feladatok:");
            foreach (Feladat feladat in megmaradtFeladatok)
            {
                Console.WriteLine(feladat);
            }

            Console.WriteLine("Szabad munkaerő:");
            foreach (Munkaero munkaero in megmaradtMunkaero)
            {
                if (munkaero.Kapacitas > 0)
                {
                    Console.WriteLine(munkaero);
                    Console.WriteLine();
                }
            }
        }
    }
}
