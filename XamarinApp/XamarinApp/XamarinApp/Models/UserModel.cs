using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Models
{
   public class UserModel:BaseModel
    {
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public string Adi { get; set; }
        public string Soyadi { get; set; }
    }
}
