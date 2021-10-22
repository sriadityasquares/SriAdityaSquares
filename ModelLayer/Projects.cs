﻿using System;
using System.Collections.Generic;

namespace ModelLayer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public partial class Projects
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Projects()
        {
            this.tblFlats = new HashSet<Flats>();
            this.tblTowers = new HashSet<Towers>();
        }


        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLocation { get; set; }
        public string BookingStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ClubHouseCharges { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Flats> tblFlats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Towers> tblTowers { get; set; }

        public string BookingStatusName { get; set; }

        public string projectImageURL { get; set; }

        public Nullable<bool> GHMC_Approval { get; set; }
        public string GHMCApprovalDocument { get; set; }
        public Nullable<bool> RERA_Approval { get; set; }
        public string RERAApprovalDocument { get; set; }
        public string Description { get; set; }
        public string LocationURL { get; set; }

        public string ApprovalStatus { get; set; }

        public string Pricing { get; set; }

        public Nullable<int> BSP { get; set; }
        public Nullable<int> ProjectType { get; set; }
        public Nullable<bool> ProjectApproved { get; set; }
        //public string ProjectTypeName { get; set; }
        //public List<ProjectTypes> ProjectTypes { get; set; }
    }
}
