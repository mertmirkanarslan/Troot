using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troot.Core
{
    //attribute kullanımı için enum oluşturuldu. burada para birimleri baz alındı. dolar, euro ve sterlin para birimleri eklendi
    //bunlar şimdilik kullanılmayacak. Extension kullanılmak istenirse yolları hatırlanmak için Troot.Core.Enum'daki kullanıma benzer enum oluşturup
    //Troot.Core.Extension yolundaki gibi kullanabilirsin. Para birimi, Ürün türü vs birçok yerde bu şekilde extension kullanılabilir.
    public enum Enum
    {
        [Display(Name = "Dollar")]
        CurrencyTypeDollar = 1,
        [Display(Name = "Euro")]
        CurrencyTypeEuro = 2,
        [Display(Name = "Sterling")]
        CurrencyTypeSterling = 3,
    }
}
