using System;
using System.Collections.Generic;

namespace juegoIA
{
    public class ArbolGeneral<T>
    {

        private NodoGeneral<T> raiz;

        public ArbolGeneral(T dato)
        {
            this.raiz = new NodoGeneral<T>(dato);
        }

        private ArbolGeneral(NodoGeneral<T> nodo)
        {
            this.raiz = nodo;
        }

        private NodoGeneral<T> getRaiz()
        {
            return raiz;
        }

        public T getDatoRaiz()
        {
            return this.getRaiz().getDato();
        }

        public void setDatoRaiz(T dato)
        {
            NodoGeneral<T> aux = new NodoGeneral<T>(dato);
            this.raiz = aux;
        }

        public List<ArbolGeneral<T>> getHijos()
        {
            List<ArbolGeneral<T>> temp = new List<ArbolGeneral<T>>();
            foreach (var element in this.raiz.getHijos())
            {
                temp.Add(new ArbolGeneral<T>(element));
            }
            return temp;
        }

        public void agregarHijo(ArbolGeneral<T> hijo)
        {
            this.raiz.getHijos().Add(hijo.getRaiz());
        }

        public void eliminarHijo(ArbolGeneral<T> hijo)
        {
            this.raiz.getHijos().Remove(hijo.getRaiz());
        }

        public bool esVacio()
        {
            return this.raiz == null;
        }

        public bool esHoja()
        {
            return this.raiz != null && this.getHijos().Count == 0;
        }

        public int altura()
        {

            if (this.esHoja())
            {
                return 0;
            }
            int alturaMax = 0;
            foreach (var hijo in getHijos())
                if (hijo.altura() > alturaMax)
                    alturaMax = hijo.altura();
            return alturaMax + 1;
        }

        public bool include(T dato)
        {
            if (this.getDatoRaiz().Equals(dato))
                return true;
            return false;
        }

        public int Nivel(T dato)
        {

            if (getDatoRaiz().Equals(dato))
            {
                return 0;
            }
            foreach (var hijo in getHijos())
            {
                int nivel = hijo.Nivel(dato);
                if (nivel != -1)
                {
                    return nivel + 1;
                }
            }
            return -1;
        }

        public void PreOrden()
        {
            if (!this.esVacio())
                Console.WriteLine(this.getDatoRaiz() + " ");

            if (!this.esHoja())
                foreach (var hijo in getHijos())
                    hijo.PreOrden();
        }

        public void PostOrden()
        {
            if (!this.esVacio())

                if (!this.esHoja())
                    foreach (var hijo in this.getHijos())
                        hijo.PostOrden();

            Console.WriteLine(this.getDatoRaiz() + " ");

        }

        public void PorNiveles()
        {
            Cola<ArbolGeneral<T>> cola = new Cola<ArbolGeneral<T>>();
            cola.encolar(this);
            while (!cola.esVacia())
            {
                int n = cola.cantidad();

                while (n > 0)
                {
                    ArbolGeneral<T> p = cola.tope();

                    cola.desencolar();

                    Console.Write(p.getDatoRaiz() + " ");

                    foreach (var v in p.getHijos())
                    {
                        cola.encolar(v);
                    }

                    n--;
                }


                Console.WriteLine();
                Console.WriteLine();
            }
        }

        public void imprimirRecorridos()
        {
            List<T> caminos = new List<T>();
            this.imprimirRecorridosAux(caminos);
        }

        private void imprimirRecorridosAux(List<T> camino)
        {
            camino.Add(this.getDatoRaiz());

            if (this.getHijos().Count == 0)
            {
                foreach (var v in camino)
                    Console.Write(v + " ");
                camino.RemoveAt(camino.Count - 1);
            }

            else
            {
                foreach (var v in this.getHijos())
                    v.imprimirRecorridosAux(camino);
                camino.RemoveAt(camino.Count - 1);
            }

            Console.WriteLine();
        }

        public List<List<T>> getListaRecorridos()
        {

            List<List<T>> recorridos = new List<List<T>>();
            List<T> camino = new List<T>();
            this.getListaRecorridosAux(recorridos, camino);

            return recorridos;
        }

        private void getListaRecorridosAux(List<List<T>> recorridos, List<T> camino)
        {
            camino.Add(this.getDatoRaiz());

            if (this.getHijos().Count == 0)
            {
                List<T> nuevoCamino = new List<T>();

                foreach (var v in camino)
                    nuevoCamino.Add(v);
                recorridos.Add(nuevoCamino);
                camino.RemoveAt(camino.Count - 1);
            }

            else
            {
                foreach (var v in this.getHijos())
                    v.getListaRecorridosAux(recorridos, camino);
                camino.RemoveAt(camino.Count - 1);
            }
        }

        public void recorridoEntreNiveles(int a, int b)
        {
            if (esVacio())
            {
                return;
            }

            Cola<ArbolGeneral<T>> cola = new Cola<ArbolGeneral<T>>();
            cola.encolar(this);
            int nivel = 0;
            while (!cola.esVacia())
            {
                nivel++;

                int n = cola.cantidad();

                while (n > 0)
                {
                    ArbolGeneral<T> p = cola.tope();

                    cola.desencolar();

                    if (nivel >= a && nivel <= b)
                        Console.Write(p.getDatoRaiz() + " ");


                    if (!p.esVacio())
                    {
                        foreach (var v in p.getHijos())
                        {
                            cola.encolar(v);
                        }
                    }

                    n--;
                }

                if (nivel >= a && nivel <= b)
                    Console.WriteLine();
            }
        }
    }
}

