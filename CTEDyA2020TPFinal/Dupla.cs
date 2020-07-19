using System;
namespace juegoIA
{
    public class Dupla
    {
        //Dupla de enteros (carta,  valorFuncionHeuristica)

        public Dupla(int carta, int valor)
        {
            this.carta = carta;
            this.valorFuncionHeuristica = valor;
        }

        private int carta;

        private int valorFuncionHeuristica;

        public void setCarta(int carta)
        {
            this.carta = carta;
        }

        public int getCarta()
        {
            return carta;
        }

        public void setFuncionHeuristica(int valor)
        {
            this.valorFuncionHeuristica = valor;
        }

        public int getValorFuncionHeuristica()
        {
            return valorFuncionHeuristica;
        }

        public string getDupla()
        {
            return "(" + getCarta() + ", " + getValorFuncionHeuristica() + ")";
        }

        public override string ToString()
        {
            return getDupla();
        }
    }
}
