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
    
    public partial class sp_GetCustomerProjects_Result
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string ProjectLocation { get; set; }
        public string BookingStatus { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
        public Nullable<int> ClubHouseCharges { get; set; }
        public Nullable<bool> GHMC_Approval { get; set; }
        public string GHMCApprovalDocument { get; set; }
        public Nullable<bool> RERA_Approval { get; set; }
        public string RERAApprovalDocument { get; set; }
        public string Description { get; set; }
        public string LocationURL { get; set; }
    }
}