using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SopaDeLetras
{
    public class Matriz
    {
        private char[,] miMatrizDeLetras;
        public char[,] MatrizDeLetras
        {
            get { return miMatrizDeLetras; }
            set { miMatrizDeLetras = value; }
        }

        public Matriz(char[,] _matrizDeLetras)
        {
            this.miMatrizDeLetras = _matrizDeLetras;
        }

        /// <summary>
        /// En base a un array de Strings Arma una matriz de Caracteres
        /// </summary>
        /// <param name="secuencias"></param>
        public void ArmarMatriz(string[] secuencias)
        {
            //Armar Matriz
            int r = miMatrizDeLetras.GetLength(0); // fila
            int c = miMatrizDeLetras.GetLength(1); // colunma

            for (int x = 0; x < r; x++)
            {
                for (int y = 0; y < c; y++)
                {
                    miMatrizDeLetras[x, y] = Convert.ToChar(secuencias[x].Substring(y, 1));
                }
            }
        }

        /// <summary>
        /// Obtiene las posiciones en la matriz de una determinada palabra
        /// </summary>
        /// <param name="palabra">Palabra a buscar</param>
        /// <returns>Retorna null si no se encontro la palabra</returns>
        public int[,] getPosiciones(string palabra)
        {
            int[,] coordenadas = new int[palabra.Length, 2];
            char Letra;
            int xLetraAnterior = 0;
            int yLetraAnterior = 0;
            bool flag = false;
            int i = 0;

            do
            {
                Letra = Convert.ToChar(palabra.Substring(i, 1));
                int[] coordenadasPL = ObtenerPosicionLetra(Letra, new int[] { xLetraAnterior, yLetraAnterior });

                //Si no encuentra la letra en la matriz, sale del bucle y retorna null
                if (coordenadasPL == null)
                {
                    return null;
                }
                //Coordenadas de la primera letra
                coordenadas[0, 0] = coordenadasPL[0];
                coordenadas[0, 1] = coordenadasPL[1];

                if (BusquedaHorizontal(palabra, i + 1, coordenadasPL, ref coordenadas))
                {
                    flag = true;
                }
                else if (BusquedaDiagonal(palabra, i + 1, coordenadasPL, ref coordenadas))
                {
                    flag = true;
                }
                else if (BusquedaVertical(palabra, i + 1, coordenadasPL, ref coordenadas))
                {
                    flag = true;
                }
                else
                {
                    xLetraAnterior = coordenadasPL[0];
                    yLetraAnterior = coordenadasPL[1] + 1;
                    i = 0;
                }
            } while (!flag);

            //Antes de devolver la coordenada, se normaliza a la tabla especificada
            //Para una matriz en C# es 0,0 pero para la matriz o tabla especificada es 1,1, mas comprensible al usuario final.
            int r = coordenadas.GetLength(0); // width
            int c = coordenadas.GetLength(1); // height
            for (int x = 0; x < r; ++x)
            {
                for (int y = 0; y < c; ++y)
                {
                    coordenadas[x, y] = coordenadas[x, y] + 1;
                }
            }
            return coordenadas;
        }

        private bool BusquedaHorizontal(string palabra, int posicionLetra, int[] coordenadasPL, ref int[,] coordenadas)
        {
            if (posicionLetra == palabra.Length)
                return true;

            char Letra = Convert.ToChar(palabra.Substring(posicionLetra, 1));
            if (coordenadasPL[0] < miMatrizDeLetras.GetLength(0) && coordenadasPL[1] + 1 < miMatrizDeLetras.GetLength(1))
            {
                if (miMatrizDeLetras[coordenadasPL[0], coordenadasPL[1] + 1].Equals(Letra))
                {
                    coordenadas[posicionLetra, 0] = coordenadasPL[0];
                    coordenadas[posicionLetra, 1] = coordenadasPL[1] + 1;
                    return BusquedaHorizontal(palabra, posicionLetra + 1, new int[] { coordenadasPL[0], coordenadasPL[1] + 1 }, ref coordenadas);
                }
                else
                    return false;
            }
            else
                return false;
        }

        private bool BusquedaDiagonal(string palabra, int posicionLetra, int[] coordenadasPL, ref int[,] coordenadas)
        {
            if (posicionLetra == palabra.Length)
                return true;

            char Letra = Convert.ToChar(palabra.Substring(posicionLetra, 1));
            if (coordenadasPL[0] + 1 < miMatrizDeLetras.GetLength(0) && coordenadasPL[1] + 1 < miMatrizDeLetras.GetLength(1))
            {
                if (miMatrizDeLetras[coordenadasPL[0] + 1, coordenadasPL[1] + 1].Equals(Letra))
                {
                    coordenadas[posicionLetra, 0] = coordenadasPL[0] + 1;
                    coordenadas[posicionLetra, 1] = coordenadasPL[1] + 1;
                    return BusquedaDiagonal(palabra, posicionLetra + 1, new int[] { coordenadasPL[0] + 1, coordenadasPL[1] + 1 }, ref coordenadas);
                }
                else
                    return false;
            }
            else
                return false;
        }

        private bool BusquedaVertical(string palabra, int posicionLetra, int[] coordenadasPL, ref int[,] coordenadas)
        {
            if (posicionLetra == palabra.Length)
                return true;

            char Letra = Convert.ToChar(palabra.Substring(posicionLetra, 1));
            if (coordenadasPL[0] + 1 < miMatrizDeLetras.GetLength(0) && coordenadasPL[1] < miMatrizDeLetras.GetLength(1))
            {
                if (miMatrizDeLetras[coordenadasPL[0] + 1, coordenadasPL[1]].Equals(Letra))
                {
                    coordenadas[posicionLetra, 0] = coordenadasPL[0] + 1;
                    coordenadas[posicionLetra, 1] = coordenadasPL[1];
                    return BusquedaVertical(palabra, posicionLetra + 1, new int[] { coordenadasPL[0] + 1, coordenadasPL[1] }, ref coordenadas);
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Obtiene la posicion que tiene una letra en la Matriz de caracteres.
        /// </summary>
        /// <param name="Letra">Letra a buscar</param>
        /// <param name="start">Parte de la matriz desde donde comenzar a buscar</param>
        /// <returns></returns>
        private int[] ObtenerPosicionLetra(char Letra, int[] start)
        {
            int[] _coordenadas = new int[2];
            int r = miMatrizDeLetras.GetLength(0); // fila
            int c = miMatrizDeLetras.GetLength(1); // colunma
            int varY = start[1];

            for (int x = start[0]; x < r; ++x)
            {
                for (int y = varY; y < c; ++y)
                {
                    if (miMatrizDeLetras[x, y].Equals(Letra))
                    {
                        _coordenadas[0] = x;
                        _coordenadas[1] = y;
                        return _coordenadas;
                    }
                }
                varY = 0;
            }
            //No encontró en la matriz, la letra a buscar
            return null;
        }

        /*
        public void ImprimirMatriz()
        {
            //Imprimir Matriz
            int r = miMatrizDeLetras.GetLength(0); // fila
            int c = miMatrizDeLetras.GetLength(1); // colunma

            for (int x = 0; x < r; x++)
            {
                for (int y = 0; y < c; y++)
                {
                    Console.Write(miMatrizDeLetras[x, y]);
                }
                Console.WriteLine("");
            }
            Console.ReadLine();
        }
         */
    }
}
