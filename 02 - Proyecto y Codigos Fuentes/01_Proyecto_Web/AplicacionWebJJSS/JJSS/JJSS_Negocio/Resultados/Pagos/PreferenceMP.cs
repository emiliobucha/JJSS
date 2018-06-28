using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JJSS_Negocio.Resultados.Pagos
{
    public class PreferenceMP
    {
        public List<Item> items { get; set; }
        public Back_Url back_urls { get; set; }
        public string auto_return { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public long quantity { get; set; }
        public string currency_id { get; set; }
        public double unit_price { get; set; }
    }

    public class Back_Url
    {
        public string success { get; set; }
        public string failure { get; set; }
        public string pending { get; set; }
    }
}
