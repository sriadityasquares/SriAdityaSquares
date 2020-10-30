using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class MyProfile
    {
       
        public string Name { get; set; }
        public Nullable<long> Mobile { get; set; }
        public string Email { get; set; }
        public string Pan { get; set; }
        public string Aadhar { get; set; }
        [Display(Name = "Code")]

        public string AgentCode { get; set; }

        [Display(Name = "Account No")]
        public string AccountNo { get; set; }
        public string Bank { get; set; }

        [Display(Name = "IFSC Code")]
        public string IFSC { get; set; }
    }
}
