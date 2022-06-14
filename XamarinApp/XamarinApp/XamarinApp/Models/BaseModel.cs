using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime createTime { get; set; }
        public bool isDeleted { get; set; }
    }
}
