using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary3;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Creo un objeto de la clase que se va a testear
            //defino los valores de las variables que voy a utilizar
            //llamo al metodo que voy a testear
            Class1 objSuma = new Class1();
            int a = 4;
            int b = 1;
            int esperado = 5;

            int suma = objSuma.TestMSuma(a, b);

            Assert.AreEqual(esperado, suma);
        }
    }
}
