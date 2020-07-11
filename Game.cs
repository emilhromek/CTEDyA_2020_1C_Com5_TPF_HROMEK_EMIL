
using System;
using System.Collections.Generic;
using System.Linq;

namespace juegoIA
{

	public class Game
	{
		public static int WIDTH = 12;
		public static int UPPER = 35;
		public static int LOWER = 25;
		
		private Jugador player1 = new ComputerPlayer();
		private Jugador player2 = new HumanPlayer();
		private List<int> naipesHuman = new List<int>();
		private List<int> naipesComputer = new List<int>();
		private int limite;
        private bool juegaHumano = false;

        ////Campo arbol minimax

        //private ArbolGeneral<StatusDeNodo> arbolMinimax;

        private ArbolGeneral<Tuple<int, int>> arbolMinimax;
		
		
		public Game()
		{
			var rnd = new Random();
			limite = rnd.Next(LOWER, UPPER);
			
			naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();
			
			for (int i = 1; i <= WIDTH; i++) {
				if (!naipesHuman.Contains(i)) {
					naipesComputer.Add(i);
				}
			}
			player1.incializar(naipesComputer, naipesHuman, limite);
			player2.incializar(naipesHuman, naipesComputer, limite);

            Estatus estatusInicial = new Estatus();

            estatusInicial.cartasHumano.AddRange(naipesHuman);

            estatusInicial.cartasIA.AddRange(naipesComputer);

            estatusInicial.limite = limite;

            estatusInicial.juegaHumano = juegaHumano;

            void armarArbol(Estatus estado)
            {
                ArbolGeneral<Tuple<int, int>> nuevo = new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(0, 0));

                ArbolGeneral<Tuple<int, int>> nuevaRaiz = armarArbolUtil(estado, nuevo);

                arbolMinimax = nuevaRaiz;
            }

            //ArbolGeneral<Tuple<int, int>> armarArbolUtil (Estatus estado, ArbolGeneral<Tuple<int, int>> arbol)
            //{
            //    if (estado.juegaHumano == true)
            //    {

            //        Console.WriteLine("estado juega humano = true");

            //        foreach (int carta in estado.cartasHumano)
            //        {
            //            Console.WriteLine(carta);
            //        }

            //        foreach (int carta in estado.cartasHumano.ToList())
            //        {


            //            arbol.agregarHijo(new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0)));

            //            if (estado.limite > 0)
            //            {
            //                Estatus estadoNuevo = estado;

            //                estadoNuevo.cartasHumano.Remove(carta);

            //                estadoNuevo.limite = estadoNuevo.limite - carta;

            //                estadoNuevo.juegaHumano = false;

            //                armarArbolUtil(estadoNuevo, arbol.getHijos()[0]);

            //                // asignar funcion heuristica usando minimax

            //            }

            //            else
            //            {
            //                //asignar funcion heuristica a hojas

            //            }
            //        }


            //    }

            //    //si juega la computadora

            //    else
            //    {
            //        Console.WriteLine("estado juega humano = false");

            //        foreach(int carta in estado.cartasIA)
            //        {
            //            Console.WriteLine(carta);
            //        }

            //        foreach (int carta in estado.cartasIA.ToList())
            //        {
            //            Console.WriteLine("entro foreach");

            //            arbol.agregarHijo(new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0)));

            //            Console.WriteLine("agregado hijo");

            //            if (estado.limite > 0)
            //            {
            //                Estatus estadoNuevo = estado;

            //                estadoNuevo.cartasIA.Remove(carta);

            //                estadoNuevo.limite = estadoNuevo.limite - carta;

            //                estadoNuevo.juegaHumano = true;

            //                armarArbolUtil(estadoNuevo, arbol.getHijos()[0]);

            //                // asignar funcion heuristica usando minimax

            //            }

            //            else
            //            {
            //                arbol.getHijos()[0].

            //            }
            //        }

            //    }

            //    return arbol;
            //}

            //ArbolGeneral<Tuple<int, int>> armarArbolUtil(Estatus estado, ArbolGeneral<Tuple<int, int>> arbol)
            //{
            //    if (estado.juegaHumano == true)
            //    {
            //        foreach (int carta in estado.cartasHumano)
            //        {

            //            arbol.agregarHijo(new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0)));

            //        }

            //        Estatus estadoNuevo = estado;

            //        foreach (ArbolGeneral<Tuple<int, int>> hijo in arbol.getHijos())
            //        {
            //            if (estado.limite > 0)
            //            {

            //                estadoNuevo.cartasHumano.Remove(hijo.getDatoRaiz().Item1);

            //                estadoNuevo.limite = estadoNuevo.limite - hijo.getDatoRaiz().Item1;

            //                estadoNuevo.juegaHumano = false;

            //                armarArbolUtil(estadoNuevo, hijo);

            //                // asignar funcion heuristica usando minimax

            //            }

            //            else
            //            {
            //                // asignar funcion a las hojas

