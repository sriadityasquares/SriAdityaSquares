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
    
    public partial class sp_GetCancelledFlatsInfo_Result
    {
        public Nullable<int> FlatID { get; set; }
        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public string FlatName { get; set; }
        public string CustomerName { get; set; }
        public Nullable<int> Area { get; set; }
        public Nullable<int> SftRate { get; set; }
        public Nullable<int> Discount { get; set; }
        public string AgentName { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<int> GrandRate { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<int> PaymentID { get; set; }
        public Nullable<System.DateTime> PaidDate { get; set; }
        public Nullable<int> Payments { get; set; }
        public Nullable<int> PaymentBalance { get; set; }
    }
}
