using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLayer
{
    public class CustomerVisitInfo
    {
        public int VisitID { get; set; }

        [Display(Name = "Agent")]
        public Nullable<int> AgentID { get; set; }
        
        public string AgentName { get; set; }

        [Display(Name = "Customer")]
        public string CustomerName { get; set; }

        [Display(Name = "Customer Mobile")]
        public string CustomerMobile { get; set; }
        public byte[] Selfie { get; set; }
        public HttpPostedFileBase SelfieFile { get; set; }
        //public  Selfie { get; set; }
        [Display(Name = "Project")]
        public string ProjectID { get; set;   }
        public string ProjectName { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public string AddedBy { get; set; }

        public string SelfieURL { get; set; }
    }
}
