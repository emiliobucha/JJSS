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
        private String clientID= "320449594801310";
        private String clientSecret= "0owd4CTkNJSAunxQuWSFyp72aEqzqOkH";
        private MP mp;

        public String NuevoPago(String pMonto)
        {
            mp = new MP(clientID, clientSecret);

            Hashtable preference = mp.createPreference("{\"items\":[{\"title\":\"Pago Clase\",\"quantity\":1,\"currency_id\":\"ARS\",\"unit_price\":"+pMonto+"}]}");

            var response = preference["response"] as Hashtable;


            return response["init_point"].ToString();
        }

    }
}
