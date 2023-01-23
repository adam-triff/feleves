using System;
using System.Collections.Generic;
using System.Text;

namespace F6PZ4A_feleves
{
    class ListaBejaro<T> : IEnumerator<T> where T : IComparable
    {
        ListaElem<T> fej;
        ListaElem<T> aktualis;

        public ListaBejaro(ListaElem<T> fej)
        {
            this.fej = fej;
            this.aktualis = new ListaElem<T>();
            this.aktualis.Kovetkezo = fej;
        }

        public object Current
        {
            get
            {
                return aktualis.Tartalom;
            }
        }

        T IEnumerator<T>.Current
        {
            get
            {
                return aktualis.Tartalom;
            }
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (aktualis == null)
            {
                return false;
            }
            aktualis = aktualis.Kovetkezo;
            return aktualis != null;
        }

        public void Reset()
        {
            aktualis = new ListaElem<T>();
            aktualis.Kovetkezo = fej;
        }
    }
}