using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _20210116_d1_Mvc_Giris.Metodlar
{
    static public class Metod
    {
        static public string Alert(string txt, AlertTypes types)
        {
            string alert = string.Empty;
            switch (types)
            {
                case AlertTypes.Success:
                    alert = "success";
                    break;
                case AlertTypes.Info:
                    alert = "info";
                    break;
                case AlertTypes.Warning:
                    alert = "warning";
                    break;
                case AlertTypes.Danger:
                    alert = "danger";
                    break;
            }

            return "<div class='alert alert-" + alert + "'>" + txt + "</div>";
        }

        static public int ToInt(this object value)
        {
            try
            {
                //return int.Parse(value.ToString());
                return Convert.ToInt32(value);
            }
            catch
            {
                return 0;
            }
        }
    }
}