            //            }
            //        }


            //    }

            //    //si juega la computadora

            //    else
            //    {
            //        foreach (int carta in estado.cartasIA)
            //        {

            //            arbol.agregarHijo(new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0)));

            //        }

            //        Estatus estadoNuevo = estado;

            //        foreach (ArbolGeneral<Tuple<int, int>> hijo in arbol.getHijos())
            //        {
            //            if (estado.limite > 0)
            //            {

            //                estadoNuevo.cartasIA.Remove(hijo.getDatoRaiz().Item1);

            //                estadoNuevo.limite = estadoNuevo.limite - hijo.getDatoRaiz().Item1;

            //                estadoNuevo.juegaHumano = true;

            //                armarArbolUtil(estadoNuevo, hijo);

            //                // asignar funcion heuristica usando minimax

            //            }

            //            else
            //            {
            //                // asignar funcion a las hojas

            //            }
            //        }



            //    }


            //    return arbol;

            //}

            //ArbolGeneral<Tuple<int, int>> armarArbolUtil(Estatus estado, ArbolGeneral<Tuple<int, int>> arbol)
            //{
            //    if (estado.juegaHumano == true)
            //    {
            //        foreach (int carta in estado.cartasHumano.ToList())
            //        {
            //            ArbolGeneral<Tuple<int, int>> arbolHijo = new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0));

            //            //arbol.agregarHijo(new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0)));

            //            if (estado.limite > 0)
            //            {

            //                Estatus estadoNuevo = estado;

            //                // tengo que referenciar eso

            //                estadoNuevo.cartasHumano.Remove(arbolHijo.getDatoRaiz().Item1);

            //                estadoNuevo.limite = estadoNuevo.limite - arbolHijo.getDatoRaiz().Item1;

            //                estadoNuevo.juegaHumano = false;

            //                armarArbolUtil(estadoNuevo, arbolHijo);

            //                // asignar funcion heuristica usando minimax

            //            }

            //            else
            //            {
            //                // asignar funcion a las hojas

            //            }

            //            arbol.agregarHijo(arbolHijo);

            //        }

            //    }

            //    //si juega la computadora

            //    else
            //    {
            //        foreach (int carta in estado.cartasIA.ToList())
            //        {
            //            ArbolGeneral<Tuple<int, int>> arbolHijo = new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0));

            //            //arbol.agregarHijo(new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0)));

            //            if (estado.limite > 0)
            //            {
            //                Estatus estadoNuevo = estado;

            //                estadoNuevo.cartasIA.Remove(arbolHijo.getDatoRaiz().Item1);

            //                estadoNuevo.limite = estadoNuevo.limite - arbolHijo.getDatoRaiz().Item1;

            //                estadoNuevo.juegaHumano = true;

            //                armarArbolUtil(estadoNuevo, arbolHijo);

            //                // asignar funcion heuristica usando minimax

            //            }

            //            else
            //            {
            //                // asignar funcion a las hojas

            //            }

            //            arbol.agregarHijo(arbolHijo);
            //        }





            //    }


            //    return arbol;

            //}

            ArbolGeneral<Tuple<int, int>> armarArbolUtil(Estatus estado, ArbolGeneral<Tuple<int, int>> arbol)
            {
                if (estado.juegaHumano == true)
                {
                    foreach (int carta in estado.cartasHumano.ToList())
                    {

                        arbol.agregarHijo(new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0)));


                        if (estado.limite - carta > 0 & estado.cartasIA.Count > 0)
                        {

                            Estatus estadoNuevo = estado;

                            estadoNuevo.cartasHumano.Remove(carta);

                            estadoNuevo.limite = estadoNuevo.limite - carta;

                            estadoNuevo.juegaHumano = false;

                            armarArbolUtil(estadoNuevo, arbol.getHijos()[arbol.getHijos().Count - 1]);

                            // asignar funcion heuristica usando minimax

                        }

                        else
                        {
                            // asignar funcion a las hojas

                        }


                    }

                }

                //si juega la computadora

                else
                {
                    foreach (int carta in estado.cartasIA.ToList())
                    {

                        arbol.agregarHijo(new ArbolGeneral<Tuple<int, int>>(new Tuple<int, int>(carta, 0)));


                        if (estado.limite - carta > 0 & estado.cartasHumano.Count > 0)
                        {

                            Estatus estadoNuevo = estado;

                            estadoNuevo.cartasIA.Remove(carta);

                            estadoNuevo.limite = estadoNuevo.limite - carta;

                            estadoNuevo.juegaHumano = true;

                            armarArbolUtil(estadoNuevo, arbol.getHijos()[arbol.getHijos().Count - 1]);

                            // asignar funcion heuristica usando minimax

                        }

                        else
                        {
                            // asignar funcion a las hojas

                        }


                    }



                }


