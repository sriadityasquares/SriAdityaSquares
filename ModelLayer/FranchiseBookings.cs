using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public  class FranchiseBookings
    {
        public int ID { get; set; }
        public Nullable<int> FranchiseID { get; set; }
        public Nullable<int> FranchiseOwnerID { get; set; }
        public string FranchiseOwnerName { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<int> TowerID { get; set; }
        public string TowerName { get; set; }
        public Nullable<int> FlatID { get; set; }
        public string FlatName { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<double> Bhk { get; set; }
        public Nullable<int> Area { get; set; }
        public string Facing { get; set; }
        public Nullable<int> SftRate { get; set; }
        public Nullable<int> HighRiseCharges { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<int> TotalRate { get; set; }
        public Nullable<int> FinalRate { get; set; }
        public Nullable<int> ClubHouseCharges { get; set; }
        public Nullable<int> GrandRate { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<int> BalanceAmount { get; set; }
        public Nullable<System.Guid> BookingID { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
