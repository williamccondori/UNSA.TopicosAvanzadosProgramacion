using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UNSA.TopicosAvanzadosProgramacion.DistanciaDocumentos
{
    [TestClass]
    public class DistanciaDocumentosTest
    {
        [TestMethod]
        public void TestDocumentoUno()
        {
            DocumentoOperacion operacion = new DocumentoOperacion();
            decimal distancia = operacion.ObtenerDistancia("texto01.txt", "texto02.txt").Redondear(4);
            Assert.AreEqual(distancia, 0.3844M);
        }

        [TestMethod]
        public void TestDocumentoDos()
        {
            DocumentoOperacion operacion = new DocumentoOperacion();
            decimal distancia = operacion.ObtenerDistancia("t1.verne.txt", "t2.bobsey.txt").Redondear(4);
            Assert.AreEqual(distancia, 0.9889M);
        }

        [TestMethod]
        public void TestDocumentoTres()
        {
            DocumentoOperacion operacion = new DocumentoOperacion();
            decimal distancia = operacion.ObtenerDistancia("t2.bobsey.txt", "t9.bacon.txt").Redondear(4);
            Assert.AreEqual(distancia, 0.9529M);
        }
    }
}
