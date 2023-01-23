using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    // Listaelem létrehozása
    class ListaElem<T> where T : IComparable
    {
        public T Tartalom { get; set; }
        public ListaElem<T> Kovetkezo { get; set; }
    }

    delegate void BejaroHandler<T>(T tartalom);

    class LancoltLista<T> : IEnumerable<T> where T : IComparable // T helyére behelyettesíthető -> Feladat, Munkaerő (mindkettő IComparable)
    {

        public ListaElem<T> fej;
        //public ListaElem<T> utso;

        public void ElemBeszuras(T tartalom)
        {
            ListaElem<T> p = fej;
            ListaElem<T> e = null;

            // feladat nehézsége szerint rendezetten szúrunk be a listába
            while (p != null && p.Tartalom.CompareTo(tartalom) > 0) // megkeresem a Tartalom helyét a listában - pl.: 5-ös nehézségű a 4-es alá, de 6-os fölé kerüljön
            {
                e = p;
                p = p.Kovetkezo;
            }

            ListaElem<T> uj = new ListaElem<T>();
            uj.Tartalom = tartalom;

            // Ha az új elem a "legnagyobb"
            if (e == null)
            {           
                uj.Kovetkezo = fej;
                fej = uj;
            }
            else
            {
                e.Kovetkezo = uj;
                uj.Kovetkezo = p;
            }

            if (p == null)
            {
                //utso = uj;
            }

        }

        public void Bejaras(BejaroHandler<T> metodus)
        {
            BejaroHandler<T> _metodus = metodus;
            ListaElem<T> p = fej;
            while (p != null)
            {
                _metodus?.Invoke(p.Tartalom);
                p = p.Kovetkezo;
            }
        }

        public void Torles(T torlendo)
        {
            ListaElem<T> e = null;
            ListaElem<T> p = fej;


            while (p != null && !p.Tartalom.Equals(torlendo))
            {
                e = p;
                p = p.Kovetkezo;
            }
            if (p != null)
            {
                // utsó a törlendő
                if (p.Kovetkezo == null)
                {
                    //utso = e;
                }

                //törlés, mert megvan
                if (e == null)
                {
                    //első elemet kell törölni
                    fej = p.Kovetkezo;
                }
                else
                {
                    //valahanyadik elemet kell törölni
                    e.Kovetkezo = p.Kovetkezo;
                }
            }
            else
            {
                //kivételt dobunk, mert nincs ilyen elem a listában
                throw new Exception();
            }

        }

        //Láncolt lista bejárhatóvá tevése
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return new ListaBejaro<T>(fej);
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return new ListaBejaro<T>(fej);
        }
    }
}