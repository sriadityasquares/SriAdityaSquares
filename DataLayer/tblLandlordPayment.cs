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
    using System.Collections.Generic;
    
    public partial class tblLandlordPayment
    {
        public int PaymentID { get; set; }
        public Nullable<int> LandlordID { get; set; }
        public string LandlordName { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<int> AmountPaid { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string PaymentMode { get; set; }
        public string ReferenceNo { get; set; }
        public string Comments { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
