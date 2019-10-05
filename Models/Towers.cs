using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public partial class Towers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Towers()
        {
            this.tblFlats = new HashSet<Flats>();
        }

        public int TowerID { get; set; }
        public string TowerName { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public Nullable<int> FlatCount { get; set; }
        public Nullable<int> BookingStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flats> tblFlats { get; set; }
        public virtual Projects tblProject { get; set; }
    }
}
