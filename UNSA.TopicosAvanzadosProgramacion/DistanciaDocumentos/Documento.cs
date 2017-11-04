using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNSA.TopicosAvanzadosProgramacion.DistanciaDocumentos
{
    public class Documento
    {
        private readonly string[] caracteresEspeciales = { " ", ",", ";", ".", ":", "\t", "\r", "\n", "[", "]", "{", "}", "_", "-", "/", "//", "*", "(", ")" };

        private string _texto;

        public Documento()
        {
        }

        public void AbrirDocumento(string ruta)
        {
            StreamReader archivo = new StreamReader(ruta, Encoding.UTF8);
            _texto = archivo.ReadToEnd();
            archivo.Close();
        }

        public List<string> ObtenerPalabras()
        {
            return _texto.Split(caracteresEspeciales, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        public static List<string> ObtenerConjuntoPalabras(List<string> primerTexto, List<string> segundoTexto)
        {
            List<string> conjunto = new List<string>();

            for (int i = 0; i < primerTexto.Count; i++)
                if (!ExistePalabra(conjunto, primerTexto[i]))
                    conjunto.Add(primerTexto[i]);

            for (int i = 0; i < segundoTexto.Count; i++)
                if (!ExistePalabra(conjunto, segundoTexto[i]))
                    conjunto.Add(segundoTexto[i]);

            return conjunto;
        }

        private static bool ExistePalabra(List<string> arreglo, string palabra)
        {
            for (int i = 0; i < arreglo.Count; i++)
                if (arreglo[i] == palabra)
                    return true;
            return false;
        }

        public static decimal CalcularModulo(int[] arreglo)
        {
            decimal modulo = 0;

            for (int i = 0; i < arreglo.Length; i++)
                modulo = modulo + (decimal)Math.Pow(arreglo[i], 2);

            return (decimal)Math.Sqrt((double)modulo);
        }

        public static int ContarPalabra(List<string> arreglo, string palabra)
        {
            int cantidad = 0;
            for (int i = 0; i < arreglo.Count; i++)
                if (arreglo[i] == palabra)
                    cantidad++;
            return cantidad;
        }
    }

    public class DocumentoOperacion
    {
        public DocumentoOperacion()
        {

        }

        public decimal ObtenerDistancia(string rutaDocumentoUno, string rutaDocumentoDos)
        {
            string rutaRelativa = "../../DistanciaDocumentos/Documentos/";

            Documento documentoUno = new Documento();
            documentoUno.AbrirDocumento(rutaRelativa + rutaDocumentoUno);
            List<string> primerTexto = documentoUno.ObtenerPalabras();

            Documento documentoDos = new Documento();
            documentoDos.AbrirDocumento(rutaRelativa + rutaDocumentoDos);
            List<string> segundoTexto = documentoDos.ObtenerPalabras();

            List<string> conjuntoTexto = Documento.ObtenerConjuntoPalabras(primerTexto, segundoTexto);

            int[] cantidadPrimero = new int[conjuntoTexto.Count];
            int[] cantidadSegundo = new int[conjuntoTexto.Count];

            for (int i = 0; i < conjuntoTexto.Count; i++)
            {
                string palabra = conjuntoTexto[i];

                int cantidadPrimerArreglo = Documento.ContarPalabra(primerTexto, palabra);
                cantidadPrimero[i] = cantidadPrimerArreglo;

                int cantidadSegundoArreglo = Documento.ContarPalabra(segundoTexto, palabra);
                cantidadSegundo[i] = cantidadSegundoArreglo;
            }

            int productoInterno = 0;

            for (int i = 0; i < cantidadPrimero.Length; i++)
            {
                int resultado = cantidadPrimero[i] * cantidadSegundo[i];
                productoInterno = productoInterno + resultado;
            }

            decimal moduloUno = Documento.CalcularModulo(cantidadPrimero);
            decimal moduloDos = Documento.CalcularModulo(cantidadSegundo);

            decimal modulo = moduloUno * moduloDos;

            double respuestaTemporal = (double)(productoInterno / modulo);

            double respuestaGrados = Math.Asin(respuestaTemporal);

            return (decimal)respuestaGrados;
        }
    }

    public static class DecimalExtension
    {
        public static decimal Redondear(this decimal numero, int decimales)
        {
            return Math.Round(numero, decimales);
        }
    }
}
