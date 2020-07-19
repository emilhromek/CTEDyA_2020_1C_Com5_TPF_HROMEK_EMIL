using System;
using System.Collections.Generic;
using System.Linq;


namespace juegoIA
{
	public abstract class Jugador
	{
        //Arbol MiniMax (solo usado por la AI)

        public ArbolGeneral<Dupla> MiniMax;

        //Arbol de jugada actual (solo usado por la AI)

        public ArbolGeneral<Dupla> jugadaActual;

        public abstract void incializar(List<int> cartasPropias, List<int> cartasOponente, int limite);
		public abstract int descartarUnaCarta();
		public abstract void cartaDelOponente(int carta);
	}
}
