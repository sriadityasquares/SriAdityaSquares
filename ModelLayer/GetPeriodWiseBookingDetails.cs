using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetPeriodWiseBookingDetails
    {
        public Nullable<int> totalbookingamount { get; set; }
        public Nullable<int> year { get; set; }
        public Nullable<int> MONTH { get; set; }
        public Nullable<int> DAY { get; set; }
        public Nullable<int> flatsbooked { get; set; }
        public Nullable<int> projectid { get; set; }
        public Nullable<int> towerid { get; set; }
        public string projectname { get; set; }
        public string towername { get; set; }
    }
}
