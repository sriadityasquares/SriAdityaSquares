//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataLayer
{
    using System;
    
    public partial class sp_GetPaymentsForCancelledFlats_Result
    {
        public Nullable<int> PaymentID { get; set; }
        public Nullable<System.Guid> BookingID { get; set; }
        public Nullable<int> FlatID { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<int> BalanceAmount { get; set; }
        public string PaymentModeID { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public Nullable<int> Aadhar { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }
        public string ChequeStatus { get; set; }
        public Nullable<System.DateTime> ChequeDate { get; set; }
        public string Details { get; set; }
    }
}
