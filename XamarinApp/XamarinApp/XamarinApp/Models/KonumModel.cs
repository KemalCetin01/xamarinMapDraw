using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Models
{
   public class LocationModel: BaseModel
    {
        public int UserID { get; set; }
        public UserModel Users { get; set; }
        public string LatLongt { get; set; }
    }
}
