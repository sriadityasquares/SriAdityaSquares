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
    
    public partial class tblCancellation
    {
        public int ID { get; set; }
        public Nullable<int> ProjectID { get; set; }
        public string Project { get; set; }
        public Nullable<int> TowerID { get; set; }
        public string Tower { get; set; }
        public Nullable<int> FlatID { get; set; }
        public string Flat { get; set; }
        public string CustomerName { get; set; }
        public string AgentName { get; set; }
        public Nullable<int> BookingAmount { get; set; }
        public Nullable<System.DateTime> BookingDate { get; set; }
        public Nullable<System.DateTime> CancelDate { get; set; }
        public string Comments { get; set; }
        public Nullable<System.Guid> BookingID { get; set; }
    }
}
