﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class salesDBEntities : DbContext
    {
        public salesDBEntities()
            : base("name=salesDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tblCity> tblCities { get; set; }
        public virtual DbSet<tblCountry> tblCountries { get; set; }
        public virtual DbSet<tblState> tblStates { get; set; }
        public virtual DbSet<tblTower> tblTowers { get; set; }
        public virtual DbSet<tblMonth> tblMonths { get; set; }
        public virtual DbSet<tblYear> tblYears { get; set; }
        public virtual DbSet<tblStatu> tblStatus { get; set; }
        public virtual DbSet<tblLevelsMaster> tblLevelsMasters { get; set; }
        public virtual DbSet<tblSchemeMaster> tblSchemeMasters { get; set; }
        public virtual DbSet<tblAgentProjectLevel> tblAgentProjectLevels { get; set; }
        public virtual DbSet<tblAgentPaymentInfo> tblAgentPaymentInfoes { get; set; }
        public virtual DbSet<tblFlat> tblFlats { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<tblAgentTotalPayment> tblAgentTotalPayments { get; set; }
        public virtual DbSet<tblFlatWiseAgentCommission> tblFlatWiseAgentCommissions { get; set; }
        public virtual DbSet<tblCustomerVisitInfo> tblCustomerVisitInfoes { get; set; }
        public virtual DbSet<tblSiteVisitInfo> tblSiteVisitInfoes { get; set; }
        public virtual DbSet<tblAgentMaster> tblAgentMasters { get; set; }
        public virtual DbSet<tblAmenity> tblAmenities { get; set; }
        public virtual DbSet<tblClientPayment> tblClientPayments { get; set; }
        public virtual DbSet<tblCustomerInfo> tblCustomerInfoes { get; set; }
        public virtual DbSet<tblCustomerInfo_backup> tblCustomerInfo_backup { get; set; }
        public virtual DbSet<tblDailyExpens> tblDailyExpenses { get; set; }
        public virtual DbSet<tblCancellation> tblCancellations { get; set; }
        public virtual DbSet<tblCustomerEnquiry> tblCustomerEnquiries { get; set; }
        public virtual DbSet<tblChequeStatu> tblChequeStatus { get; set; }
        public virtual DbSet<tblProject> tblProjects { get; set; }
        public virtual DbSet<tblAgreement> tblAgreements { get; set; }
        public virtual DbSet<tblPaymentInfo> tblPaymentInfoes { get; set; }
        public virtual DbSet<tblBookingInformation> tblBookingInformations { get; set; }
        public virtual DbSet<tblSM> tblSMS { get; set; }
    
        public virtual ObjectResult<sp_GetFlatDetails_Result> sp_GetFlatDetails(Nullable<int> flatID, Nullable<int> projectID)
        {
            var flatIDParameter = flatID.HasValue ?
                new ObjectParameter("FlatID", flatID) :
                new ObjectParameter("FlatID", typeof(int));
    
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFlatDetails_Result>("sp_GetFlatDetails", flatIDParameter, projectIDParameter);
        }
    
        public virtual ObjectResult<sp_GetPeriodWiseBookingDetails_Result> sp_GetPeriodWiseBookingDetails(Nullable<int> option, string project, string year, string month, string fromdate, string todate)
        {
            var optionParameter = option.HasValue ?
                new ObjectParameter("Option", option) :
                new ObjectParameter("Option", typeof(int));
    
            var projectParameter = project != null ?
                new ObjectParameter("Project", project) :
                new ObjectParameter("Project", typeof(string));
    
            var yearParameter = year != null ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(string));
    
            var monthParameter = month != null ?
                new ObjectParameter("Month", month) :
                new ObjectParameter("Month", typeof(string));
    
            var fromdateParameter = fromdate != null ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(string));
    
            var todateParameter = todate != null ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetPeriodWiseBookingDetails_Result>("sp_GetPeriodWiseBookingDetails", optionParameter, projectParameter, yearParameter, monthParameter, fromdateParameter, todateParameter);
        }
    
        public virtual ObjectResult<sp_getBookingInfoByDates_Result> sp_getBookingInfoByDates(string fromdate, string todate, string projectid)
        {
            var fromdateParameter = fromdate != null ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(string));
    
            var todateParameter = todate != null ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(string));
    
            var projectidParameter = projectid != null ?
                new ObjectParameter("projectid", projectid) :
                new ObjectParameter("projectid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getBookingInfoByDates_Result>("sp_getBookingInfoByDates", fromdateParameter, todateParameter, projectidParameter);
        }
    
        public virtual ObjectResult<sp_GetTowerDetails_Result> sp_GetTowerDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetTowerDetails_Result>("sp_GetTowerDetails");
        }
    
        public virtual ObjectResult<sp_GetAgentWiseBookingDetails_Result> sp_GetAgentWiseBookingDetails(string project, string fromdate, string todate)
        {
            var projectParameter = project != null ?
                new ObjectParameter("Project", project) :
                new ObjectParameter("Project", typeof(string));
    
            var fromdateParameter = fromdate != null ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(string));
    
            var todateParameter = todate != null ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentWiseBookingDetails_Result>("sp_GetAgentWiseBookingDetails", projectParameter, fromdateParameter, todateParameter);
        }
    
        public virtual ObjectResult<sp_GetBhkWiseBookingDetails_Result> sp_GetBhkWiseBookingDetails(string project, string fromdate, string todate)
        {
            var projectParameter = project != null ?
                new ObjectParameter("Project", project) :
                new ObjectParameter("Project", typeof(string));
    
            var fromdateParameter = fromdate != null ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(string));
    
            var todateParameter = todate != null ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetBhkWiseBookingDetails_Result>("sp_GetBhkWiseBookingDetails", projectParameter, fromdateParameter, todateParameter);
        }
    
        public virtual ObjectResult<sp_GetFacingWiseBookingDetails_Result> sp_GetFacingWiseBookingDetails(string project, string fromdate, string todate)
        {
            var projectParameter = project != null ?
                new ObjectParameter("Project", project) :
                new ObjectParameter("Project", typeof(string));
    
            var fromdateParameter = fromdate != null ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(string));
    
            var todateParameter = todate != null ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFacingWiseBookingDetails_Result>("sp_GetFacingWiseBookingDetails", projectParameter, fromdateParameter, todateParameter);
        }
    
        public virtual ObjectResult<sp_GetAgentsByProjectID_Result> sp_GetAgentsByProjectID(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentsByProjectID_Result>("sp_GetAgentsByProjectID", projectIDParameter);
        }
    
        public virtual ObjectResult<sp_GetFlatWiseTotalAgentCommission_Result> sp_GetFlatWiseTotalAgentCommission(Nullable<int> projectID, Nullable<int> towerID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            var towerIDParameter = towerID.HasValue ?
                new ObjectParameter("TowerID", towerID) :
                new ObjectParameter("TowerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFlatWiseTotalAgentCommission_Result>("sp_GetFlatWiseTotalAgentCommission", projectIDParameter, towerIDParameter);
        }
    
        public virtual ObjectResult<sp_GetAgentCommissionByAgentLogin_Result> sp_GetAgentCommissionByAgentLogin(string agentEmail)
        {
            var agentEmailParameter = agentEmail != null ?
                new ObjectParameter("AgentEmail", agentEmail) :
                new ObjectParameter("AgentEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentCommissionByAgentLogin_Result>("sp_GetAgentCommissionByAgentLogin", agentEmailParameter);
        }
    
        public virtual ObjectResult<sp_GetAgentCommissionByAgentLogins_Result> sp_GetAgentCommissionByAgentLogins(string agentEmail)
        {
            var agentEmailParameter = agentEmail != null ?
                new ObjectParameter("AgentEmail", agentEmail) :
                new ObjectParameter("AgentEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentCommissionByAgentLogins_Result>("sp_GetAgentCommissionByAgentLogins", agentEmailParameter);
        }
    
        public virtual ObjectResult<sp_GetPercentages_Result> sp_GetPercentages()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetPercentages_Result>("sp_GetPercentages");
        }
    
        public virtual ObjectResult<Nullable<int>> sp_GetAgentPayBalance(Nullable<int> agentID)
        {
            var agentIDParameter = agentID.HasValue ?
                new ObjectParameter("AgentID", agentID) :
                new ObjectParameter("AgentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("sp_GetAgentPayBalance", agentIDParameter);
        }
    
        public virtual ObjectResult<sp_GetAgentLocations_Result> sp_GetAgentLocations()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentLocations_Result>("sp_GetAgentLocations");
        }
    
        public virtual ObjectResult<sp_GetAgentCommissionNdBalanceByAgentLogins_Result> sp_GetAgentCommissionNdBalanceByAgentLogins(string agentEmail)
        {
            var agentEmailParameter = agentEmail != null ?
                new ObjectParameter("AgentEmail", agentEmail) :
                new ObjectParameter("AgentEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentCommissionNdBalanceByAgentLogins_Result>("sp_GetAgentCommissionNdBalanceByAgentLogins", agentEmailParameter);
        }
    
        public virtual ObjectResult<sp_GetUsersWithRoles_Result> sp_GetUsersWithRoles()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetUsersWithRoles_Result>("sp_GetUsersWithRoles");
        }
    
        public virtual ObjectResult<sp_GetAgentFlatWiseCommissionByLogin_Result> sp_GetAgentFlatWiseCommissionByLogin(string agentEmail)
        {
            var agentEmailParameter = agentEmail != null ?
                new ObjectParameter("AgentEmail", agentEmail) :
                new ObjectParameter("AgentEmail", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentFlatWiseCommissionByLogin_Result>("sp_GetAgentFlatWiseCommissionByLogin", agentEmailParameter);
        }
    
        public virtual ObjectResult<sp_GetMySiteVisits_Result> sp_GetMySiteVisits(string userName)
        {
            var userNameParameter = userName != null ?
                new ObjectParameter("UserName", userName) :
                new ObjectParameter("UserName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetMySiteVisits_Result>("sp_GetMySiteVisits", userNameParameter);
        }
    
        public virtual ObjectResult<Nullable<long>> sp_GetAgentNumbers(string agentID)
        {
            var agentIDParameter = agentID != null ?
                new ObjectParameter("AgentID", agentID) :
                new ObjectParameter("AgentID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<long>>("sp_GetAgentNumbers", agentIDParameter);
        }
    
        public virtual ObjectResult<Nullable<double>> sp_GetHighestPercetageInProject(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<double>>("sp_GetHighestPercetageInProject", projectIDParameter);
        }
    
        public virtual ObjectResult<sp_GetBookingStatistics_Result> sp_GetBookingStatistics(Nullable<int> towerID)
        {
            var towerIDParameter = towerID.HasValue ?
                new ObjectParameter("TowerID", towerID) :
                new ObjectParameter("TowerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetBookingStatistics_Result>("sp_GetBookingStatistics", towerIDParameter);
        }
    
        public virtual ObjectResult<sp_GetGraphicalPeriodWiseBookingDetails_Result> sp_GetGraphicalPeriodWiseBookingDetails(Nullable<int> option, Nullable<int> project, string year, string month)
        {
            var optionParameter = option.HasValue ?
                new ObjectParameter("Option", option) :
                new ObjectParameter("Option", typeof(int));
    
            var projectParameter = project.HasValue ?
                new ObjectParameter("Project", project) :
                new ObjectParameter("Project", typeof(int));
    
            var yearParameter = year != null ?
                new ObjectParameter("Year", year) :
                new ObjectParameter("Year", typeof(string));
    
            var monthParameter = month != null ?
                new ObjectParameter("Month", month) :
                new ObjectParameter("Month", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetGraphicalPeriodWiseBookingDetails_Result>("sp_GetGraphicalPeriodWiseBookingDetails", optionParameter, projectParameter, yearParameter, monthParameter);
        }
    
        public virtual ObjectResult<sp_GetCustomerFlats_Result> sp_GetCustomerFlats(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetCustomerFlats_Result>("sp_GetCustomerFlats", usernameParameter);
        }
    
        public virtual ObjectResult<sp_GetCustomerTowers_Result> sp_GetCustomerTowers(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetCustomerTowers_Result>("sp_GetCustomerTowers", usernameParameter);
        }
    
        public virtual ObjectResult<sp_GetAgentBookingGraphByAgentLogins_Result> sp_GetAgentBookingGraphByAgentLogins(string agentEmail, string projectID, string fromDate, string toDate)
        {
            var agentEmailParameter = agentEmail != null ?
                new ObjectParameter("AgentEmail", agentEmail) :
                new ObjectParameter("AgentEmail", typeof(string));
    
            var projectIDParameter = projectID != null ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(string));
    
            var fromDateParameter = fromDate != null ?
                new ObjectParameter("fromDate", fromDate) :
                new ObjectParameter("fromDate", typeof(string));
    
            var toDateParameter = toDate != null ?
                new ObjectParameter("toDate", toDate) :
                new ObjectParameter("toDate", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentBookingGraphByAgentLogins_Result>("sp_GetAgentBookingGraphByAgentLogins", agentEmailParameter, projectIDParameter, fromDateParameter, toDateParameter);
        }
    
        public virtual int sp_Cancel_FlatBooking(Nullable<int> flatID, string comments)
        {
            var flatIDParameter = flatID.HasValue ?
                new ObjectParameter("FlatID", flatID) :
                new ObjectParameter("FlatID", typeof(int));
    
            var commentsParameter = comments != null ?
                new ObjectParameter("comments", comments) :
                new ObjectParameter("comments", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Cancel_FlatBooking", flatIDParameter, commentsParameter);
        }
    
        public virtual ObjectResult<sp_GetChequeInfo_Result> sp_GetChequeInfo()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetChequeInfo_Result>("sp_GetChequeInfo");
        }
    
        public virtual ObjectResult<sp_getPaymentInfoByDates_Result> sp_getPaymentInfoByDates(string fromdate, string todate, string projectid)
        {
            var fromdateParameter = fromdate != null ?
                new ObjectParameter("fromdate", fromdate) :
                new ObjectParameter("fromdate", typeof(string));
    
            var todateParameter = todate != null ?
                new ObjectParameter("todate", todate) :
                new ObjectParameter("todate", typeof(string));
    
            var projectidParameter = projectid != null ?
                new ObjectParameter("projectid", projectid) :
                new ObjectParameter("projectid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_getPaymentInfoByDates_Result>("sp_getPaymentInfoByDates", fromdateParameter, todateParameter, projectidParameter);
        }
    
        public virtual ObjectResult<sp_GetFlatLifeCycle_Result> sp_GetFlatLifeCycle(Nullable<int> flatID)
        {
            var flatIDParameter = flatID.HasValue ?
                new ObjectParameter("FlatID", flatID) :
                new ObjectParameter("FlatID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFlatLifeCycle_Result>("sp_GetFlatLifeCycle", flatIDParameter);
        }
    
        public virtual ObjectResult<sp_GetProfile_Result> sp_GetProfile(string role, string email)
        {
            var roleParameter = role != null ?
                new ObjectParameter("Role", role) :
                new ObjectParameter("Role", typeof(string));
    
            var emailParameter = email != null ?
                new ObjectParameter("Email", email) :
                new ObjectParameter("Email", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetProfile_Result>("sp_GetProfile", roleParameter, emailParameter);
        }
    
        public virtual ObjectResult<sp_GetFlatPaymentDetails_Result> sp_GetFlatPaymentDetails()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFlatPaymentDetails_Result>("sp_GetFlatPaymentDetails");
        }
    
        public virtual ObjectResult<sp_GetCustomerProjects_Result> sp_GetCustomerProjects(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetCustomerProjects_Result>("sp_GetCustomerProjects", usernameParameter);
        }
    
        public virtual ObjectResult<sp_GetClientProjects_Result> sp_GetClientProjects(string username)
        {
            var usernameParameter = username != null ?
                new ObjectParameter("Username", username) :
                new ObjectParameter("Username", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetClientProjects_Result>("sp_GetClientProjects", usernameParameter);
        }
    
        public virtual ObjectResult<sp_GetEligibleFlatsForCommission_Result> sp_GetEligibleFlatsForCommission()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetEligibleFlatsForCommission_Result>("sp_GetEligibleFlatsForCommission");
        }
    
        public virtual ObjectResult<sp_GetSchemeBasedBookings_Result> sp_GetSchemeBasedBookings()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetSchemeBasedBookings_Result>("sp_GetSchemeBasedBookings");
        }
    
        public virtual ObjectResult<sp_GetPaymentsDetails_Result> sp_GetPaymentsDetails(Nullable<int> paymentID)
        {
            var paymentIDParameter = paymentID.HasValue ?
                new ObjectParameter("PaymentID", paymentID) :
                new ObjectParameter("PaymentID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetPaymentsDetails_Result>("sp_GetPaymentsDetails", paymentIDParameter);
        }
    
        public virtual ObjectResult<sp_GetFlatsByTowerID_Result> sp_GetFlatsByTowerID(Nullable<int> towerID)
        {
            var towerIDParameter = towerID.HasValue ?
                new ObjectParameter("TowerID", towerID) :
                new ObjectParameter("TowerID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFlatsByTowerID_Result>("sp_GetFlatsByTowerID", towerIDParameter);
        }
    
        public virtual ObjectResult<sp_GetAgentPercentageByProject_Result> sp_GetAgentPercentageByProject(Nullable<int> projectID)
        {
            var projectIDParameter = projectID.HasValue ?
                new ObjectParameter("ProjectID", projectID) :
                new ObjectParameter("ProjectID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetAgentPercentageByProject_Result>("sp_GetAgentPercentageByProject", projectIDParameter);
        }
    
        public virtual ObjectResult<sp_GetBookingDetails_Result> sp_GetBookingDetails(Nullable<int> flatID)
        {
            var flatIDParameter = flatID.HasValue ?
                new ObjectParameter("FlatID", flatID) :
                new ObjectParameter("FlatID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetBookingDetails_Result>("sp_GetBookingDetails", flatIDParameter);
        }
    
        public virtual ObjectResult<sp_GetDueReminders_Result> sp_GetDueReminders()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetDueReminders_Result>("sp_GetDueReminders");
        }
    
        public virtual ObjectResult<sp_GetFlatWiseAgentCommission_Result> sp_GetFlatWiseAgentCommission(Nullable<int> flatID)
        {
            var flatIDParameter = flatID.HasValue ?
                new ObjectParameter("FlatID", flatID) :
                new ObjectParameter("FlatID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_GetFlatWiseAgentCommission_Result>("sp_GetFlatWiseAgentCommission", flatIDParameter);
        }
    }
}
