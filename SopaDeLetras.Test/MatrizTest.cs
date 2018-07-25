using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SopaDeLetras;
namespace SopaDeLetras.Test
{
    [TestClass]
    public class MatrizTest
    {
        [TestMethod]
        public void TestGetPosicionesNull()
        {
            string[] secuencias = { "AGVNFT", "XJILSB", "CHAOHD", "ERCVTQ", "ASOYAO", "ERMYUA", "TELEFE" };
            Matriz matrizDeLetras = new Matriz(new char[7, 6]);
            matrizDeLetras.ArmarMatriz(secuencias);
            int[,] retorno = matrizDeLetras.getPosiciones("TELEFO");
            if (retorno == null)
                Assert.IsNull(retorno);

        }

        [TestMethod]
        public void TestGetPosiciones()
        {
            string[] secuencias = { "AGVNFT", "XJILSB", "CHAOHD", "ERCVTQ", "ASOYAO", "ERMYUA", "TELEFE" };
            Matriz matrizDeLetras = new Matriz(new char[7, 6]);
            matrizDeLetras.ArmarMatriz(secuencias);
            int[,] retorno = matrizDeLetras.getPosiciones("TELEFE");
            Assert.AreEqual(retorno[0, 0], 7);
            Assert.AreEqual(retorno[0, 1], 1);

            Assert.AreEqual(retorno[1, 0], 7);
            Assert.AreEqual(retorno[1, 1], 2);


        }
    }
}
