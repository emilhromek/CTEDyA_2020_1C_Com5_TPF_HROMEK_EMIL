
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


        public Game()
        {
            var rnd = new Random();
            limite = rnd.Next(LOWER, UPPER);
            naipesHuman = Enumerable.Range(1, WIDTH).OrderBy(x => rnd.Next()).Take(WIDTH / 2).ToList();

            for (int i = 1; i <= WIDTH; i++)
            {
                if (!naipesHuman.Contains(i))
                {
                    naipesComputer.Add(i);
                }
            }

            player1.incializar(naipesComputer, naipesHuman, limite);
            player2.incializar(naipesHuman, naipesComputer, limite);

        }


        private void printScreen()
        {
            Console.WriteLine();
            Console.WriteLine("Limite: " + limite.ToString());
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
            if (!juegaHumano)
            {
                Console.WriteLine("Gano Ud");
            }
            else
            {
                Console.WriteLine("Gano Computer");
            }

        }

        private bool fin()
        {
            return limite < 0;
        }

        public void play()
        {
            while (!this.fin())
            {
                int n = 0;
                do
                {
                    Console.WriteLine("Elija una opcion: ");
                    Console.WriteLine("1: Comenzar nuevo juego");
                    Console.WriteLine("2: Desde este punto, imprimir todos los resultados posibles");
                    Console.WriteLine("3: Dado un conjunto de jugadas, imprimir todos los resultados posibles");
                    Console.WriteLine("4: Dada una profundidad, imprimir las jugadas a dicha profundidad");
                    Console.WriteLine("5: Jugar");
                    Console.WriteLine();

                    try
                    {
                        n = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                    }
                    catch (FormatException)
                    {
                        n = 0;
                    }
                    switch (n)
                    {
                        case 1:
                            Game game = new Game();
                            game.play();
                            break;
                        case 2:
                            Consulta1();
                            break;
                        case 3:
                            Consulta2();
                            break;
                        case 4:
                            Consulta3();
                            break;
                        default:
                            break;
                    }

                } while (n != 5);

                this.printScreen();
                this.turn(player2, player1, naipesHuman); // Juega el usuario
                if (!this.fin())
                {
                    this.printScreen();
                    this.turn(player1, player2, naipesComputer); // Juega la IA
                }

            }
            this.printWinner();

            return;
        }

        private void Consulta2()
        {
            List<int> cartasDisponibles = new List<int>();

            cartasDisponibles.AddRange(naipesHuman);

            List<int> cartasAMostrar = new List<int>();

            int carta = -1;

            do
            {
                Console.WriteLine("Ingrese las posibles cartas a jugar o escriba 0 para terminar:");
                Console.WriteLine();
                foreach (int c in cartasDisponibles)
                {
                    Console.WriteLine(c);
                }
                Console.WriteLine();
                foreach (int c in cartasAMostrar)
                {
                    Console.WriteLine(c);
                }
                Console.WriteLine();
                if (cartasDisponibles.Count == 0)
                {
                    break;
                }
                try
                {
                    carta = int.Parse(Console.ReadLine());
                    Console.WriteLine();
                    if (carta == 0)
                    {
                        try
                        {
                            if (cartasAMostrar.Count == 0)
                            {
                                throw new NoHayNaipesException();
                            }
                        }
                        finally
                        {
                        }
                    }
                    else
                    {
                        cartasAMostrar.Add(carta);
                        cartasDisponibles.Remove(carta);
                    }
                }
                catch (NoHayNaipesException)
                {
                    carta = -1;
                    Console.WriteLine("Como mínimo, debe agregar 1 naipe");
                    Console.WriteLine("Presione una tecla para continuar.");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (FormatException)
                {
                    carta = -1;
                    Console.WriteLine("");
                    Console.WriteLine("Opción no válida.");
                    Console.WriteLine("Presione una tecla para continuar.");
                    Console.ReadLine();
                    Console.Clear();
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Opción no válida.");
                    Console.WriteLine("Presione una tecla para continuar.");
                    Console.ReadLine();
                    Console.Clear();
                }
            } while (carta != 0);

            foreach (int c in cartasAMostrar)
            {
                bool juegaHumanoConsulta = !juegaHumano;

                int limiteConsulta = limite;

                foreach (ArbolGeneral<Dupla> h in player1.jugadaActual.getHijos())
                {

                    if (h.getDatoRaiz().getCarta() == c)
                    {
                        Console.WriteLine("Para la carta: " + c);

                        List<List<Dupla>> lista = new List<List<Dupla>>();

                        lista = h.getListaRecorridos();

                        foreach (List<Dupla> a in lista)
                        {
                            string linea = "";

                            foreach (Dupla i in a)
                            {
                                if(juegaHumanoConsulta == true)
                                {
                                    limiteConsulta = limiteConsulta - i.getCarta();

                                    linea = linea + "H tira carta: " + i.getCarta() + " (lim. restante:  " + limiteConsulta + "), ";

                                    juegaHumanoConsulta = false;

                                    if(a.IndexOf(i) == a.Count - 1)
                                    {
                                        linea = linea + "gana AI";
                                    }
                                }
                                else
                                {
                                    limiteConsulta = limiteConsulta - i.getCarta();

                                    linea = linea + "AI tira carta: " + i.getCarta() + " (lim. restante: " + limiteConsulta + "), ";

                                    juegaHumanoConsulta = true;

                                    if (a.IndexOf(i) == a.Count - 1)
                                    {
                                        linea = linea + "gana H";
                                    }
                                }
                            }



                            Console.WriteLine(linea);

                            Console.WriteLine();

                            juegaHumanoConsulta = !juegaHumano;

                            limiteConsulta = limite;

                        }

                        break; // fin de la busqueda secuencial
                    }
                }


            }


        }

        // Consulta 1 es similar a consulta 2, pero agregar todas las cartas a mostrar

        private void Consulta1()
        {

            List<int> cartasAMostrar = new List<int>();

            cartasAMostrar.AddRange(naipesHuman);

            foreach (int c in cartasAMostrar)
            {
                bool juegaHumanoConsulta = !juegaHumano;

                int limiteConsulta = limite;

                foreach (ArbolGeneral<Dupla> h in player1.jugadaActual.getHijos())
                {

                    if (h.getDatoRaiz().getCarta() == c)
                    {
                        List<List<Dupla>> lista = new List<List<Dupla>>();

                        lista = h.getListaRecorridos();

                        foreach (List<Dupla> a in lista)
                        {
                            string linea = "";

                            foreach (Dupla i in a)
                            {
                                if (juegaHumanoConsulta == true)
                                {
                                    limiteConsulta = limiteConsulta - i.getCarta();

                                    linea = linea + "H tira carta: " + i.getCarta() + " (lim. restante:  " + limiteConsulta + "), ";

                                    juegaHumanoConsulta = false;

                                    if (a.IndexOf(i) == a.Count - 1)
                                    {
                                        linea = linea + "gana AI";
                                    }
                                }
                                else
                                {
                                    limiteConsulta = limiteConsulta - i.getCarta();

                                    linea = linea + "AI tira carta: " + i.getCarta() + " (lim. restante: " + limiteConsulta + "), ";

                                    juegaHumanoConsulta = true;

                                    if (a.IndexOf(i) == a.Count - 1)
                                    {
                                        linea = linea + "gana H";
                                    }
                                }
                            }

                            Console.WriteLine(linea);

                            Console.WriteLine();

                            juegaHumanoConsulta = !juegaHumano;

                            limiteConsulta = limite;

                        }

                        break; // fin de la busqueda secuencial
                    }
                }


            }


        }

        private void Consulta3()
        {
            int opcion = -1;

            do
            {
                Console.WriteLine("Ingrese la profundidad o 0 para salir:");
                Console.WriteLine();
                try
                {
                    opcion = int.Parse(Console.ReadLine());
                    break;
                }

                catch (FormatException)
                {
                    opcion = -1;
                    Console.WriteLine("");
                    Console.WriteLine("Opción no válida.");
                    Console.WriteLine("Presione una tecla para continuar.");
                    Console.ReadLine();
                    Console.Clear();
                }

            } while (opcion != 0);

            Console.WriteLine();

            player1.MiniMax.recorridoEntreNiveles(opcion, opcion);

            Console.WriteLine();
        }


    }



    public class NoHayNaipesException : Exception
    {
    }

    public class FueraDeRangoException : Exception
    {
    }
}