                return arbol;

            }


            armarArbol(estatusInicial);



            arbolMinimax.PorNiveles();



            //    // Arbol miniMax

            //    StatusDeNodo statusInicial = new StatusDeNodo();

            //    statusInicial.setLimite(limite);

            //    statusInicial.setPuntosActuales(0);

            //    statusInicial.setJuegaHumano(true);

            //    statusInicial.setCartaActual(0);

            //    foreach(int carta in naipesHuman)
            //    {
            //        statusInicial.agregarCartasHumano(carta);
            //    }

            //    foreach (int carta in naipesComputer)
            //    {
            //        statusInicial.agregarCartasComputadora(carta);
            //    }

            //    statusInicial.setJuegaHumano(true);

            //    arbolMinimax = new ArbolGeneral<StatusDeNodo>(statusInicial);

            //    LlenarArbol(arbolMinimax);

            //    Console.WriteLine("Prueba de arbol:");

            //    Console.WriteLine(arbolMinimax.getDatoRaiz().getPuntosActuales());

            //    foreach(ArbolGeneral<StatusDeNodo> h in arbolMinimax.getHijos())
            //    {
            //        Console.WriteLine(h.getDatoRaiz().getCartaActual());
            //    }
            //}

            //public void LlenarArbol(ArbolGeneral<StatusDeNodo> arbol)
            //{
            //if(arbol.getDatoRaiz().getJuegaHumano() == true)
            //{
            //    foreach(int carta in arbol.getDatoRaiz().getCartasHumano())
            //    {
            //        StatusDeNodo nuevoStatus = new StatusDeNodo();

            //        foreach (int h in arbol.getDatoRaiz().getCartasHumano())
            //        {
            //            nuevoStatus.agregarCartasHumano(h);
            //        }

            //        foreach (int ia in arbol.getDatoRaiz().getCartasComputadora())
            //        {
            //            nuevoStatus.agregarCartasComputadora(ia);
            //        }

            //        nuevoStatus.setJuegaHumano(false);

            //        nuevoStatus.setPuntosActuales(arbol.getDatoRaiz().getPuntosActuales() + carta);

            //        nuevoStatus.setCartaActual(carta);

            //        nuevoStatus.removerCartaHumano(carta);

            //        nuevoStatus.setLimite(arbol.getDatoRaiz().getLimite());

            //        ArbolGeneral<StatusDeNodo> nuevoArbol = new ArbolGeneral<StatusDeNodo>(nuevoStatus);

            //        if (nuevoStatus.getPuntosActuales() >= nuevoStatus.getLimite())
            //        {
            //            return;
            //        }

            //        LlenarArbol(nuevoArbol);
            //    }
            //}

            //if (arbol.getDatoRaiz().getJuegaHumano() == false)
            //{
            //    foreach (int carta in arbol.getDatoRaiz().getCartasComputadora())
            //    {
            //        StatusDeNodo nuevoStatus = new StatusDeNodo();

            //        foreach (int h in arbol.getDatoRaiz().getCartasHumano())
            //        {
            //            nuevoStatus.agregarCartasHumano(h);
            //        }

            //        foreach (int ia in arbol.getDatoRaiz().getCartasComputadora())
            //        {
            //            nuevoStatus.agregarCartasComputadora(ia);
            //        }

            //        nuevoStatus.setJuegaHumano(true);

            //        nuevoStatus.setPuntosActuales(arbol.getDatoRaiz().getPuntosActuales() + carta);

            //        nuevoStatus.setCartaActual(carta);

            //        nuevoStatus.removerCartaComputadora(carta);

            //        nuevoStatus.setLimite(arbol.getDatoRaiz().getLimite());

            //        ArbolGeneral<StatusDeNodo> nuevoArbol = new ArbolGeneral<StatusDeNodo>(nuevoStatus);

            //        if (nuevoStatus.getPuntosActuales() >= nuevoStatus.getLimite())
            //        {
            //            return;
            //        }

            //        LlenarArbol(nuevoArbol);


            //    }
            //}


        }


        private void printScreen()
		{
			Console.WriteLine();
			Console.WriteLine("Limite:" + limite.ToString());
		}
		
		private void turn(Jugador jugador, Jugador oponente, List<int> naipes)
		{
			int carta = jugador.descartarUnaCarta();
			naipes.Remove(carta);
			limite -= carta;
			oponente.cartaDelOponente(carta);
			juegaHumano = !juegaHumano;
		}
		
		
		
		private void printWinner()
		{
			if (!juegaHumano) {
				Console.WriteLine("Gano el Ud");
			} else {
				Console.WriteLine("Gano Computer");
			}
			
		}
		
		private bool fin()
		{
			return limite < 0;
		}
		
		public void play()
		{
			while (!this.fin()) {
				this.printScreen();
				this.turn(player2, player1, naipesHuman); // Juega el usuario
				if (!this.fin()) {
					this.printScreen();
					this.turn(player1, player2, naipesComputer); // Juega la IA
				}
			}
			this.printWinner();
		}
		
		
	}
}
