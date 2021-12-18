using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Troot.Core
{
    public static class Extension
    {
        //extensionlar public static olmalı.
        //önce tip alıyoruz, sonra extensionlarımızı yazıyoruz
        public static string GetCurrencies(this Enum currencyType)
        {
            var result = currencyType.GetType().GetMember(currencyType.ToString()).First().GetCustomAttributes<DisplayAttribute>().First().Name;
            return result.ToString();
        }

        //1. extension => TRY to USD
        public static string toDollar(this double variable)
        {
            return variable / 17 + "$";
        }

        //2. extension => TRY to EUR
        public static string toEuro(this double variable)
        {
            return variable / 20.0 + "€";
        }

        //3. extension => TRY to GBP
        public static string toSterling(this double variable)
        {
            return variable / 22.0 + "€";
        }
    }
}
