using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SopaDeLetras;


namespace TelefeWebApi.Controllers
{
    public class ValuesController : ApiController
    {
        public HttpResponseMessage Get(string word = "TELEFE")
        {
            string[] secuencias = { "AGVNFT", "XJILSB", "CHAOHD", "ERCVTQ", "ASOYAO", "ERMYUA", "TELEFE" };
            Matriz matrizDeLetras = new Matriz(new char[7, 6]);
            matrizDeLetras.ArmarMatriz(secuencias);
            int[,] retorno = matrizDeLetras.getPosiciones(word);
            if (retorno == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "La palabra no esta en la matriz");
            else
            {
                //Solo guarda la busqueda si la palabra estaba en la matriz.
                HistorialController HC = new HistorialController();
                HC.Insert(word);
                return Request.CreateResponse(HttpStatusCode.OK, ImprimirMatriz(retorno));
            }
        }

        private string ImprimirMatriz(int[,] array)
        {
            //Imprimir Matriz
            string retorno = "";
            int r = array.GetLength(0); // fila
            int c = array.GetLength(1); // colunma

            /**/
            retorno = retorno + "{";
            for (int x = 0; x < r; x++)
            {
                retorno = retorno + "{";
                for (int y = 0; y < c; y++)
                {
                    //Console.Write(miMatrizDeLetras[x, y]);
                    retorno = retorno + array[x, y] + ";";
                }
                retorno = retorno.Substring(0, retorno.Length - 1);
                retorno = retorno + "}";
            }
            retorno = retorno + "}";
            return retorno;
            //Imprimir Matriz
        }



    }
}
