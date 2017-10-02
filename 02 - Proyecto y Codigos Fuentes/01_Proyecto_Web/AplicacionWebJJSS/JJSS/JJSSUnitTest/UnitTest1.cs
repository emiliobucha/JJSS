using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JJSS_Negocio;
using System.Data;

namespace JJSSUnitTest
{
    [TestClass]
    public class UnitTestPrueba
    {
        //[TestMethod]
        //public void UTCalcularEdadPasa()
        //{
        //    GestorInscripciones CE = new GestorInscripciones();
        //    DateTime fecha = new DateTime(1994, 2, 21);
        //    string edadTexto = CE.calcularEdad(fecha);
        //    string esperado = "23";
        //    Assert.AreEqual(esperado, edadTexto);
        //}

        //[TestMethod]
        //public void UTCalcularEdadFalla()
        //{
        //    GestorInscripciones CE = new GestorInscripciones();
        //    DateTime fecha = new DateTime(1994, 2, 21);
        //    string edadTexto = CE.calcularEdad(fecha);
        //    string esperado = "13";
        //    Assert.AreEqual(esperado, edadTexto);
        //}



        ////[TestMethod]
        ////public void UTCalcularEdadCorrectaPasa()
        ////{
        ////    GestorInscripciones CE = new GestorInscripciones();
        ////    DateTime fecha = new DateTime(1992, 9, 15);
        ////    string edadTexto = CE.calcularEdadCorrecta(fecha);
        ////    string esperado = "24";
        ////    Assert.AreEqual(esperado, edadTexto);
        ////}

        ////[TestMethod]
        ////public void UTCalcularEdadCorrectaFalla()
        ////{
        ////    GestorInscripciones CE = new GestorInscripciones();
        ////    DateTime fecha = new DateTime(1992, 9, 15);
        ////    string edadTexto = CE.calcularEdadCorrecta(fecha);
        ////    string esperado = "25";
        ////    Assert.AreEqual(esperado, edadTexto);
        ////}

        ////[TestMethod]
        ////public void UTGenerarNuevaClaseFalla()
        ////{
        ////    GestorClases GC = new GestorClases();
        ////    int pTipo = 1;
        ////    double pPrecio = 2.2;
        ////    DataTable pHorarios = new DataTable();
        ////    pHorarios.Columns.Add("hora_desde");
        ////    pHorarios.Columns.Add("hora_hasta");
        ////    pHorarios.Columns.Add("nombre_dia");

        ////    for (int i = 0; i < 7; i++)
        ////    {
        ////        DataRow drNuevoHorario = pHorarios.NewRow();
        ////        drNuevoHorario["hora_desde"] = "10:00";
        ////        drNuevoHorario["hora_hasta"] = "11:00";
        ////        drNuevoHorario["nombre_dia"] = "D" + i;
        ////        pHorarios.Rows.Add(drNuevoHorario);
        ////    }

        ////    string pNombre = "Test Clase Longaniza 2";
        ////    string retorno = GC.GenerarNuevaClase(pTipo, pPrecio, pHorarios, pNombre);
        ////    string esperado = "";
        ////    Assert.AreEqual(esperado, retorno);
        ////}
        //[TestMethod]
        //public void UTe()
        //{
        //    modEmails md = new modEmails();
        //    md.Enviar();
        //}



    }
}
