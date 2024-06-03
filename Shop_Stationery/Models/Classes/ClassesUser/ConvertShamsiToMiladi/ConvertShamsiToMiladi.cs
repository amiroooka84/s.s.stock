using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MD.PersianDateTime.Standard;


namespace Shop_Stationery.Models.Classes.ClassesUser.ConvertShamsiToMiladi
{
    public static class ConvertDateTime
    {
        public static DateTime ConvertShamsiToMiladi(string date)
        {
            PersianDateTime persianDateTime = PersianDateTime.Parse(date);
            return persianDateTime.ToDateTime();
        }

        public static string ConvertMiladiToShamsi(this DateTime? date, string format)
        {
            PersianDateTime persianDateTime = new PersianDateTime(date);
            return persianDateTime.ToString(format);
        }
    }
}
