using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Designations
    {
        public int ID { get; set; }
        public string Designation { get; set; }
        public Nullable<double> ConstructionPercentage { get; set; }
        public Nullable<double> NonConstructionPercentage { get; set; }
        public Nullable<double> ConditionalPercentage { get; set; }
        public Nullable<bool> Active { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
