using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.Entities.DTOs
{
    public class User:Base
    {
        public string kullaniciAdi { get; set; }
        public string sifre { get; set; }
        public string adi { get; set; }
        public string soyadi { get; set; }

    }
}
