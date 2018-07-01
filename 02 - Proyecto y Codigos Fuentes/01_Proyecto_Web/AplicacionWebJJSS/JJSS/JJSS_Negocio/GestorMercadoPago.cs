using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mercadopago;
using System.Collections;
using JJSS_Negocio.Resultados.Pagos;
using Newtonsoft.Json;

namespace JJSS_Negocio
{
    public class GestorMercadoPago
    {
        private String clientID = "4309155312672093";
        private String clientSecret = "QflIW4b4IUeIG9mHM51lTzTc4xEb5zax";
        private MP mp;
        private string uri = "http://ec2-34-228-115-59.compute-1.amazonaws.com:8081/Presentacion/Pagos/";


        public string NuevoPago(double pMonto, string pConcepto)
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
        public string NuevoPagoMultiple(IList<ObjetoPagable> objetosPagables)
        {
            mp = new MP(clientID, clientSecret);

            List<Item> items = new List<Item>();

            foreach (var objeto in objetosPagables)
            {
                items.Add(new Item
                {
                   currency_id = "ARS",
                    quantity = 1,
                    unit_price = (double)objeto.Monto,
                    title = objeto.GetDescripcion()
                });
            }

            PreferenceMP preferenceMP =new PreferenceMP
            {
                items = items,
                auto_return = "all",
                back_urls = new Back_Url
                {
                    failure = uri + "PagoMultipleFinalizado.aspx",
                    pending = uri + "PagoMultipleFinalizado.aspx",
                    success = uri + "PagoMultipleFinalizado.aspx"
                }
            };

            string preferenceData = JsonConvert.SerializeObject(preferenceMP);

            
            Hashtable preference = mp.createPreference(preferenceData);
           

            var response = preference["response"] as Hashtable;

            return response["init_point"].ToString();
        }

    }
}

