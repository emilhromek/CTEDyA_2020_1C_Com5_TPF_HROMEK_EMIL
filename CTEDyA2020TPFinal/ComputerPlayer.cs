
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{
	public class ComputerPlayer: Jugador
	{

        private List<int> naipes = new List<int>();
        private List<int> naipesHuman = new List<int>();
        private int limite;

        //Estado inicial

        private Estatus estadoInicial = new Estatus();

        public ComputerPlayer()
		{
		}
		
		public override void  incializar(List<int> cartasPropias, List<int> cartasOponente, int limite)
		{
            this.naipes = cartasPropias;
            this.naipesHuman = cartasOponente;
            this.limite = limite;

            //Inicializar el estadoInicial

            estadoInicial.setCartasHumano(cartasOponente);

            estadoInicial.setCartasIA(cartasPropias);

            estadoInicial.setLimite(limite);

            estadoInicial.setJuegaHumanoTrue();

            //Armar arbol MiniMax

            MiniMax = ArmarArbolMinimax(estadoInicial);

            //Copiar el arbol MiniMax

            jugadaActual = MiniMax;
        }

        public override int descartarUnaCarta()
		{
            //Mostrar cartas disponibles de la AI

            int carta = 0;
            Console.WriteLine("Naipes disponibles (AI): ");
            for (int i = 0; i < naipes.Count; i++)
            {
                Console.Write(naipes[i].ToString());
                if (i < naipes.Count - 1)
                {
                    Console.Write(", ");
                }
            }

            //Elegir la primera carta con funcion heuristica 1

            foreach (ArbolGeneral<Dupla> h in jugadaActual.getHijos())
            {
                if (h.getDatoRaiz().getValorFuncionHeuristica() == 1)
                {
                    jugadaActual = h;

                    carta = h.getDatoRaiz().getCarta();

                    Console.WriteLine();

                    Console.WriteLine("La IA jugo la carta " + carta);

                    Console.WriteLine();

                    return carta;
                }
            }

            //Si no hay ninguna carta con funcion = 1, elegir la primera que tenga -1

            Console.WriteLine();

            Console.WriteLine("La AI jugo la carta " + jugadaActual.getHijos()[0].getDatoRaiz().getCarta());

            Console.WriteLine();

            //Actualizar jugadaActual

            jugadaActual = jugadaActual.getHijos()[0];

            return jugadaActual.getDatoRaiz().getCarta();
        }

        //Recibir la carta del humano y actualizar jugadaActual

        public override void cartaDelOponente(int carta)
		{
            foreach(ArbolGeneral<Dupla> h in jugadaActual.getHijos())
            {
                if (h.getDatoRaiz().getCarta() == carta)
                {
                    jugadaActual = h;
                }
            }

        }

        //Funcion para armar el arbol MiniMax, a partir del estadoInicial

        private ArbolGeneral<Dupla> ArmarArbolMinimax (Estatus estado)
        {
            ArbolGeneral<Dupla> nuevo = new ArbolGeneral<Dupla>(new Dupla(0, 0));

            ArmarArbolMinimaxAux(estado, nuevo);

            return nuevo;
        }

        private void ArmarArbolMinimaxAux(Estatus estado, ArbolGeneral<Dupla> raiz)
        {
            if (estado.returnJuegaHumano() == true)
            {
                foreach (int carta in estado.getCartasHumano())
                {
                    ArbolGeneral<Dupla> h = new ArbolGeneral<Dupla>(new Dupla(carta, 0));

                    raiz.agregarHijo(h);

                    if (estado.getLimite() - carta >= 0 && estado.getCartasIA().Count > 0)
                    {
                        List<int> nuevasCartaH = new List<int>();

                        nuevasCartaH.AddRange(estado.getCartasHumano());

                        nuevasCartaH.Remove(carta);

                        Estatus estadoNuevo = new Estatus();

                        estadoNuevo.setCartasHumano(nuevasCartaH);

                        estadoNuevo.setCartasIA(estado.getCartasIA());

                        estadoNuevo.setLimite(estado.getLimite() - carta);

                        estadoNuevo.setJuegaHumanoFalse();

                        ArmarArbolMinimaxAux(estadoNuevo, h);

                        bool existe = false;

                        foreach (ArbolGeneral<Dupla> hijo in h.getHijos())
                        {
                            if (hijo.getDatoRaiz().getValorFuncionHeuristica() == 1)
                            {
                                existe = true;
                            }

                        }

                        if (existe == true)
                        {
                            h.getDatoRaiz().setFuncionHeuristica(1);
                        }
                        else
                        {
                            h.getDatoRaiz().setFuncionHeuristica(-1);
                        }

                    }

                    else
                    {
                        h.getDatoRaiz().setFuncionHeuristica(1);
                    }


                }

            }

            else
            {
                foreach (int carta in estado.getCartasIA())
                {
                    ArbolGeneral<Dupla> h = new ArbolGeneral<Dupla>(new Dupla(carta, 0));

                    raiz.agregarHijo(h);

                    if (estado.getLimite() - carta >= 0 && estado.getCartasHumano().Count > 0)
                    {
                        List<int> nuevasCartaIA = new List<int>();

                        nuevasCartaIA.AddRange(estado.getCartasIA());

                        nuevasCartaIA.Remove(carta);

                        Estatus estadoNuevo = new Estatus();

                        estadoNuevo.setCartasIA(nuevasCartaIA);

                        estadoNuevo.setCartasHumano(estado.getCartasHumano());

                        estadoNuevo.setLimite(estado.getLimite() - carta);

                        estadoNuevo.setJuegaHumanoTrue();

                        ArmarArbolMinimaxAux(estadoNuevo, h);

                        bool existe = false;

                        foreach (ArbolGeneral<Dupla> hijo in h.getHijos())
                        {
                            if (hijo.getDatoRaiz().getValorFuncionHeuristica() == -1)
                            {
                                existe = true;
                            }


                        }

                        if (existe == true)
                        {
                            h.getDatoRaiz().setFuncionHeuristica(-1);
                        }
                        else
                        {
                            h.getDatoRaiz().setFuncionHeuristica(1);
                        }


                    }

                    else
                    {
                        h.getDatoRaiz().setFuncionHeuristica(-1);
                    }

                }

            }
        }


    }
}
