using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{

    public class SiteVisitInfo
    {
        public int ID { get; set; }
        [Display(Name = "Project")]
        [Required]
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }
        [Display(Name = "Agent")]
        [Required]
        public Nullable<int> AgentID { get; set; }
        public string AgentName { get; set; }
        [Display(Name = "Immediate Senior")]
        
        public Nullable<int> ImmediateSeniorID { get; set; }
        public string ImmediateSeniorName { get; set; }
        [Display(Name = "Customer")]
        [Required]
        public string CustomerName { get; set; }
        [Display(Name = "No Of Customers")]
        public Nullable<int> NoOfCustomers { get; set; }
        [Display(Name = "Customer Mobile")]
        [Required]
        public string CustomerMobile { get; set; }
        [Display(Name = "From Address")]
        public string FromAddress { get; set; }
        [Display(Name = "To Address")]
        public string ToAddress { get; set; }

        [Display(Name = "Visit Date")]
        [Required]
        public string DateOfVisit { get; set; }

        
        [Display(Name = "I accept to deduct Rs.3000/- on every site visit made by me through company transport from my commission.")]
        //[Range(typeof(bool), "true", "true", ErrorMessage = "Please accept the terms and conditions!")]
        public bool TermsAccepted { get; set; }
        public string Status { get; set; }
        public Nullable<bool> isApproved { get; set; }
        public string ApprovedOrRejectedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }

}
