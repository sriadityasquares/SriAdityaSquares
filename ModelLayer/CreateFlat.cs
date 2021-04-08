using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class CreateFlat
    {
        public int FlatID { get; set; }
        public string FlatName { get; set; }
        public List<Projects> ProjectID { get; set; }
        public List<Towers> TowerID { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<double> Bhk { get; set; }
        public Nullable<int> Sft { get; set; }
        public string Facing { get; set; }
        public string BookingStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string FlatPlanURL { get; set; }
        public virtual Projects tblProject { get; set; }
        public virtual Towers tblTower { get; set; }
    }
}
