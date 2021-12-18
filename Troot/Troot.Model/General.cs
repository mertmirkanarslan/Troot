using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troot.Model
{
    public class General<T>
    {
        //Model üzerindeki genel işlemleri yaparken kullanacağımız class
        public bool IsSuccess { get; set; } //Veri doğrulamasında kullanacağımız bir tanımlama
        public T Entity { get; set; } //Tek bir entity dönerken kullanıyoruz
        public List<T> List { get; set; } //T tipinde liste dönebiliriz bu tanımlama ile.
        public string ExceptionMessage { get; set; } //hata mesajlarını dönerken kullanacağız
        public string Message { get; set; }  //Mesaj dönerken kullanacağız
    }
}
