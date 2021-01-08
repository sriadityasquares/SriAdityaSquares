using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLayer
{
    public class FranchiseRegistration
    {
        public int RegisterNo { get; set; }
        public int Country { get; set; }

        public int State { get; set; }

        public int City { get; set; }

        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        public int Area { get; set; }

        public string AreaName { get; set; }

        public int IBOID { get; set; }
        public string IBOName { get; set; }

        public int Amount { get; set; }

        public string Duration { get; set; }

        public HttpPostedFileBase Receipt { get; set; }

        public string ReceiptPath { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }

        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public string Status { get; set; }
        public string Comments { get; set; }

        public string ChangedStatus { get; set; }
    }
}
