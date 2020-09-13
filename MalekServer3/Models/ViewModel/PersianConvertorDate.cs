using System;
using System.Globalization;

namespace MalekServer3
{
    public static class PersianConvertorDate
    {
        public static string ToRial(this int value)
        {
            return value.ToString("#,0 Tooman");
        }
        public static string ToShamsi(this string val)
        {
            DateTime value = Convert.ToDateTime(val);
            PersianCalendar pc=new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }
    }
}