using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{

    public  class LandLord
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<long> MobileNo { get; set; }
        public string eMail { get; set; }
        public string Aadhar { get; set; }
        public string PAN { get; set; }
        public string BankAcctNo { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string IFSC { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Status { get; set; }

        public string SurveyNo { get; set; }
        public string Location { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
