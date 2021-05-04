using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Cancellation
    {
        public int ID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string Project { get; set; }
        public Nullable<int> TowerID { get; set; }
        public string Tower { get; set; }
        public Nullable<int> FlatID { get; set; }
        public string Flat { get; set; }
        public string CustomerName { get; set; }
        public string AgentName { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }

        public string Comments { get; set; }

        public Nullable<System.Guid> BookingID { get; set; }
    }
}
