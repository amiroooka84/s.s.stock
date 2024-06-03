using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_Stationery.Models.Classes.ClassesUser.SMS
{
    public class SMS
    {
        public bool SendConfirmCode(string Code , string Number)
        {
            try
            {
                var sender = "100088777";
                var receptor = Number;
                var message = "فروشگاه استوک ستاره سهیل<br/>" +
                    "کد تایید:" + Code;
                var api = new Kavenegar.KavenegarApi("634441596E5250654C5A32694D465678397850507075636E646A657464396F57352B7A45444F6F79722F413D");
                api.Send(sender, receptor, message);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
