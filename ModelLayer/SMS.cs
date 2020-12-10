using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class SMS
    {
        public int MessageNo { get; set; }
        public string MessageType { get; set; }
        public string Message { get; set; }
        public string Recipients { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
