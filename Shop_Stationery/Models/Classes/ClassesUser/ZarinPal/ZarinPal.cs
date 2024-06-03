using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.Classes.ClassesUser.ZarinPal
{
    public class ZarinPalRequest
    {
        public string merchant_id { get; set; }
        public int amount { get; set; }
        public string description { get; set; }
        public string callback_url { get; set; }
    }

    public class DataBackRequest
    {
        public int code { get; set; }
        public string message { get; set; }
        public string authority { get; set; }
        public string fee_type { get; set; }
        public int fee { get; set; }
    }

    public class ZarinPalBackRequest
    {
        public DataBackRequest data { get; set; }
        public List<object> errors { get; set; }
    }

    public class DataVerify
    {
        public int code { get; set; }
        public long ref_id { get; set; }
        public string card_pan { get; set; }
        public string callback_url { get; set; }
        public string card_hash { get; set; }
        public string fee_type { get; set; }
        public int fee { get; set; }
    }

    public class ZarinPalVerify
    {
        public DataVerify data { get; set; }
        public List<object> errors { get; set; }

    }
    public class ZarinPalRequestVerify
    {
        public string merchant_id { get; set; }
        public int amount { get; set; }
        public string authority { get; set; }

    }
}
