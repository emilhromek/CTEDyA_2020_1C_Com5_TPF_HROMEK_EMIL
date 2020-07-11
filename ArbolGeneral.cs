using System;
using System.Collections.Generic;

namespace juegoIA
{
    public class ArbolGeneral<T>
	{
		
		private NodoGeneral<T> raiz;

		public ArbolGeneral(T dato) {
			this.raiz = new NodoGeneral<T>(dato);
		}
	
		private ArbolGeneral(NodoGeneral<T> nodo) {
			this.raiz = nodo;
		}
	
		private NodoGeneral<T> getRaiz() {
			return raiz;
		}
	
		public T getDatoRaiz() {
			return this.getRaiz().getDato();
		}
	
		public List<ArbolGeneral<T>> getHijos() {
			List<ArbolGeneral<T>> temp= new List<ArbolGeneral<T>>();
			foreach (var element in this.raiz.getHijos()) {
				temp.Add(new ArbolGeneral<T>(element));
			}
			return temp;
		}
	
		public void agregarHijo(ArbolGeneral<T> hijo) {
			this.raiz.getHijos().Add(hijo.getRaiz());
		}
	
		public void eliminarHijo(ArbolGeneral<T> hijo) {
			this.raiz.getHijos().Remove(hijo.getRaiz());
		}
	
		public bool esVacio() {
			return this.raiz == null;
		}
	
		public bool esHoja() {
			return this.raiz != null && this.getHijos().Count == 0;
		}
	
		public int altura() {

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

        public bool include (T dato)
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
                var v = cola.desencolar();
                Console.WriteLine(v.getDatoRaiz());
                foreach (var hijo in (v as ArbolGeneral<T>).getHijos())
                    cola.encolar((hijo as ArbolGeneral<T>));
            }
        }


    }
}
