using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class AgentMaster
    {
        public int AgentID { get; set; }
        public string AgentCode { get; set; }
        public string AgentName { get; set; }
        public Nullable<long> AgentMobileNo { get; set; }
        public string AgenteMail { get; set; }
        public string AgentStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public string AgentAadhar { get; set; }
        public string AgentPAN { get; set; }
        public string AgentBankAcctNo { get; set; }
        public string AgentBankName { get; set; }
        public string AgentBankBranch { get; set; }
        public string AgentBankIFSC { get; set; }
        public string AgentSponserCode { get; set; }
        public string AgentSuperSponserCode { get; set; }

        public string BookingStatusName { get; set; }

        public string AgentParent { get; set; }
        public string AgentChild { get; set; }

        //public int[] arrayListParent { get; set; }

        public Nullable<double> AgentLatitude { get; set; }
        public Nullable<double> AgentLongitude { get; set; }

        public bool isDuplicateAgentCode { get; set; }

        public bool isDuplicateAgentEmail { get; set; }

        public bool isDuplicateAgentMobile { get; set; }

        public int Designation { get; set; }

        public Nullable<int> OldDesignation { get; set; }
        public Nullable<System.DateTime> EffectivePercentageDate { get; set; }

        public Nullable<int> FranchiseID { get; set; }

        public string Gender { get; set; }
        public string MaritalStatus { get; set; }
        public string Dob { get; set; }
        public string City { get; set; }
        public string State { get; set; }

    }
}
