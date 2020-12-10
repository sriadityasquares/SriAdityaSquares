using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class BookingBL
    {
        BookingDL db = new BookingDL();
        public List<Projects> BindProjects()
        {
            return db.BindProjects();
        }


        public List<Projects> BindClientProjects(string username)
        {
            return db.BindClientProjects(username);
        }

        public List<Projects> BindCustomerProjects(string username)
        {
            return db.BindCustomerProjects(username);
        }
        public List<Projects> BindProjectsBasedOnLocation(string locationName)
        {
            return db.BindProjectsBasedOnLocation(locationName);
        }
        public Projects BindProjectDetails(string ProjectName)
        {
            return db.BindProjectDetails(ProjectName);
        }
        public List<Projects> BindAllProjects()
        {
            return db.BindAllProjects();
        }

        public List<Towers> BindTowers(int projectID)
        {
            return db.BindTowers(projectID);
        }
        public List<Towers> BindCustomerTowers(string username)
        {
            return db.BindCustomerTowers(username);
        }
        public List<Flats> BindFlats(int towerID)
        {
            return db.BindFlats(towerID);
        }

        public List<Flats> BindAllFlats(int towerID)
        {
            return db.BindAllFlats(towerID);
        }

        public List<Flats> BindAllFlats()
        {
            return db.BindAllFlats();
        }
        public List<Towers> BindTowersInProgress(int projectID)
        {
            return db.BindTowersInProgress(projectID);
        }

        public List<Flats> BindFlatsInProgress(int towerID)
        {
            return db.BindFlatsInProgress(towerID);
        }

        public List<Flats> BindFlatsExceptOpen(int towerID)
        {
            return db.BindFlatsExceptOpen(towerID);
        }

        public List<Flats> BindCustomerFlats(string username)
        {
            return db.BindCustomerFlats(username);
        }

        public List<AgentProjectLevel> BindProjectAgents(int projectID)
        {
            return db.BindProjectAgents(projectID);
        }

        public List<Schemes> BindSchemes(int projectID)
        {
            return db.BindSchemes(projectID);
        }

        public List<FlatDetails> BindFlatDetails(int FlatId, int ProjectID)
        {
            return db.BindFlatDetails(FlatId, ProjectID);
        }

        public List<GetGraphicalPeriodWiseBooking> GetAgentPercentagesByProject(int projectID)
        {
            return db.GetAgentPercentagesByProject(projectID);
        }
        public bool SaveNewBooking(BookingInformation b)
        {
            return db.SaveNewBooking(b);
        }

        public bool UpdateBooking(BookingInformation b)
        {
            return db.UpdateBooking(b);
        }
        public List<GetChequeInfo> GetChequeInfo()
        {
            return db.GetChequeInfo();
        }

        public bool UpdateChequeInfo(GetChequeInfo gci)
        {
            return db.UpdateChequeInfo(gci);
        }

        public bool CancelBooking(int flatId, string comments)
        {
            return db.CancelBooking(flatId, comments);
        }

        public List<Cancellation> GetCancellations()
        {
            return db.GetCancellations();
        }
        public bool UpdateCancellation(string comments, int id)
        {
            return db.UpdateCancellation(comments, id);
        }

        public bool UpdatePaymentDetails(int payID,string details)
        {
            return db.UpdatePaymentDetails(payID, details);
        }

        public List<PaymentInformation> BindPaymentDetails(int FlatId)
        {
            return db.BindPaymentDetails(FlatId);
        }

        public List<AgentPaymentInformation> BindAgentPaymentDetails(int FlatId)
        {
            return db.BindAgentPaymentDetails(FlatId);
        }

        public bool SaveNewPayment(PaymentInformation payInfo)
        {
            return db.SaveNewPayment(payInfo);
        }

        public bool SaveNewAgentPayment(AgentPaymentInformation payInfo)
        {
            return db.SaveNewAgentPayment(payInfo);
        }

        public bool SaveNewAgentTotalPayment(AgentTotalPayments payInfo)
        {
            return db.SaveNewAgentTotalPayment(payInfo);
        }
        public List<GetTowerDetails> BindTowerDetails()
        {
            return db.BindTowerDetails();
        }

        public List<Schemes> BindSchemeDetails()
        {
            return db.BindSchemeDetails();
        }
        public List<AgentMaster> BindAgents()
        {
            return db.BindAgents();
        }

        public List<FlatWiseAgentCommission> BindAgentDashboard(string email)
        {
            return db.BindAgentDashboard(email);
        }

        public List<GetFlatLifeCycle> BindFlatLifeCycle(int flatID)
        {
            return db.BindFlatLifeCycle(flatID);
        }
        public List<FlatWiseAgentCommission> BindAgentsDashboard(string email)
        {
            return db.BindAgentsDashboard(email);
        }

        public List<TreeObject> GetAgentGraphicalHierarchy(string email)
        {
            return db.GetAgentGraphicalHierarchy(email);
        }

        public List<GetAgentFlatWiseCommissionByLogin> BindFlatWiseAgentsCommissionByLogins(string email)
        {
            return db.BindFlatWiseAgentsCommissionByLogins(email);
        }

        public BookingInformation GetBookingInformation(int FlatID)
        {
            return db.GetBookingInformation(FlatID);
        }

        public GetPaymentsDetails GetPaymentInformation(int paymentID)
        {
            return db.GetPaymentInformation(paymentID);
        }

        public List<AgentProjectLevel> BindAgentProjectLevels()
        {
            return db.BindAgentProjectLevels();
        }

        public List<LevelsMaster> BindLevelsMasters()
        {
            return db.BindLevelsMasters();
        }

        public AgentTotalPayments GetAgentTotalPay(int AgentID)
        {
            return db.GetAgentTotalPay(AgentID);
        }

        public List<GetAgentLocations> GetAgentLocations()
        {
            return db.GetAgentLocations();
        }

        public List<FlatWiseAgentCommission> BindFlatWiseAgentCommission(int flatID)
        {
            return db.BindFlatWiseAgentCommission(flatID);
        }

        public bool UpdateAgentPayment(FlatWiseAgentCommission fwac)
        {
            return db.UpdateAgentPayment(fwac);
        }

        public bool UploadSelfie(CustomerVisitInfo cvi)
        {
            return db.UploadSelfie(cvi);
        }

        public bool CheckDuplicateMobile(CustomerVisitInfo cvi)
        {
            return db.CheckDuplicateMobile(cvi);
        }
        public List<CustomerVisitInfo> GetSelfies(int projectID, string mobile)
        {
            return db.GetSelfies(projectID, mobile);
        }

        public bool AddSiteVisit(SiteVisitInfo svi,string username)
        {
            return db.AddSiteVisit(svi, username);
        }

        public bool AddClientPayments(ClientPayments cp)
        {
            return db.AddClientPayments(cp);
        }

        public bool AddDailyExpense(DailyExpense de)
        {
            return db.AddDailyExpense(de);
        }

        public bool AddAgreement(Agreements a)
        {
            return db.AddAgreement(a);
        }

        public List<GetMySiteVisits> GetMySiteVisits(string username)
        {
            return db.GetMySiteVisits(username);
        }


        public List<ClientPayments> GetClientPayments()
        {
            return db.GetClientPayments();
        }

        public List<DailyExpense> GetDailyExpenses()
        {
            return db.GetDailyExpenses();
        }

        public List<Agreements> GetAgreements()
        {
            return db.GetAgreements();
        }

        public bool DeleteAgreement(int id)
        {
            return db.DeleteAgreement(id);
        }

        public bool DeleteDailyExpense(int id)
        {
            return db.DeleteDailyExpense(id);
        }

        public bool DeleteClientPayment(int id)
        {
            return db.DeleteClientPayment(id);
        }

        public List<SiteVisitInfo> GetSiteVisitsForApproval()
        {
            return db.GetSiteVisitsForApproval();
        }

        public bool UpdateSiteVisitApproval(SiteVisitInfo svi,string username)
        {
            return db.UpdateSiteVisitApproval(svi, username);
        }

        public List<GetBookingStatistics> BindBookingStatistics(int towerID)
        {
            return db.BindBookingStatistics(towerID);
        }

        public List<Amenity> GetProjectAmenities(string projectName)
        {
            return db.GetProjectAmenities(projectName);
        }

        public List<GetGraphicalPeriodWiseBooking> GetAgentBookingGraph(string username, string projectID, string fromDate, string toDate)
        {
            return db.GetAgentBookingGraph(username, projectID, fromDate, toDate);
        }

        public void SaveCustomerInquiry(CustomerEnquiry ce)
        {
            db.SaveCustomerInquiry(ce);
        }

        public List<GetBookingStatistics> BindSchemeBasedBookings(int projectID)
        {
            return db.BindSchemeBasedBookings(projectID);
        }
    }
}
