using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mercadopago;
using System.Collections;

namespace JJSS_Negocio
{
    public class GestorMercadoPago
    {
        private String clientID = "320449594801310";
        private String clientSecret = "0owd4CTkNJSAunxQuWSFyp72aEqzqOkH";
        private MP mp;

        public String NuevoPago(double pMonto, string pConcepto)
        {
            mp = new MP(clientID, clientSecret);


            string preferenceData = "{\"items\":" +
                                    "[{" +
                                    "\"title\":\""+ pConcepto +"\"," +
                                    "\"quantity\":1," +
                                    "\"currency_id\":\"ARS\"," +
                                    "\"unit_price\":" + pMonto +
                                    "}]" +
                                    "}";

            Hashtable preference = mp.createPreference(preferenceData);

            var response = preference["response"] as Hashtable;

            return response["init_point"].ToString();
        }

    }
}
