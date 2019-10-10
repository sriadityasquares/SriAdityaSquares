using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer
{
    public partial class Flats
    {
        public int FlatID { get; set; }
        public string FlatName { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> TowerID { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<int> Bhk { get; set; }
        public Nullable<int> Sft { get; set; }
        public string Facing { get; set; }
        public Nullable<int> BookingStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public virtual Projects tblProject { get; set; }
        public virtual Towers tblTower { get; set; }
    }
}
