using System;
using System.Collections.Generic;
namespace juegoIA
{
    public class Estatus
    {
        public Estatus()
        {
        }

        private List<int> cartasHumano = new List<int>();

        private List<int> cartasIA = new List<int>();

        private int limite;

        private bool juegaHumano;

        public List<int> getCartasHumano()
        {
            return cartasHumano;
        }

        public void setCartasHumano(List<int> lista)
        {
            cartasHumano = lista;
        }

        public List<int> getCartasIA()
        {
            return cartasIA;
        }

        public void setCartasIA(List<int> lista)
        {
            cartasIA = lista;
        }

        public int getLimite()
        {
            return limite;
        }

        public void setLimite(int l)
        {
            limite = l;
        }

        public bool returnJuegaHumano()
        {
            return juegaHumano;
        }

        public void setJuegaHumanoTrue()
        {
            juegaHumano = true;
        }

        public void setJuegaHumanoFalse()
        {
            juegaHumano = false;
        }
    }
}
