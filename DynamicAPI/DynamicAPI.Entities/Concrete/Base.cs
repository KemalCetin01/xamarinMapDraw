using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicAPI.Entities.Concrete
{
    public class Base
    {
        public int Id { get; set; }
        public DateTime createTime { get; set; }
        public bool isDeleted { get; set; }
    }
}
