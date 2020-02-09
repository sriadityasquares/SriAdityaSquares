using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class LevelsMaster
    {
        public int LevelID { get; set; }
        public string LevelName { get; set; }
        public Nullable<double> Percentage { get; set; }
        public string BookingStatus { get; set; }
        //public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
