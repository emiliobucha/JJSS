using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JJSS_Entidad;
using JJSS_Negocio;

namespace JJSS_Negocio
{
    public class GestorParametro
    {


        /*
         * Metodo que modifica el valor de los parametros generales
         * Parametros:  pIdParamentro: entero que indica el ID del parametro
         *              pValor : decimal que indica el nuevo valor
         * Retornos:    "" - Transaccion correcta de la BD
         *              ex.Message- error en la BD
         * 
         */
        public string modificarParametro(int pIdParametro, decimal pValor)
        {
            using (var db = new JJSSEntities())
            {
                try
                {
                    parametro paramEncontrado = db.parametro.Find(pIdParametro);
                    paramEncontrado.valor = pValor;
                    db.SaveChanges();

                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

                return "";
            }
        }
        
    }
}
