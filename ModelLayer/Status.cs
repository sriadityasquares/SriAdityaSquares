using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public partial class Status
    {
        public string ID { get; set; }
        public string BookingStatus { get; set; }
        public Nullable<int> StatusType { get; set; }
    }
}
