using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class IBOAdvanceForm
    {
        public int ID { get; set; }

        [Display(Name = "IBO")]
        public Nullable<int> IBOID { get; set; }
        public string IBOName { get; set; }

        [Display(Name = "Senior")]
        public Nullable<int> SeniorID { get; set; }
        public string SeniorName { get; set; }

        [Display(Name = "Director")]
        public Nullable<int> DirectorID { get; set; }
        public string DirectorName { get; set; }

        [Display(Name = "Project")]
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }

        [Display(Name = "Tower")]
        public Nullable<int> TowerID { get; set; }
        public string TowerName { get; set; }

        [Display(Name = "Flat")]
        public Nullable<int> FlatID { get; set; }
        public string FlatName { get; set; }

        [Display(Name = "Amount")]
        public Nullable<decimal> AmountPaid { get; set; }

        [Display(Name = "Approved By Director")]
        public bool ApprovedByDirector { get; set; }

        [Display(Name = "Paid Through")]
        public string PaidThrough { get; set; }

        [Display(Name = "Paid Date")]
        public Nullable<System.DateTime> PaidDate { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
