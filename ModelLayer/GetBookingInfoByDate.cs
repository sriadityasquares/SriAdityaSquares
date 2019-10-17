using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetBookingInfoByDate
    {
        public string projectname { get; set; }
        public string towername { get; set; }
        public string flatname { get; set; }
        public string name { get; set; }
        public string agentname { get; set; }
        public string bookingdate { get; set; }
        public Nullable<int> bookingamount { get; set; }
        public Nullable<int> discount { get; set; }
        public Nullable<int> highrisecharges { get; set; }
        public Nullable<int> area { get; set; }
        public Nullable<int> year { get; set; }
        public Nullable<int> month { get; set; }
        public Nullable<int> day { get; set; }
    }
}
