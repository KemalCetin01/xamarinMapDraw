using DynamicAPI.Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.Entities.Concrete
{
    public class Location : Base
    {
        public int UserID { get; set; }
        public User User { get; set; }
        public string LatLongt { get; set; }
    }
}
