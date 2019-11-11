using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetTowerDetails
    {
        public int TowerID { get; set; }
        public string TowerName { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> FlatCount { get; set; }
        public string BookingStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string ProjectName { get; set; }

        public string BookingStatusName { get; set; }
    }
}
