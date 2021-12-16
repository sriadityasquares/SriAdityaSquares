using AutoMapper;
using log4net;
using ModelLayer;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class BookingDL
    {
        salesDBEntities dbEntity = new salesDBEntities();
        private static readonly ILog log =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<Projects> BindProjects()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Projects> lstProjects = new List<Projects>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblProject, Projects>();
                });
                IMapper mapper = config.CreateMapper();
                lstProjects = mapper.Map<List<tblProject>, List<Projects>>(dbEntity.tblProjects.Where(a => a.BookingStatus == "O").ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstProjects;
        }

        public List<Projects> BindFranchiseProjects(string username)
        {


            List<Projects> lstProjects = new List<Projects>();

            try
            {
                this.dbEntity.Configuration.ProxyCreationEnabled = false;
                var franchise = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == username).FirstOrDefault();
                if (franchise.FranchiseID > 0)
                {
                    //lstCountry = dbEntity.tblProjects.ToList();
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<sp_GetFranchiseProjects_Result, Projects>();
                    });
                    IMapper mapper = config.CreateMapper();
                    lstProjects = mapper.Map<List<sp_GetFranchiseProjects_Result>, List<Projects>>(dbEntity.sp_GetFranchiseProjects(franchise.FranchiseID).ToList()).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstProjects;
        }
        public List<Projects> BindClientProjects(string username)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Projects> lstProjects = new List<Projects>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetClientProjects_Result, Projects>();
                });
                IMapper mapper = config.CreateMapper();
                lstProjects = mapper.Map<List<sp_GetClientProjects_Result>, List<Projects>>(dbEntity.sp_GetClientProjects(username).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstProjects;
        }

        public List<Projects> BindCustomerProjects(string username)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Projects> lstProjects = new List<Projects>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetCustomerProjects_Result, Projects>();
                });
                IMapper mapper = config.CreateMapper();
                lstProjects = mapper.Map<List<sp_GetCustomerProjects_Result>, List<Projects>>(dbEntity.sp_GetCustomerProjects(username).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstProjects;
        }

        public List<Projects> BindProjectsBasedOnLocation(string locationName)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Projects> lstProjects = new List<Projects>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblProject, Projects>();
                });
                IMapper mapper = config.CreateMapper();
                lstProjects = mapper.Map<List<tblProject>, List<Projects>>(dbEntity.tblProjects.Where(a => a.ProjectLocation == locationName && a.BookingStatus == "O").ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstProjects;
        }
        public Projects BindProjectDetails(string projectName)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            Projects Projects = new Projects();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblProject, Projects>();
                });
                IMapper mapper = config.CreateMapper();
                Projects = mapper.Map<tblProject, Projects>(dbEntity.tblProjects.Where(a => a.ProjectName == projectName).FirstOrDefault());
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return Projects;
        }
        public List<Projects> BindAllProjects()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Projects> lstProjects = new List<Projects>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblProject, Projects>();
                });
                IMapper mapper = config.CreateMapper();
                lstProjects = mapper.Map<List<tblProject>, List<Projects>>(dbEntity.tblProjects.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstProjects;
        }
        public List<Towers> BindTowers(int projectID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Towers> lstTowers = new List<Towers>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblTower, Towers>();
                });
                IMapper mapper = config.CreateMapper();
                lstTowers = mapper.Map<List<tblTower>, List<Towers>>(dbEntity.tblTowers.Where(a => a.ProjectID == projectID && a.BookingStatus == "O").ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstTowers;
        }

        public List<Towers> BindCustomerTowers(string username)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Towers> lstTowers = new List<Towers>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetCustomerTowers_Result, Towers>();
                });
                IMapper mapper = config.CreateMapper();
                lstTowers = mapper.Map<List<sp_GetCustomerTowers_Result>, List<Towers>>(dbEntity.sp_GetCustomerTowers(username).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstTowers;
        }

        public List<Towers> BindFranchiseTowers(int projectID, string username)
        {
            List<Towers> lstTowers = new List<Towers>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                this.dbEntity.Configuration.ProxyCreationEnabled = false;
                var franchise = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == username).FirstOrDefault();
                if (franchise.FranchiseID > 0)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<sp_GetFranchiseTowers_Result, Towers>();
                    });
                    IMapper mapper = config.CreateMapper();
                    lstTowers = mapper.Map<List<sp_GetFranchiseTowers_Result>, List<Towers>>(dbEntity.sp_GetFranchiseTowers(franchise.FranchiseID, projectID).ToList()).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstTowers;
        }

        public List<Flats> BindCustomerFlats(string username)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Flats> lstFlats = new List<Flats>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetCustomerFlats_Result, Flats>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlats = mapper.Map<List<sp_GetCustomerFlats_Result>, List<Flats>>(dbEntity.sp_GetCustomerFlats(username).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlats;
        }
        public List<Towers> BindTowersInProgress(int projectID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Towers> lstTowers = new List<Towers>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblTower, Towers>();
                });
                IMapper mapper = config.CreateMapper();
                lstTowers = mapper.Map<List<tblTower>, List<Towers>>(dbEntity.tblTowers.Where(a => a.ProjectID == projectID && a.BookingStatus == "H" || a.BookingStatus == "S").ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstTowers;
        }
        public List<Flats> BindFlats(int towerID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Flats> lstFlats = new List<Flats>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblFlat, Flats>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID && a.BookingStatus == "O" || a.BookingStatus == "H" || a.BookingStatus == "P").ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlats;
        }

        public List<Flats> BindAllFlats(int towerID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Flats> lstFlats = new List<Flats>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetFlatsByTowerID_Result, Flats>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlats = mapper.Map<List<sp_GetFlatsByTowerID_Result>, List<Flats>>(dbEntity.sp_GetFlatsByTowerID(towerID).ToList()).ToList();
                //lstFlats.OrderBy(x)
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlats;
        }

        public List<Flats> BindAllFlats()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Flats> lstFlats = new List<Flats>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblFlat, Flats>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.OrderByDescending(x => x.FlatID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlats;
        }

        public List<Flats> BindFlatsInProgress(int towerID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Flats> lstFlats = new List<Flats>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblFlat, Flats>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID && (a.BookingStatus == "P" || a.BookingStatus == "S" || a.BookingStatus == "C" || a.BookingStatus == "D")).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlats;
        }

        public List<Flats> BindFranchiseFlatsInProgress(int towerID, string username)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Flats> lstFlats = new List<Flats>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var franchise = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == username).FirstOrDefault();
                if (franchise.FranchiseID > 0)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<sp_GetFranchiseFlatsInProgress_Result, Flats>();
                    });
                    IMapper mapper = config.CreateMapper();
                    lstFlats = mapper.Map<List<sp_GetFranchiseFlatsInProgress_Result>, List<Flats>>(dbEntity.sp_GetFranchiseFlatsInProgress(franchise.FranchiseID, towerID).ToList()).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlats;
        }

        public List<Flats> BindFlatsExceptOpen(int towerID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Flats> lstFlats = new List<Flats>();
            try
            {
                //dbEntity.tblAgentMasters.Where(x=>x.AgenteMail == )
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblFlat, Flats>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID && (a.BookingStatus == "P" || a.BookingStatus == "S" || a.BookingStatus == "C")).ToList()).ToList();


            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlats;
        }


        public List<AgentProjectLevel> BindProjectAgents(int projectID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<AgentProjectLevel> lstAgents = new List<AgentProjectLevel>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentsByProjectID_Result, AgentProjectLevel>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentsByProjectID_Result>, List<AgentProjectLevel>>(dbEntity.sp_GetAgentsByProjectID(projectID).OrderBy(x => x.AgentName).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgents;
        }

        public List<Schemes> BindSchemes(int projectID)
        {
            List<Schemes> lstSchemes = new List<Schemes>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblSchemeMaster, Schemes>();
                });
                IMapper mapper = config.CreateMapper();
                lstSchemes = mapper.Map<List<tblSchemeMaster>, List<Schemes>>(dbEntity.tblSchemeMasters.Where(z => z.ProjectID == projectID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstSchemes;
        }
        public List<FlatDetails> BindFlatDetails(int flatID, int ProjectID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<FlatDetails> lstFlatDetails = new List<FlatDetails>();
            try
            {

                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetFlatDetails_Result, FlatDetails>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlatDetails = mapper.Map<List<sp_GetFlatDetails_Result>, List<FlatDetails>>(dbEntity.sp_GetFlatDetails(flatID, ProjectID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlatDetails;
        }


        public List<GetGraphicalPeriodWiseBooking> GetAgentPercentagesByProject(int projectID)
        {
            List<GetGraphicalPeriodWiseBooking> agentPercentages = new List<GetGraphicalPeriodWiseBooking>();
            try
            {

                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentPercentageByProject_Result, GetGraphicalPeriodWiseBooking>();
                });
                IMapper mapper = config.CreateMapper();
                agentPercentages = mapper.Map<List<sp_GetAgentPercentageByProject_Result>, List<GetGraphicalPeriodWiseBooking>>(dbEntity.sp_GetAgentPercentageByProject(projectID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return agentPercentages;
        }


        public bool SaveNewFranchiseBooking(BookingInformation bookingInfo)
        {
            try
            {
                var franchise = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == bookingInfo.CreatedBy).FirstOrDefault();
                bookingInfo.FranchiseID = franchise.FranchiseID;
                //var franchiseOwnerName = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == bookingInfo.CreatedBy).Select(x => x.AgentName).FirstOrDefault();
                //bookingInfo.FranchiseID = dbEntity.tblFranchiseRegistrations.Where(x => x.IBOID == franchiseOwnerID).Select(x => x.RegisterNo).FirstOrDefault();

                var projectType = dbEntity.tblProjects.Where(x => x.ProjectID == bookingInfo.ProjectID).Select(x => x.ProjectType).FirstOrDefault();
                var BSP = dbEntity.tblProjects.Where(x => x.ProjectID == bookingInfo.ProjectID).Select(x => x.BSP).FirstOrDefault();
                var highestPercentage = dbEntity.sp_GetAgentPercentage(1, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                var levelPercentage = dbEntity.sp_GetAgentPercentage(bookingInfo.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                var TotalComm = (bookingInfo.FinalRate * Convert.ToDouble(highestPercentage)) / 100;
                bookingInfo.TotalComm = TotalComm;
                var fRate = bookingInfo.TotalRate - BSP;
                if (Convert.ToDouble(levelPercentage) == Convert.ToDouble(highestPercentage))
                {
                    bookingInfo.SASComm = TotalComm;
                    //bookingInfo.SASComm = TotalComm1;
                    bookingInfo.AgentComm = 0;
                }
                else
                {
                    //bookingInfo.AgentComm = (bookingInfo.FinalRate * Convert.ToDouble(levelPercentage)) / 100;
                    bookingInfo.AgentComm = ((fRate * bookingInfo.Area) * Convert.ToDouble(levelPercentage)) / 100;
                    bookingInfo.SASComm = TotalComm - bookingInfo.AgentComm;
                }
                bookingInfo.SASTDS = (bookingInfo.SASComm * 5) / 100;
                bookingInfo.SASNet = bookingInfo.SASComm - bookingInfo.SASTDS;
                bookingInfo.AgentTDS = (bookingInfo.AgentComm * 5) / 100;
                bookingInfo.AgentNet = bookingInfo.AgentComm - bookingInfo.AgentTDS;
                bookingInfo.BookingID = Guid.NewGuid();

                bookingInfo.Day = Convert.ToDateTime(bookingInfo.CreatedDate).Day;
                bookingInfo.Month = Convert.ToDateTime(bookingInfo.CreatedDate).Month;
                bookingInfo.Year = Convert.ToDateTime(bookingInfo.CreatedDate).Year;
                int noOfDays = Convert.ToInt32(bookingInfo.PaymentTimePeriod);
                bookingInfo.DueDate = DateTime.Now.AddDays(noOfDays);
                //var x = dbEntity.tblSchemeMasters.ToList().Where(y => y.SchemeID.ToString() == bookingInfo.SchemeID).Select(z => z.PaymentPercentage).FirstOrDefault();
                var schemePercentage = dbEntity.tblSchemeMasters.Where(z => z.SchemeID.ToString() == bookingInfo.SchemeID).Select(z => z.PaymentPercentage).FirstOrDefault();
                bookingInfo.SchemePercentage = Convert.ToInt32(schemePercentage);
                //Booking Info
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblBookingInformation_Franchise>();
                });
                tblBookingInformation_Franchise bookingDetails = new tblBookingInformation_Franchise();
                IMapper mapper = config.CreateMapper();
                mapper.Map<BookingInformation, tblBookingInformation_Franchise>(bookingInfo, bookingDetails);

                dbEntity.tblBookingInformation_Franchise.Add(bookingDetails);

                //Customer Info
                var config1 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblCustomerInfo_Franchise>();
                });
                tblCustomerInfo_Franchise customerDetails = new tblCustomerInfo_Franchise();
                IMapper mapper1 = config1.CreateMapper();
                mapper1.Map<BookingInformation, tblCustomerInfo_Franchise>(bookingInfo, customerDetails);
                dbEntity.tblCustomerInfo_Franchise.Add(customerDetails);

                //Payment Info
                var config2 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblPaymentInfo_Franchise>();
                });
                tblPaymentInfo_Franchise paymentDetails = new tblPaymentInfo_Franchise();
                IMapper mapper2 = config2.CreateMapper();
                mapper2.Map<BookingInformation, tblPaymentInfo_Franchise>(bookingInfo, paymentDetails);
                dbEntity.tblPaymentInfo_Franchise.Add(paymentDetails);

                //Agent Payment Info
                bookingInfo.SASNetBalance = bookingInfo.SASNet;
                bookingInfo.AgentNetBalance = bookingInfo.AgentNet;
                bookingInfo.SASNetPaid = 0;
                bookingInfo.AgentNetPaid = 0;

                var config3 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblAgentPaymentInfo_Franchise>();
                });
                tblAgentPaymentInfo_Franchise agentPaymentDetails = new tblAgentPaymentInfo_Franchise();
                IMapper mapper3 = config3.CreateMapper();
                mapper3.Map<BookingInformation, tblAgentPaymentInfo_Franchise>(bookingInfo, agentPaymentDetails);
                dbEntity.tblAgentPaymentInfo_Franchise.Add(agentPaymentDetails);

                //Flat Status Change
                tblFlat flat = dbEntity.tblFlats.Where(x => x.FlatID == bookingInfo.FlatID).FirstOrDefault();
                //var bookingstatus = "";
                //if (bookingInfo.BookingAmount >= (0.25) * bookingInfo.FinalRate)
                //{
                //    bookingstatus = "S";
                //}
                //else
                //    bookingstatus = "P";
                flat.BookingStatus = "F";

                //Flat Wise Agent Commissions
                List<tblFlatWiseAgentCommissions_Franchise> lstFwac = new List<tblFlatWiseAgentCommissions_Franchise>();
                var agentParent = dbEntity.tblAgentMasters.Where(x => x.AgentID == bookingInfo.AgentID).Select(x => x.AgentParent).FirstOrDefault();
                if (!string.IsNullOrEmpty(agentParent))
                {
                    var allAgentList = dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A").ToList();
                    agentParent = agentParent + ',' + bookingInfo.AgentID;
                    var agentList = agentParent.Split(',');
                    foreach (var agent in agentList)
                    {
                        tblFlatWiseAgentCommissions_Franchise fwac = new tblFlatWiseAgentCommissions_Franchise();
                        var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                        fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                        fwac.FlatName = bookingInfo.FlatName;
                        fwac.AgentID = currentAgent.AgentID;
                        fwac.AgentName = currentAgent.AgentName;
                        fwac.AmountPaid = 0;
                        fwac.Discount = 0;
                        fwac.Percentage = dbEntity.sp_GetAgentPercentage(currentAgent.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                        lstFwac.Add(fwac);
                    }
                    lstFwac = lstFwac.OrderBy(x => x.Percentage).ToList();
                    double oldPercentage = 0;
                    foreach (var item in lstFwac)
                    {
                        var difference = item.Percentage - oldPercentage;
                        item.AgentCommission = Convert.ToInt32(((fRate * bookingInfo.Area) * difference) / 100);
                        item.AgentCommission = Convert.ToInt32(item.AgentCommission - (0.05 * item.AgentCommission));
                        item.NetBalance = item.AgentCommission;
                        oldPercentage = Convert.ToDouble(item.Percentage);
                        dbEntity.tblFlatWiseAgentCommissions_Franchise.Add(item);
                    }

                }
                else
                {
                    tblFlatWiseAgentCommissions_Franchise fwac = new tblFlatWiseAgentCommissions_Franchise();
                    //var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                    fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                    fwac.FlatName = bookingInfo.FlatName;
                    fwac.AgentID = Convert.ToInt32(bookingInfo.AgentID);
                    fwac.AgentName = bookingInfo.AgentName;
                    fwac.Percentage = dbEntity.sp_GetAgentPercentage(bookingInfo.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                    fwac.AgentCommission = Convert.ToInt32(((fRate * bookingInfo.Area) * fwac.Percentage) / 100);
                    fwac.AgentCommission = Convert.ToInt32(fwac.AgentCommission - (0.05 * fwac.AgentCommission));
                    fwac.AmountPaid = 0;
                    fwac.Discount = 0;
                    fwac.NetBalance = fwac.AgentCommission;
                    dbEntity.tblFlatWiseAgentCommissions_Franchise.Add(fwac);
                    //lstFwac.Add(fwac);
                }
                // SAS Commission to be added 
                if (bookingInfo.AgentComm != 0)
                {
                    tblFlatWiseAgentCommissions_Franchise fwac = new tblFlatWiseAgentCommissions_Franchise();
                    //var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                    fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                    fwac.FlatName = bookingInfo.FlatName;
                    fwac.AgentID = 1;
                    fwac.AgentName = "SAS";
                    fwac.Percentage = Convert.ToDouble(highestPercentage);
                    fwac.AmountPaid = 0;
                    fwac.Discount = 0;
                    //fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == bookingInfo.Level).Select(y => y.Percentage).FirstOrDefault();
                    fwac.AgentCommission = Convert.ToInt32(bookingInfo.SASNet);
                    fwac.NetBalance = fwac.AgentCommission;
                    dbEntity.tblFlatWiseAgentCommissions_Franchise.Add(fwac);
                }

                dbEntity.SaveChanges();
                //Franchise Booking Table Save



                var config9 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblFranchiseBooking>();
                });
                tblFranchiseBooking tblFranchiseBooking = new tblFranchiseBooking();
                IMapper mapper9 = config9.CreateMapper();
                mapper9.Map<BookingInformation, tblFranchiseBooking>(bookingInfo, tblFranchiseBooking);
                tblFranchiseBooking.FranchiseID = bookingInfo.FranchiseID;
                tblFranchiseBooking.FranchiseOwnerID = franchise.AgentID;
                tblFranchiseBooking.FranchiseOwnerName = franchise.AgentName;
                tblFranchiseBooking.Status = "Pending";
                tblFranchiseBooking.CustomerName = bookingInfo.Name;
                dbEntity.tblFranchiseBookings.Add(tblFranchiseBooking);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool SaveNewBooking(BookingInformation bookingInfo)
        {
            try
            {
                //var franchiseOwnerID = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == bookingInfo.CreatedBy).Select(x => x.AgentID).FirstOrDefault();
                //var franchiseOwnerName = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == bookingInfo.CreatedBy).Select(x => x.AgentName).FirstOrDefault();
                //bookingInfo.FranchiseID = dbEntity.tblFranchiseRegistrations.Where(x => x.IBOID == franchiseOwnerID).Select(x => x.RegisterNo).FirstOrDefault();
                var projectType = dbEntity.tblProjects.Where(x => x.ProjectID == bookingInfo.ProjectID).Select(x => x.ProjectType).FirstOrDefault();
                var BSP = dbEntity.tblProjects.Where(x => x.ProjectID == bookingInfo.ProjectID).Select(x => x.BSP).FirstOrDefault();
                var highestPercentage = dbEntity.sp_GetAgentPercentage(1, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                var levelPercentage = dbEntity.sp_GetAgentPercentage(bookingInfo.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                var TotalComm = (bookingInfo.FinalRate * Convert.ToDouble(highestPercentage)) / 100;
                bookingInfo.TotalComm = TotalComm;
                var fRate = bookingInfo.TotalRate - BSP;
                if (Convert.ToDouble(levelPercentage) == Convert.ToDouble(highestPercentage))
                {
                    bookingInfo.SASComm = TotalComm;
                    //bookingInfo.SASComm = TotalComm1;
                    bookingInfo.AgentComm = 0;
                }
                else
                {
                    //bookingInfo.AgentComm = (bookingInfo.FinalRate * Convert.ToDouble(levelPercentage)) / 100;
                    bookingInfo.AgentComm = ((fRate * bookingInfo.Area) * Convert.ToDouble(levelPercentage)) / 100;
                    bookingInfo.SASComm = TotalComm - bookingInfo.AgentComm;
                }
                bookingInfo.SASTDS = (bookingInfo.SASComm * 5) / 100;
                bookingInfo.SASNet = bookingInfo.SASComm - bookingInfo.SASTDS;
                bookingInfo.AgentTDS = (bookingInfo.AgentComm * 5) / 100;
                bookingInfo.AgentNet = bookingInfo.AgentComm - bookingInfo.AgentTDS;
                bookingInfo.BookingID = Guid.NewGuid();

                bookingInfo.Day = Convert.ToDateTime(bookingInfo.CreatedDate).Day;
                bookingInfo.Month = Convert.ToDateTime(bookingInfo.CreatedDate).Month;
                bookingInfo.Year = Convert.ToDateTime(bookingInfo.CreatedDate).Year;
                int noOfDays = Convert.ToInt32(bookingInfo.PaymentTimePeriod);
                bookingInfo.DueDate = DateTime.Now.AddDays(noOfDays);
                //var x = dbEntity.tblSchemeMasters.ToList().Where(y => y.SchemeID.ToString() == bookingInfo.SchemeID).Select(z => z.PaymentPercentage).FirstOrDefault();
                var schemePercentage = dbEntity.tblSchemeMasters.Where(z => z.SchemeID.ToString() == bookingInfo.SchemeID).Select(z => z.PaymentPercentage).FirstOrDefault();
                bookingInfo.SchemePercentage = Convert.ToInt32(schemePercentage);
                //Booking Info
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblBookingInformation>();
                });
                tblBookingInformation bookingDetails = new tblBookingInformation();
                IMapper mapper = config.CreateMapper();
                mapper.Map<BookingInformation, tblBookingInformation>(bookingInfo, bookingDetails);

                dbEntity.tblBookingInformations.Add(bookingDetails);

                //Customer Info
                var config1 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblCustomerInfo>();
                });
                tblCustomerInfo customerDetails = new tblCustomerInfo();
                IMapper mapper1 = config1.CreateMapper();
                mapper1.Map<BookingInformation, tblCustomerInfo>(bookingInfo, customerDetails);
                dbEntity.tblCustomerInfoes.Add(customerDetails);

                //Payment Info
                var config2 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblPaymentInfo>();
                });
                tblPaymentInfo paymentDetails = new tblPaymentInfo();
                IMapper mapper2 = config2.CreateMapper();
                paymentDetails.ViewReceipt = true;
                mapper2.Map<BookingInformation, tblPaymentInfo>(bookingInfo, paymentDetails);
                dbEntity.tblPaymentInfoes.Add(paymentDetails);

                //Agent Payment Info
                bookingInfo.SASNetBalance = bookingInfo.SASNet;
                bookingInfo.AgentNetBalance = bookingInfo.AgentNet;
                bookingInfo.SASNetPaid = 0;
                bookingInfo.AgentNetPaid = 0;

                var config3 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblAgentPaymentInfo>();
                });
                tblAgentPaymentInfo agentPaymentDetails = new tblAgentPaymentInfo();
                IMapper mapper3 = config3.CreateMapper();
                mapper3.Map<BookingInformation, tblAgentPaymentInfo>(bookingInfo, agentPaymentDetails);
                dbEntity.tblAgentPaymentInfoes.Add(agentPaymentDetails);

                //Flat Status Change
                tblFlat flat = dbEntity.tblFlats.Where(x => x.FlatID == bookingInfo.FlatID).FirstOrDefault();
                var bookingstatus = "";
                if (bookingInfo.BookingAmount >= (0.25) * bookingInfo.FinalRate)
                {
                    bookingstatus = "S";
                }
                else
                {
                    if (bookingInfo.DueAmount > bookingInfo.BookingAmount)
                        bookingstatus = "D";
                    else
                        bookingstatus = "P";
                }

                flat.BookingStatus = bookingstatus;

                //Flat Wise Agent Commissions
                List<tblFlatWiseAgentCommission> lstFwac = new List<tblFlatWiseAgentCommission>();
                var agentParent = dbEntity.tblAgentMasters.Where(x => x.AgentID == bookingInfo.AgentID).Select(x => x.AgentParent).FirstOrDefault();
                if (!string.IsNullOrEmpty(agentParent))
                {
                    var allAgentList = dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A").ToList();
                    agentParent = agentParent + ',' + bookingInfo.AgentID;
                    var agentList = agentParent.Split(',');
                    foreach (var agent in agentList)
                    {
                        tblFlatWiseAgentCommission fwac = new tblFlatWiseAgentCommission();
                        var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                        fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                        fwac.FlatName = bookingInfo.FlatName;
                        fwac.AgentID = currentAgent.AgentID;
                        fwac.AgentName = currentAgent.AgentName;
                        fwac.AmountPaid = 0;
                        fwac.Discount = 0;
                        fwac.Advance = 0;
                        fwac.Comment = "";
                        fwac.Percentage = dbEntity.sp_GetAgentPercentage(currentAgent.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                        lstFwac.Add(fwac);
                    }
                    lstFwac = lstFwac.OrderBy(x => x.Percentage).ToList();
                    double oldPercentage = 0;
                    foreach (var item in lstFwac)
                    {
                        var difference = item.Percentage - oldPercentage;
                        item.AgentCommission = Convert.ToInt32(((fRate * bookingInfo.Area) * difference) / 100);
                        item.AgentCommission = Convert.ToInt32(item.AgentCommission - (0.05 * item.AgentCommission));
                        item.NetBalance = item.AgentCommission;
                        oldPercentage = Convert.ToDouble(item.Percentage);
                        dbEntity.tblFlatWiseAgentCommissions.Add(item);
                    }

                }
                else
                {
                    tblFlatWiseAgentCommission fwac = new tblFlatWiseAgentCommission();
                    //var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                    fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                    fwac.FlatName = bookingInfo.FlatName;
                    fwac.AgentID = Convert.ToInt32(bookingInfo.AgentID);
                    fwac.AgentName = bookingInfo.AgentName;
                    fwac.Percentage = dbEntity.sp_GetAgentPercentage(bookingInfo.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                    fwac.AgentCommission = Convert.ToInt32(((fRate * bookingInfo.Area) * fwac.Percentage) / 100);
                    fwac.AgentCommission = Convert.ToInt32(fwac.AgentCommission - (0.05 * fwac.AgentCommission));
                    fwac.AmountPaid = 0;
                    fwac.Discount = 0;
                    fwac.Advance = 0;
                    fwac.Comment = "";
                    fwac.NetBalance = fwac.AgentCommission;
                    dbEntity.tblFlatWiseAgentCommissions.Add(fwac);
                    //lstFwac.Add(fwac);
                }
                // SAS Commission to be added 
                if (bookingInfo.AgentComm != 0)
                {
                    tblFlatWiseAgentCommission fwac = new tblFlatWiseAgentCommission();
                    //var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                    fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                    fwac.FlatName = bookingInfo.FlatName;
                    fwac.AgentID = 1;
                    fwac.AgentName = "SAS";
                    fwac.Percentage = Convert.ToDouble(highestPercentage);
                    fwac.AmountPaid = 0;
                    fwac.Discount = 0;
                    fwac.Advance = 0;
                    fwac.Comment = "";
                    //fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == bookingInfo.Level).Select(y => y.Percentage).FirstOrDefault();
                    fwac.AgentCommission = Convert.ToInt32(bookingInfo.SASNet);
                    fwac.NetBalance = fwac.AgentCommission;
                    dbEntity.tblFlatWiseAgentCommissions.Add(fwac);
                }

                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }
        public bool UpdateBooking(BookingInformation bookingInfo)
        {
            try
            {

                dbEntity.sp_BookingAuditForUpdate(bookingInfo.BookingID);
                dbEntity.SaveChanges();
                var projectType = dbEntity.tblProjects.Where(x => x.ProjectID == bookingInfo.ProjectID).Select(x => x.ProjectType).FirstOrDefault();
                var BSP = dbEntity.tblProjects.Where(x => x.ProjectID == bookingInfo.ProjectID).Select(x => x.BSP).FirstOrDefault();
                var highestPercentage = dbEntity.sp_GetAgentPercentage(1, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                var levelPercentage = dbEntity.sp_GetAgentPercentage(bookingInfo.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                var TotalComm = (bookingInfo.FinalRate * Convert.ToDouble(highestPercentage)) / 100;
                bookingInfo.TotalComm = TotalComm;
                var fRate = bookingInfo.TotalRate - BSP;
                if (Convert.ToDouble(levelPercentage) == Convert.ToDouble(highestPercentage))
                {
                    bookingInfo.SASComm = TotalComm;
                    //bookingInfo.SASComm = TotalComm1;
                    bookingInfo.AgentComm = 0;

                }
                else
                {
                    //bookingInfo.AgentComm = (bookingInfo.FinalRate * Convert.ToDouble(levelPercentage)) / 100;
                    bookingInfo.AgentComm = ((fRate * bookingInfo.Area) * Convert.ToDouble(levelPercentage)) / 100;
                    bookingInfo.SASComm = TotalComm - bookingInfo.AgentComm;
                }
                bookingInfo.SASTDS = (bookingInfo.SASComm * 5) / 100;
                bookingInfo.SASNet = bookingInfo.SASComm - bookingInfo.SASTDS;
                bookingInfo.AgentTDS = (bookingInfo.AgentComm * 5) / 100;
                bookingInfo.AgentNet = bookingInfo.AgentComm - bookingInfo.AgentTDS;
                //bookingInfo.BookingID = Guid.NewGuid();
                //bookingInfo.CreatedBy = "";
                //bookingInfo.CreatedDate = System.DateTime.Now.Date;
                bookingInfo.Day = Convert.ToDateTime(bookingInfo.CreatedDate).Day;
                bookingInfo.Month = Convert.ToDateTime(bookingInfo.CreatedDate).Month;
                bookingInfo.Year = Convert.ToDateTime(bookingInfo.CreatedDate).Year;
                int noOfDays = Convert.ToInt32(bookingInfo.PaymentTimePeriod);
                bookingInfo.DueDate = DateTime.Now.AddDays(noOfDays);
                var schemePercentage = dbEntity.tblSchemeMasters.Where(z => z.SchemeID.ToString() == bookingInfo.SchemeID).Select(z => z.PaymentPercentage).FirstOrDefault();
                bookingInfo.SchemePercentage = Convert.ToInt32(schemePercentage);
                tblBookingInformation bookingOld = dbEntity.tblBookingInformations.Where(x => x.BookingID == bookingInfo.BookingID).FirstOrDefault();

                //Update Booking Info
                if (bookingOld != null)
                {

                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<BookingInformation, tblBookingInformation>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    });
                    IMapper mapper = config.CreateMapper();
                    //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                    mapper.Map<BookingInformation, tblBookingInformation>(bookingInfo, bookingOld);
                }
                tblCustomerInfo customerOld = dbEntity.tblCustomerInfoes.Where(x => x.BookingID == bookingInfo.BookingID).FirstOrDefault();
                bookingInfo.ID = customerOld.ID;
                customerOld.Pincode = bookingInfo.Pincode;
                //Update Customer Info
                if (customerOld != null)
                {


                    var config1 = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<BookingInformation, tblCustomerInfo>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    });
                    IMapper mapper1 = config1.CreateMapper();
                    //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                    mapper1.Map<BookingInformation, tblCustomerInfo>(bookingInfo, customerOld);
                }
                //int? sum = 0;
                //Update Payment Info
                List<tblPaymentInfo> paymentListOld = dbEntity.tblPaymentInfoes.Where(x => x.BookingID == bookingInfo.BookingID).OrderBy(x => x.PaymentID).ToList();
                for (int i = 0; i < paymentListOld.Count; i++)
                {
                    if (i == 0)
                    {
                        paymentListOld[i].BookingAmount = bookingInfo.BookingAmount;
                        paymentListOld[i].BalanceAmount = bookingInfo.BalanceAmount;
                        paymentListOld[i].CreatedDate = bookingInfo.CreatedDate;
                        paymentListOld[i].Day = Convert.ToDateTime(bookingInfo.CreatedDate).Day;
                        paymentListOld[i].Month = Convert.ToDateTime(bookingInfo.CreatedDate).Month;
                        paymentListOld[i].Year = Convert.ToDateTime(bookingInfo.CreatedDate).Year;
                        paymentListOld[i].PaymentMode = bookingInfo.PaymentMode;
                        paymentListOld[i].PaymentModeID = bookingInfo.PaymentModeID;
                        paymentListOld[i].ReferenceNo = bookingInfo.ReferenceNo;
                        //paymentListOld[i].ChequeStatus = bookingInfo.ChequeStatus;
                        paymentListOld[i].ChequeDate = bookingInfo.ChequeDate;
                    }
                    else
                    {
                        paymentListOld[i].BalanceAmount = paymentListOld[i - 1].BalanceAmount - paymentListOld[i].BookingAmount;
                    }
                }

                //Update Agent Payment Info
                bookingInfo.SASNetBalance = bookingInfo.SASNet;
                bookingInfo.AgentNetBalance = bookingInfo.AgentNet;
                bookingInfo.SASNetPaid = 0;
                bookingInfo.AgentNetPaid = 0;
                List<tblAgentPaymentInfo> agentPaymentListOld = dbEntity.tblAgentPaymentInfoes.Where(x => x.BookingID == bookingInfo.BookingID).OrderBy(x => x.AgentPaymentID).ToList();
                for (int j = 0; j < agentPaymentListOld.Count; j++)
                {
                    if (j == 0)
                    {
                        agentPaymentListOld[j].SASNetPaid = bookingInfo.SASNetPaid;
                        agentPaymentListOld[j].SASNetBalance = bookingInfo.SASNetBalance;
                        agentPaymentListOld[j].AgentNetPaid = bookingInfo.AgentNetPaid;
                        agentPaymentListOld[j].AgentNetBalance = bookingInfo.AgentNetBalance;
                        agentPaymentListOld[j].CreatedDate = bookingInfo.CreatedDate;
                        agentPaymentListOld[j].Day = Convert.ToDateTime(bookingInfo.CreatedDate).Day;
                        agentPaymentListOld[j].Month = Convert.ToDateTime(bookingInfo.CreatedDate).Month;
                        agentPaymentListOld[j].Year = Convert.ToDateTime(bookingInfo.CreatedDate).Year;
                    }
                    else
                    {
                        agentPaymentListOld[j].SASNetBalance = agentPaymentListOld[j - 1].SASNetBalance - agentPaymentListOld[j].SASNetPaid;
                        agentPaymentListOld[j].AgentNetBalance = agentPaymentListOld[j - 1].AgentNetBalance - agentPaymentListOld[j].AgentNetPaid;
                    }
                }

                //Flat Status Change
                tblFlat flat = dbEntity.tblFlats.Where(x => x.FlatID == bookingInfo.FlatID).FirstOrDefault();
                var bookingstatus = "";
                if (bookingInfo.BookingAmount >= (0.25) * bookingInfo.FinalRate)
                {
                    bookingstatus = "S";
                }
                else
                    bookingstatus = "P";
                flat.BookingStatus = bookingstatus;

                //Flat Wise Agent Commissions
                List<tblFlatWiseAgentCommission> lstFwacOld = dbEntity.tblFlatWiseAgentCommissions.Where(x => x.FlatID == bookingInfo.FlatID).ToList();
                //lstFwacOld.RemoveAll(x => x.FlatID == bookingInfo.FlatID);
                dbEntity.tblFlatWiseAgentCommissions.RemoveRange(lstFwacOld);
                //foreach(var item in lstFwacOld)
                //{
                //    dbEntity.tblFlatWiseAgentCommissions.Remove(item);
                //}
                List<tblFlatWiseAgentCommission> lstFwac = new List<tblFlatWiseAgentCommission>();
                var agentParent = dbEntity.tblAgentMasters.Where(x => x.AgentID == bookingInfo.AgentID).Select(x => x.AgentParent).FirstOrDefault();
                if (!string.IsNullOrEmpty(agentParent))
                {
                    var allAgentList = dbEntity.tblAgentMasters.Where(x => x.AgentStatus == "A").ToList();
                    agentParent = agentParent + ',' + bookingInfo.AgentID;
                    var agentList = agentParent.Split(',');
                    foreach (var agent in agentList)
                    {
                        tblFlatWiseAgentCommission fwac = new tblFlatWiseAgentCommission();
                        var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                        fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                        fwac.FlatName = bookingInfo.FlatName;
                        fwac.AgentID = currentAgent.AgentID;
                        fwac.AgentName = currentAgent.AgentName;
                        fwac.AmountPaid = 0;
                        fwac.Discount = 0;
                        fwac.Advance = 0;
                        fwac.Comment = "";
                        fwac.Percentage = dbEntity.sp_GetAgentPercentage(currentAgent.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                        lstFwac.Add(fwac);
                    }
                    lstFwac = lstFwac.OrderBy(x => x.Percentage).ToList();
                    double oldPercentage = 0;
                    foreach (var item in lstFwac)
                    {
                        var difference = item.Percentage - oldPercentage;
                        item.AgentCommission = Convert.ToInt32(((fRate * bookingInfo.Area) * difference) / 100);
                        item.AgentCommission = Convert.ToInt32(item.AgentCommission - (0.05 * item.AgentCommission));
                        item.NetBalance = item.AgentCommission;
                        item.Discount = 0;
                        item.Advance = 0;
                        item.Comment = "";
                        oldPercentage = Convert.ToDouble(item.Percentage);
                        dbEntity.tblFlatWiseAgentCommissions.Add(item);
                    }
                }
                else
                {
                    tblFlatWiseAgentCommission fwac = new tblFlatWiseAgentCommission();
                    //var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                    fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                    fwac.FlatName = bookingInfo.FlatName;
                    fwac.AgentID = Convert.ToInt32(bookingInfo.AgentID);
                    fwac.AgentName = bookingInfo.AgentName;
                    fwac.Percentage = dbEntity.sp_GetAgentPercentage(bookingInfo.AgentID, projectType, Convert.ToDateTime(bookingInfo.BookingDate)).FirstOrDefault();
                    fwac.AgentCommission = Convert.ToInt32(((fRate * bookingInfo.Area) * fwac.Percentage) / 100);
                    fwac.AgentCommission = Convert.ToInt32(fwac.AgentCommission - (0.05 * fwac.AgentCommission));
                    fwac.AmountPaid = 0;
                    fwac.Discount = 0;
                    fwac.Advance = 0;
                    fwac.Comment = "";
                    fwac.NetBalance = fwac.AgentCommission;
                    dbEntity.tblFlatWiseAgentCommissions.Add(fwac);
                    //lstFwac.Add(fwac);
                }

                // SAS Commission to be added 
                if (bookingInfo.AgentComm != 0)
                {
                    tblFlatWiseAgentCommission fwac = new tblFlatWiseAgentCommission();
                    //var currentAgent = allAgentList.Where(x => x.AgentID == Convert.ToInt32(agent)).FirstOrDefault();
                    fwac.FlatID = Convert.ToInt32(bookingInfo.FlatID);
                    fwac.FlatName = bookingInfo.FlatName;
                    fwac.AgentID = 1;
                    fwac.AgentName = "SAS";
                    fwac.Percentage = Convert.ToDouble(highestPercentage);
                    fwac.AmountPaid = 0;
                    fwac.Discount = 0;
                    fwac.Advance = 0;
                    fwac.Comment = "";
                    //fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == bookingInfo.Level).Select(y => y.Percentage).FirstOrDefault();
                    fwac.AgentCommission = Convert.ToInt32(bookingInfo.SASNet);
                    fwac.NetBalance = fwac.AgentCommission;
                    dbEntity.tblFlatWiseAgentCommissions.Add(fwac);
                }
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }


        public List<GetChequeInfo> GetChequeInfo()
        {
            List<GetChequeInfo> lstChequeInfo = new List<GetChequeInfo>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetChequeInfo_Result, GetChequeInfo>();
                });
                IMapper mapper = config.CreateMapper();
                lstChequeInfo = mapper.Map<List<sp_GetChequeInfo_Result>, List<GetChequeInfo>>(dbEntity.sp_GetChequeInfo().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstChequeInfo;
        }

        public bool UpdateChequeInfo(GetChequeInfo gci)
        {
            try
            {
                if (gci.ChequeStatus == "Cancelled" || gci.ChequeStatus == "Bounced")
                {
                    var getPaymentsForBooking = dbEntity.tblPaymentInfoes.Where(x => x.BookingID == gci.BookingID).OrderBy(x => x.CreatedDate).ToList();
                    var totalAmount = getPaymentsForBooking[0].BalanceAmount + getPaymentsForBooking[0].BookingAmount;
                    for (int i = 0; i < getPaymentsForBooking.Count; i++)
                    {
                        if (getPaymentsForBooking[i].PaymentID == gci.PaymentID)
                        {
                            getPaymentsForBooking[i].BookingAmount = 0;
                            getPaymentsForBooking[i].ChequeStatus = gci.ChequeStatus;
                        }
                        if (i == 0)
                            getPaymentsForBooking[i].BalanceAmount = totalAmount - getPaymentsForBooking[i].BookingAmount;
                        else
                            getPaymentsForBooking[i].BalanceAmount = getPaymentsForBooking[i - 1].BalanceAmount - getPaymentsForBooking[i].BookingAmount;
                    }
                    dbEntity.SaveChanges();
                }
                else
                {
                    var updateStatus = dbEntity.tblPaymentInfoes.Where(x => x.PaymentID == gci.PaymentID).FirstOrDefault();
                    updateStatus.ChequeStatus = gci.ChequeStatus;
                    dbEntity.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }
        public bool CancelBooking(int flatID, string comments)
        {
            try
            {
                dbEntity.sp_Cancel_FlatBooking(flatID, comments);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }


        public bool UpdateCancellation(string comments, int id)
        {
            try
            {
                var cancellation = dbEntity.tblCancellations.Where(x => x.ID == id).FirstOrDefault();
                cancellation.Comments = comments;
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool UpdatePaymentDetails(int id, string details, string Ref)
        {
            try
            {
                var payments = dbEntity.tblPaymentInfoes.Where(x => x.PaymentID == id).FirstOrDefault();
                payments.Details = details;
                payments.ReferenceNo = Ref;
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }


        public List<Cancellation> GetCancellations()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<Cancellation> lstCancel = new List<Cancellation>();
            try
            {

                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblCancellation, Cancellation>();
                });
                IMapper mapper = config.CreateMapper();
                lstCancel = mapper.Map<List<tblCancellation>, List<Cancellation>>(dbEntity.tblCancellations.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstCancel;
        }
        public List<PaymentInformation> BindPaymentDetails(int FlatId)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<PaymentInformation> lstPayDetails = new List<PaymentInformation>();
            try
            {

                //var cancelledFlat = dbEntity.tblFlats.Where(x => x.FlatID == FlatId).Select(x => x.BookingStatus).FirstOrDefault();


                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblPaymentInfo, PaymentInformation>();
                });
                IMapper mapper = config.CreateMapper();
                //WHERE ChequeStatus = 'Bounced'
                lstPayDetails = mapper.Map<List<tblPaymentInfo>, List<PaymentInformation>>(dbEntity.tblPaymentInfoes.Where(x => x.FlatID == FlatId && x.ChequeStatus != "Bounced").ToList()).ToList();
                var bookingID = lstPayDetails[0].BookingID;
                lstPayDetails[0].Customer = dbEntity.tblCustomerInfoes.Where(x => x.BookingID == bookingID).Select(x => x.Name).ToList().FirstOrDefault().ToString();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstPayDetails;
        }


        public List<PaymentInformation> BindPaymentDetailsForCancelledFlats(Guid BookingID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<PaymentInformation> lstPayDetails = new List<PaymentInformation>();
            try
            {

                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetPaymentsForCancelledFlats_Result, PaymentInformation>();
                });
                IMapper mapper = config.CreateMapper();
                lstPayDetails = mapper.Map<List<sp_GetPaymentsForCancelledFlats_Result>, List<PaymentInformation>>(dbEntity.sp_GetPaymentsForCancelledFlats(BookingID).ToList()).ToList();
                //var bookingID = lstPayDetails[0].BookingID;
                //lstPayDetails[0].Customer = dbEntity.tblCustomerInfoes.Where(x => x.BookingID == bookingID).Select(x => x.Name).ToList().FirstOrDefault().ToString();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstPayDetails;
        }

        public List<AgentPaymentInformation> BindAgentPaymentDetails(int FlatId)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<AgentPaymentInformation> lstPayDetails = new List<AgentPaymentInformation>();
            try
            {

                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentPaymentInfo, AgentPaymentInformation>();
                });
                IMapper mapper = config.CreateMapper();
                lstPayDetails = mapper.Map<List<tblAgentPaymentInfo>, List<AgentPaymentInformation>>(dbEntity.tblAgentPaymentInfoes.Where(x => x.FlatID == FlatId).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstPayDetails;
        }

        public bool SaveNewPayment(PaymentInformation payInfo)
        {
            try
            {
                //payInfo.CreatedBy = "";
                //payInfo.CreatedDate = System.DateTime.Now.Date;


                payInfo.CreatedDate = Convert.ToDateTime(payInfo.PaymentDate);
                payInfo.Day = Convert.ToDateTime(payInfo.CreatedDate).Day;
                payInfo.Month = Convert.ToDateTime(payInfo.CreatedDate).Month;
                payInfo.Year = Convert.ToDateTime(payInfo.CreatedDate).Year;
                var config2 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PaymentInformation, tblPaymentInfo>();
                });
                tblPaymentInfo paymentDetails = new tblPaymentInfo();
                IMapper mapper2 = config2.CreateMapper();
                mapper2.Map<PaymentInformation, tblPaymentInfo>(payInfo, paymentDetails);
                dbEntity.tblPaymentInfoes.Add(paymentDetails);
                if (payInfo.BalanceAmount == 0)
                {
                    tblFlat flat = dbEntity.tblFlats.Where(x => x.FlatID == payInfo.FlatID).FirstOrDefault();
                    flat.BookingStatus = "S";
                }
                else
                {
                    var dueAmount = dbEntity.tblBookingInformations.Where(x => x.BookingID == payInfo.BookingID).Select(x => x.DueAmount).FirstOrDefault();
                    var paidAmountList = dbEntity.tblPaymentInfoes.Where(x => x.BookingID == payInfo.BookingID).Select(x => x.BookingAmount).ToList();
                    var paidAmount = 0;
                    foreach (var amount in paidAmountList)
                        paidAmount = paidAmount + Convert.ToInt32(amount);
                    if (dueAmount != null)
                    {
                        if(dueAmount < paidAmount)
                        {
                            tblFlat flat = dbEntity.tblFlats.Where(x => x.FlatID == payInfo.FlatID).FirstOrDefault();
                            flat.BookingStatus = "P";
                        }

                    }
                }
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool SaveNewAgentPayment(AgentPaymentInformation payInfo)
        {
            try
            {
                //payInfo.CreatedDate = payInfo.;
                payInfo.Day = Convert.ToDateTime(payInfo.CreatedDate).Day;
                payInfo.Month = Convert.ToDateTime(payInfo.CreatedDate).Month;
                payInfo.Year = Convert.ToDateTime(payInfo.CreatedDate).Year;
                var config2 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AgentPaymentInformation, tblAgentPaymentInfo>();
                });
                tblAgentPaymentInfo paymentDetails = new tblAgentPaymentInfo();
                IMapper mapper2 = config2.CreateMapper();
                mapper2.Map<AgentPaymentInformation, tblAgentPaymentInfo>(payInfo, paymentDetails);
                dbEntity.tblAgentPaymentInfoes.Add(paymentDetails);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool SaveNewAgentTotalPayment(AgentTotalPayments payInfo)
        {
            try
            {

                var config2 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AgentTotalPayments, tblAgentTotalPayment>();
                });
                tblAgentTotalPayment paymentDetails = new tblAgentTotalPayment();
                IMapper mapper2 = config2.CreateMapper();
                mapper2.Map<AgentTotalPayments, tblAgentTotalPayment>(payInfo, paymentDetails);
                dbEntity.tblAgentTotalPayments.Add(paymentDetails);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }
        public List<GetTowerDetails> BindTowerDetails()
        {
            List<GetTowerDetails> lstTowers = new List<GetTowerDetails>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetTowerDetails_Result, GetTowerDetails>();
                });
                IMapper mapper = config.CreateMapper();
                lstTowers = mapper.Map<List<sp_GetTowerDetails_Result>, List<GetTowerDetails>>(dbEntity.sp_GetTowerDetails().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstTowers;
        }

        public List<Schemes> BindSchemeDetails()
        {
            List<Schemes> lstSchemes = new List<Schemes>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblSchemeMaster, Schemes>().ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.SchemeStatus));
                });
                IMapper mapper = config.CreateMapper();
                lstSchemes = mapper.Map<List<tblSchemeMaster>, List<Schemes>>(dbEntity.tblSchemeMasters.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstSchemes;
        }
        public List<AgentMaster> BindAgents()
        {
            List<AgentMaster> lstAgents = new List<AgentMaster>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, AgentMaster>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<tblAgentMaster>, List<AgentMaster>>(dbEntity.tblAgentMasters.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }
            return lstAgents;
        }

        public List<AgentMaster> BindFranchiseAgents(string username)
        {
            List<AgentMaster> lstAgents = new List<AgentMaster>();

            try
            {
                var franchise = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == username).FirstOrDefault();
                if (franchise.FranchiseID > 0)
                {
                    //lstCountry = dbEntity.tblProjects.ToList();
                    var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, AgentMaster>();
                });
                    IMapper mapper = config.CreateMapper();
                    lstAgents = mapper.Map<List<tblAgentMaster>, List<AgentMaster>>(dbEntity.tblAgentMasters.Where(x => x.FranchiseID == franchise.FranchiseID && x.AgenteMail != username).ToList()).ToList();
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }
            return lstAgents;
        }
        public List<AgentMaster> BindTeamAgents(string username)
        {
            List<AgentMaster> lstAgents = new List<AgentMaster>();

            try
            {


                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, AgentMaster>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<tblAgentMaster>, List<AgentMaster>>(dbEntity.tblAgentMasters.Where(x => x.CreatedBy == username).ToList());

            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }
            return lstAgents;
        }
        public List<FlatWiseAgentCommission> BindAgentDashboard(string email)
        {
            List<FlatWiseAgentCommission> lstAgents = new List<FlatWiseAgentCommission>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentCommissionByAgentLogin_Result, FlatWiseAgentCommission>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentCommissionByAgentLogin_Result>, List<FlatWiseAgentCommission>>(dbEntity.sp_GetAgentCommissionByAgentLogin(email).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgents;
        }

        public List<GetFlatLifeCycle> BindFlatLifeCycle(int flatID)
        {
            List<GetFlatLifeCycle> lstFlatLifeCycle = new List<GetFlatLifeCycle>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetFlatLifeCycle_Result, GetFlatLifeCycle>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlatLifeCycle = mapper.Map<List<sp_GetFlatLifeCycle_Result>, List<GetFlatLifeCycle>>(dbEntity.sp_GetFlatLifeCycle(flatID).ToList()).ToList();
                for (int i = 0; i < lstFlatLifeCycle.Count; i++)
                {
                    if (i == 0)
                    {
                        lstFlatLifeCycle[i].FlatEvent = "START";

                    }
                    else
                    {
                        if (lstFlatLifeCycle[i].BalanceAmount == 0)
                        {
                            lstFlatLifeCycle[i].FlatEvent = "END";
                        }
                        else
                        {
                            lstFlatLifeCycle[i].FlatEvent = "PAYMENT " + i;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstFlatLifeCycle;
        }

        public List<FlatWiseAgentCommission> BindAgentsDashboard(string email)
        {
            List<FlatWiseAgentCommission> lstAgents = new List<FlatWiseAgentCommission>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentCommissionNdBalanceByAgentLogins_Result, FlatWiseAgentCommission>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentCommissionNdBalanceByAgentLogins_Result>, List<FlatWiseAgentCommission>>(dbEntity.sp_GetAgentCommissionNdBalanceByAgentLogins(email).ToList()).ToList();
                //lstAgents.IndexOf(lstAgents.Single(i =>i.)
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                ex.ToString();
            }
            return lstAgents;
        }


        public List<TreeObject> GetAgentGraphicalHierarchy(string email)
        {
            List<AgentGraphicalMapping> lstAgents = new List<AgentGraphicalMapping>();
            var result = new List<TreeObject>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentCommissionNdBalanceByAgentLogins_Result, AgentGraphicalMapping>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentCommissionNdBalanceByAgentLogins_Result>, List<AgentGraphicalMapping>>(dbEntity.sp_GetAgentCommissionNdBalanceByAgentLogins(email).OrderBy(x => x.AgentID).ToList()).ToList();

                List<TreeObject> lstTreeObject = new List<TreeObject>();
                foreach (var agent in lstAgents)
                {
                    TreeObject TreeObject = new TreeObject();
                    TreeObject.AgentCode = agent.AgentCode;
                    TreeObject.AgentName = agent.AgentName.Split(' ')[0].ToUpper();
                    TreeObject.AgentSponserCode = agent.AgentSponserCode;
                    TreeObject.colorScheme = "#1696d3";
                    lstTreeObject.Add(TreeObject);
                }
                lstTreeObject = lstTreeObject.GroupBy(x => x.AgentCode).Select(x => x.First()).ToList();
                //var x  = mapper.Map<List<sp_GetAgentCommissionNdBalanceByAgentLogins_Result>, List<TreeObject>>(dbEntity.sp_GetAgentCommissionNdBalanceByAgentLogins(email).ToList()).ToList();
                result = TreeObject.FlatToHierarchy(lstTreeObject).ToList();
            }
            catch (Exception ex)
            {

            }
            return result;
        }
        public List<GetAgentFlatWiseCommissionByLogin> BindFlatWiseAgentsCommissionByLogins(string email)
        {
            List<GetAgentFlatWiseCommissionByLogin> lstAgents = new List<GetAgentFlatWiseCommissionByLogin>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentFlatWiseCommissionByLogin_Result, GetAgentFlatWiseCommissionByLogin>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentFlatWiseCommissionByLogin_Result>, List<GetAgentFlatWiseCommissionByLogin>>(dbEntity.sp_GetAgentFlatWiseCommissionByLogin(email).ToList()).ToList();

            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                ex.ToString();
            }
            return lstAgents;
        }

        public BookingInformation GetBookingInformationForCancelledFlats(Guid bookingID)
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetBookingInfoForCancelledFlats_Result, BookingInformation>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<sp_GetBookingInfoForCancelledFlats_Result, BookingInformation>(dbEntity.sp_GetBookingInfoForCancelledFlats(bookingID).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new BookingInformation();
            }
        }

        public BookingInformation GetBookingInformation(int FlatID)
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetBookingDetails_Result, BookingInformation>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<sp_GetBookingDetails_Result, BookingInformation>(dbEntity.sp_GetBookingDetails(FlatID).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new BookingInformation();
            }
        }
        public GetPaymentsDetails GetPaymentInformation(int paymentID)
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetPaymentsDetails_Result, GetPaymentsDetails>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<sp_GetPaymentsDetails_Result, GetPaymentsDetails>(dbEntity.sp_GetPaymentsDetails(paymentID).FirstOrDefault());
            }
            catch (Exception ex)
            {
                return new GetPaymentsDetails();
            }
        }


        public List<AgentProjectLevel> BindAgentProjectLevels()
        {
            try
            {
                List<AgentProjectLevel> lstAgents = new List<AgentProjectLevel>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentProjectLevels_Result, AgentProjectLevel>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentProjectLevels_Result>, List<AgentProjectLevel>>(dbEntity.sp_GetAgentProjectLevels().ToList()).ToList();
                return lstAgents;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return new List<AgentProjectLevel>();
            }
        }

        public List<LevelsMaster> BindLevelsMasters()
        {
            try
            {
                List<LevelsMaster> lstLevels = new List<LevelsMaster>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblLevelsMaster, LevelsMaster>().ForMember(dest => dest.BookingStatus, opt => opt.MapFrom(src => src.Status));
                });
                IMapper mapper = config.CreateMapper();
                lstLevels = mapper.Map<List<tblLevelsMaster>, List<LevelsMaster>>(dbEntity.tblLevelsMasters.ToList()).ToList();
                return lstLevels;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return new List<LevelsMaster>();
            }
        }

        public AgentTotalPayments GetAgentTotalPay(int AgentID)
        {
            AgentTotalPayments agentPayDetails = new AgentTotalPayments();
            var list = dbEntity.sp_GetAgentPayBalance(AgentID).ToList();
            if (list.Count > 1)
            {
                agentPayDetails.AgentCommission = list[1];
                if (list[0] == null)
                {
                    list[0] = 0;
                }
                agentPayDetails.TotalPaid = list[0];
                agentPayDetails.TotalBalance = list[1] - list[0];
            }
            return agentPayDetails;
        }

        public List<GetAgentLocations> GetAgentLocations()
        {
            List<GetAgentLocations> lstAgentLocations = new List<GetAgentLocations>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentLocations_Result, GetAgentLocations>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgentLocations = mapper.Map<List<sp_GetAgentLocations_Result>, List<GetAgentLocations>>(dbEntity.sp_GetAgentLocations().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgentLocations;
        }

        public List<FlatWiseAgentCommission> BindFlatWiseAgentCommission(int flatID)
        {
            List<FlatWiseAgentCommission> fwac = new List<FlatWiseAgentCommission>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetFlatWiseAgentCommission_Result, FlatWiseAgentCommission>();
                });
                IMapper mapper = config.CreateMapper();
                fwac = mapper.Map<List<sp_GetFlatWiseAgentCommission_Result>, List<FlatWiseAgentCommission>>(dbEntity.sp_GetFlatWiseAgentCommission(flatID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return fwac;
        }


        public bool UpdateAgentPayment(FlatWiseAgentCommission fwac)
        {
            try
            {
                tblFlatWiseAgentCommission fwacOld = dbEntity.tblFlatWiseAgentCommissions.Where(x => x.AgentID == fwac.AgentID && x.FlatID == fwac.FlatID).FirstOrDefault();
                fwacOld.AmountPaid = fwac.AmountPaid;
                fwacOld.Discount = fwac.Discount;
                fwacOld.NetBalance = fwac.AgentCommission - fwac.AmountPaid - fwac.Discount;
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }

        }

        public bool UploadSelfie(CustomerVisitInfo cvi)
        {
            try
            {
                tblCustomerVisitInfo cvInfo = new tblCustomerVisitInfo();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CustomerVisitInfo, tblCustomerVisitInfo>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<CustomerVisitInfo, tblCustomerVisitInfo>(cvi, cvInfo);
                dbEntity.tblCustomerVisitInfoes.Add(cvInfo);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool CheckDuplicateMobile(CustomerVisitInfo cvi)
        {
            int projectid = Convert.ToInt32(cvi.ProjectID);
            var result = dbEntity.tblCustomerVisitInfoes.Where(x => x.ProjectID == projectid && x.CustomerMobile == cvi.CustomerMobile).FirstOrDefault();
            if (result == null)
                return true;
            else
                return false;

        }

        public List<CustomerVisitInfo> GetSelfies(int projectID, string mobile)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<CustomerVisitInfo> lstSelfies = new List<CustomerVisitInfo>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblCustomerVisitInfo, CustomerVisitInfo>();
                });
                IMapper mapper = config.CreateMapper();
                var date = DateTime.Now.Date.AddDays(-30);
                if (mobile == "")
                    lstSelfies = mapper.Map<List<tblCustomerVisitInfo>, List<CustomerVisitInfo>>(dbEntity.tblCustomerVisitInfoes.Where(a => a.ProjectID == projectID && a.DateAdded >= date).ToList()).ToList();
                else
                    lstSelfies = mapper.Map<List<tblCustomerVisitInfo>, List<CustomerVisitInfo>>(dbEntity.tblCustomerVisitInfoes.Where(a => a.ProjectID == projectID && a.CustomerMobile == mobile && a.DateAdded >= date).ToList()).ToList();
                foreach (var item in lstSelfies)
                {
                    string base64String = Convert.ToBase64String(item.Selfie, 0, item.Selfie.Length);
                    item.SelfieURL = "data:image/png;base64," + base64String;
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstSelfies;
        }

        public bool AddSiteVisit(SiteVisitInfo svi, string username)
        {
            try
            {
                tblSiteVisitInfo svInfo = new tblSiteVisitInfo();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<SiteVisitInfo, tblSiteVisitInfo>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<SiteVisitInfo, tblSiteVisitInfo>(svi, svInfo);
                dbEntity.tblSiteVisitInfoes.Add(svInfo);
                dbEntity.SaveChanges();
                string agentIDS = "1," + svi.AgentID + "," + svi.ImmediateSeniorID;
                var agentNumbers = dbEntity.sp_GetAgentNumbers(agentIDS).ToArray();
                var message = "New Site Visit Request" + Environment.NewLine;
                message = message + "Project Name : #Project" + Environment.NewLine + "Agent Name : #Agent" + Environment.NewLine + "Sr. Agent Name : #SrAgent" + Environment.NewLine + "Customer Name : #Customer" + Environment.NewLine + "Customer Mobile : #Mobile" + Environment.NewLine + "Visit Date : #Date" + Environment.NewLine + "From Address : #From" + Environment.NewLine + "To Address : #To" + Environment.NewLine + "Status : #Status";
                message = message.Replace("#Project", svi.ProjectName).Replace("#Agent", svi.AgentName).Replace("#SrAgent", svi.ImmediateSeniorName).Replace("#Customer", svi.CustomerName).Replace("#Mobile", svi.CustomerMobile).Replace("#Date", svi.DateOfVisit).Replace("#From", svi.FromAddress).Replace("#To", svi.ToAddress).Replace("#Status", svi.Status);
                foreach (var agent in agentNumbers)
                {
                    var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + agent + "&smsContentType=english");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Cache-Control", "no-cache");
                    IRestResponse response = client.Execute(request);
                    SMS sms = new SMS();
                    sms.MessageType = "New Site Visit";
                    sms.Message = message;
                    sms.Recipients = agent.ToString();
                    sms.CreatedBy = username;
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    //var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                    sms.CreatedDate = indianTime;
                    CommonDL common = new CommonDL();
                    common.LogSMS(sms);
                }
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }


        public bool AddClientPayments(ClientPayments cp)
        {
            try
            {
                tblClientPayment cpInfo = new tblClientPayment();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ClientPayments, tblClientPayment>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<ClientPayments, tblClientPayment>(cp, cpInfo);
                dbEntity.tblClientPayments.Add(cpInfo);
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool AddDailyExpense(DailyExpense de)
        {
            try
            {
                tblDailyExpens deInfo = new tblDailyExpens();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DailyExpense, tblDailyExpens>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<DailyExpense, tblDailyExpens>(de, deInfo);
                dbEntity.tblDailyExpenses.Add(deInfo);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool AddAgreement(Agreements a)
        {
            try
            {
                tblAgreement aeInfo = new tblAgreement();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Agreements, tblAgreement>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<Agreements, tblAgreement>(a, aeInfo);
                dbEntity.tblAgreements.Add(aeInfo);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }
        public List<ClientPayments> GetClientPayments()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblClientPayment, ClientPayments>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblClientPayment>, List<ClientPayments>>(dbEntity.tblClientPayments.ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<ClientPayments>();
            }
        }

        public List<DailyExpense> GetDailyExpenses()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblDailyExpens, DailyExpense>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblDailyExpens>, List<DailyExpense>>(dbEntity.tblDailyExpenses.OrderByDescending(x => x.ExpenseID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<DailyExpense>();
            }
        }

        public bool UpdateDailyExpenses(DailyExpense de)
        {
            try
            {
                var oldDailyExpense = dbEntity.tblDailyExpenses.Where(x => x.ExpenseID == de.ExpenseID).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DailyExpense, tblDailyExpens>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<DailyExpense, tblDailyExpens>(de, oldDailyExpense);

                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public List<Agreements> GetAgreements()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgreement, Agreements>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblAgreement>, List<Agreements>>(dbEntity.tblAgreements.ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<Agreements>();
            }
        }

        public bool DeleteAgreement(int id)
        {
            try
            {
                var agreement = dbEntity.tblAgreements.Where(x => x.AgreementID == id).FirstOrDefault();
                dbEntity.tblAgreements.Remove(agreement);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteClientPayment(int id)
        {
            try
            {
                var cp = dbEntity.tblClientPayments.Where(x => x.ClientPayID == id).FirstOrDefault();
                dbEntity.tblClientPayments.Remove(cp);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteDailyExpense(int id)
        {
            try
            {
                var de = dbEntity.tblDailyExpenses.Where(x => x.ExpenseID == id).FirstOrDefault();
                dbEntity.tblDailyExpenses.Remove(de);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<GetMySiteVisits> GetMySiteVisits(string username)
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetMySiteVisits_Result, GetMySiteVisits>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<sp_GetMySiteVisits_Result>, List<GetMySiteVisits>>(dbEntity.sp_GetMySiteVisits(username).ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<GetMySiteVisits>();
            }
        }



        public List<SiteVisitInfo> GetSiteVisitsForApproval()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblSiteVisitInfo, SiteVisitInfo>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblSiteVisitInfo>, List<SiteVisitInfo>>(dbEntity.tblSiteVisitInfoes.Where(x => x.Status == "Pending").ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<SiteVisitInfo>();
            }
        }

        public bool UpdateSiteVisitApproval(SiteVisitInfo svi, string username)
        {
            try
            {
                //p.BookingStatusName = null;
                tblSiteVisitInfo old = dbEntity.tblSiteVisitInfoes.Where(x => x.ID == svi.ID).FirstOrDefault();
                if (old != null)
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<SiteVisitInfo, tblSiteVisitInfo>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    });
                    IMapper mapper = config.CreateMapper();
                    //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                    mapper.Map<SiteVisitInfo, tblSiteVisitInfo>(svi, old);
                    dbEntity.SaveChanges();
                    string agentIDS = "1," + svi.AgentID + "," + svi.ImmediateSeniorID;
                    if (svi.isApproved == true)
                        agentIDS = agentIDS + "," + svi.CustomerMobile;
                    var agentNumbers = dbEntity.sp_GetAgentNumbers(agentIDS).ToArray();
                    var message = "Site Visit Request Status" + Environment.NewLine;
                    message = message + "Project Name : #Project" + Environment.NewLine + "Agent Name : #Agent" + Environment.NewLine + "Sr. Agent Name : #SrAgent" + Environment.NewLine + "Customer Name : #Customer" + Environment.NewLine + "Customer Mobile : #Mobile" + Environment.NewLine + "Visit Date : #Date" + Environment.NewLine + "From Address : #From" + Environment.NewLine + "To Address : #To" + Environment.NewLine + "Status : #Status";
                    message = message.Replace("#Project", svi.ProjectName).Replace("#Agent", svi.AgentName).Replace("#SrAgent", svi.ImmediateSeniorName).Replace("#Customer", svi.CustomerName).Replace("#Mobile", svi.CustomerMobile).Replace("#Date", svi.DateOfVisit).Replace("#From", svi.FromAddress).Replace("#To", svi.ToAddress).Replace("#Status", svi.Status);
                    foreach (var agent in agentNumbers)
                    {
                        var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + agent + "&smsContentType=english");
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("Cache-Control", "no-cache");
                        IRestResponse response = client.Execute(request);

                        SMS sms = new SMS();
                        sms.MessageType = "Site Visit Approvals";
                        sms.Message = message;
                        sms.Recipients = agent.ToString();
                        sms.CreatedBy = username;
                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                        //var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                        sms.CreatedDate = indianTime;
                        CommonDL common = new CommonDL();
                        common.LogSMS(sms);
                    }
                }
                return true;

            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public List<GetBookingStatistics> BindBookingStatistics(int towerID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<GetBookingStatistics> lstBookingStats = new List<GetBookingStatistics>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetBookingStatistics_Result, GetBookingStatistics>();
                });
                IMapper mapper = config.CreateMapper();
                lstBookingStats = mapper.Map<List<sp_GetBookingStatistics_Result>, List<GetBookingStatistics>>(dbEntity.sp_GetBookingStatistics(towerID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstBookingStats;
        }

        public List<GetBookingStatistics> BindSchemeBasedBookings(int projectID)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<GetBookingStatistics> lstBookingStats = new List<GetBookingStatistics>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetSchemeBasedBookings_Result, GetBookingStatistics>();
                });
                IMapper mapper = config.CreateMapper();
                lstBookingStats = mapper.Map<List<sp_GetSchemeBasedBookings_Result>, List<GetBookingStatistics>>(dbEntity.sp_GetSchemeBasedBookings().Where(x => x.ProjectID == projectID).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstBookingStats;
        }
        public List<Amenity> GetProjectAmenities(string projectName)
        {
            List<Amenity> lstAmenities = new List<Amenity>();
            try
            {
                var projectID = dbEntity.tblProjects.Where(x => x.ProjectName == projectName).Select(x => x.ProjectID).FirstOrDefault();


                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAmenity, Amenity>();
                });
                IMapper mapper = config.CreateMapper();
                lstAmenities = mapper.Map<List<tblAmenity>, List<Amenity>>(dbEntity.tblAmenities.Where(x => x.ProjectID == projectID).ToList()).ToList();
            }

            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAmenities;
        }

        public List<ProjectTransactions> GetProjectTransactions()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<ProjectTransactions> lstProTransactions = new List<ProjectTransactions>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_ProjectTransactions_Result, ProjectTransactions>();
                });
                IMapper mapper = config.CreateMapper();
                lstProTransactions = mapper.Map<List<sp_ProjectTransactions_Result>, List<ProjectTransactions>>(dbEntity.sp_ProjectTransactions().ToList()).ToList();

            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstProTransactions;
        }
        public List<GetGraphicalPeriodWiseBooking> GetAgentBookingGraph(string username, string projectid, string fromDate, string toDate)
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentBookingGraphByAgentLogins_Result, GetGraphicalPeriodWiseBooking>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<sp_GetAgentBookingGraphByAgentLogins_Result>, List<GetGraphicalPeriodWiseBooking>>(dbEntity.sp_GetAgentBookingGraphByAgentLogins(username, projectid, fromDate, toDate).ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<GetGraphicalPeriodWiseBooking>();
            }
        }

        public int? GetFranchiseNoWithPaymentID(int payID)
        {
            try
            {
                var bookingID = dbEntity.tblPaymentInfoes.Where(x => x.PaymentID == payID).Select(x => x.BookingID).FirstOrDefault();
                return dbEntity.tblBookingInformations.Where(x => x.BookingID == bookingID).Select(x => x.FranchiseID).FirstOrDefault();
            }
            catch
            {
                return 0;
            }
        }

        public int? GetFranchiseNoWithFlatID(int flatID)
        {
            try
            {
                //var bookingID = dbEntity.tblPaymentInfoes.Where(x => x.PaymentID == payID).Select(x => x.BookingID).FirstOrDefault();
                return dbEntity.tblBookingInformations.Where(x => x.FlatID == flatID).Select(x => x.FranchiseID).FirstOrDefault();
            }
            catch
            {
                return 0;
            }
        }

        public void SaveCustomerInquiry(CustomerEnquiry ce)
        {
            try
            {
                tblCustomerEnquiry ceInfo = new tblCustomerEnquiry();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<CustomerEnquiry, tblCustomerEnquiry>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<CustomerEnquiry, tblCustomerEnquiry>(ce, ceInfo);
                dbEntity.tblCustomerEnquiries.Add(ceInfo);
                dbEntity.SaveChanges();

            }
            catch (Exception ex)
            {

            }
        }

        public DashboardParameters BindDashboardParameters()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetDashboardParameters_Result, DashboardParameters>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<sp_GetDashboardParameters_Result, DashboardParameters>(dbEntity.sp_GetDashboardParameters().FirstOrDefault());

                //return mapper.Map<sp_GetDashboardParameters_Result>,<DashboardParameters>(dbEntity.sp_GetDashboardParameters().FirstOrDefault()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new DashboardParameters();
            }
        }

        public List<GetTopIBOInMonth> BindTopIBO(bool SAS)
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<GetTopIBOInMonth> lstTopIBO = new List<GetTopIBOInMonth>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetTopIBOInMonth_Result, GetTopIBOInMonth>();
                });
                IMapper mapper = config.CreateMapper();
                lstTopIBO = mapper.Map<List<sp_GetTopIBOInMonth_Result>, List<GetTopIBOInMonth>>(dbEntity.sp_GetTopIBOInMonth(SAS).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstTopIBO;
        }

        public List<AgentMaster> BindRecentAgents()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<AgentMaster> lstAgents = new List<AgentMaster>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblAgentMaster, AgentMaster>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<tblAgentMaster>, List<AgentMaster>>(dbEntity.tblAgentMasters.OrderByDescending(x => x.CreatedDate).Take(5).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstAgents;
        }

        public List<BookingInformation> BindRecentBooking()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<BookingInformation> lstBooking = new List<BookingInformation>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblBookingInformation, BookingInformation>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<tblBookingInformation>, List<BookingInformation>>(dbEntity.tblBookingInformations.OrderByDescending(x => x.CreatedDate).Take(5).ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            foreach (var booking in lstBooking)
            {
                booking.FormattedDate = Convert.ToDateTime(booking.CreatedDate).Date.ToString("dd/MM/yyyy");
                //booking.AGENT
            }
            return lstBooking;
        }

        public CustomerInfo GetCustomerInfo(Guid? bookingID)
        {
            //return dbEntity.tblCustomerInfoes.Where(x => x.BookingID == bookingID).FirstOrDefault();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<tblCustomerInfo, CustomerInfo>();
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<tblCustomerInfo, CustomerInfo>(dbEntity.tblCustomerInfoes.Where(x => x.BookingID == bookingID).FirstOrDefault());

        }
        public List<BookingInformation> BindRecentPayments()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<BookingInformation> lstBooking = new List<BookingInformation>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetRecentPayments_Result, BookingInformation>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<sp_GetRecentPayments_Result>, List<BookingInformation>>(dbEntity.sp_GetRecentPayments().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstBooking;
        }

        public List<DailyExpense> BindRecentExpenses()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<DailyExpense> lstBooking = new List<DailyExpense>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetRecentExpenses_Result, DailyExpense>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<sp_GetRecentExpenses_Result>, List<DailyExpense>>(dbEntity.sp_GetRecentExpenses().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstBooking;
        }

        public List<GetGraphicalPeriodWiseBooking> BindRecentBookingGraph()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<GetGraphicalPeriodWiseBooking> lstBooking = new List<GetGraphicalPeriodWiseBooking>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetRecentBookingGraph_Result, GetGraphicalPeriodWiseBooking>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<sp_GetRecentBookingGraph_Result>, List<GetGraphicalPeriodWiseBooking>>(dbEntity.sp_GetRecentBookingGraph().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstBooking;
        }

        public List<GetGraphicalPeriodWiseBooking> BindRecentPaymentGraph()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<GetGraphicalPeriodWiseBooking> lstBooking = new List<GetGraphicalPeriodWiseBooking>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetRecentPaymentGraph_Result, GetGraphicalPeriodWiseBooking>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<sp_GetRecentPaymentGraph_Result>, List<GetGraphicalPeriodWiseBooking>>(dbEntity.sp_GetRecentPaymentGraph().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstBooking;
        }

        public List<GetGraphicalPeriodWiseBooking> BindRecentAddedIBOGraph()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<GetGraphicalPeriodWiseBooking> lstBooking = new List<GetGraphicalPeriodWiseBooking>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetRecentAddedIBOGraph_Result, GetGraphicalPeriodWiseBooking>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<sp_GetRecentAddedIBOGraph_Result>, List<GetGraphicalPeriodWiseBooking>>(dbEntity.sp_GetRecentAddedIBOGraph().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstBooking;
        }

        public List<PastDue> GetPastDue()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<PastDue> lstBooking = new List<PastDue>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_PastDue_Result, PastDue>();
                });
                IMapper mapper = config.CreateMapper();
                lstBooking = mapper.Map<List<sp_PastDue_Result>, List<PastDue>>(dbEntity.sp_PastDue().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstBooking;
        }

        public List<GetProjectWiseArea> GetProjectWiseArea()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<GetProjectWiseArea> lstArea = new List<GetProjectWiseArea>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetProjectWiseArea_Result, GetProjectWiseArea>();
                });
                IMapper mapper = config.CreateMapper();
                lstArea = mapper.Map<List<sp_GetProjectWiseArea_Result>, List<GetProjectWiseArea>>(dbEntity.sp_GetProjectWiseArea().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstArea;
        }
        public bool RegisterFranchise(FranchiseRegistration fr)
        {
            try
            {
                tblFranchiseRegistration frInfo = new tblFranchiseRegistration();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FranchiseRegistration, tblFranchiseRegistration>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<FranchiseRegistration, tblFranchiseRegistration>(fr, frInfo);
                dbEntity.tblFranchiseRegistrations.Add(frInfo);

                tblFranchiseRegistrationStatu frStatusInfo = new tblFranchiseRegistrationStatu();
                var config1 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FranchiseRegistration, tblFranchiseRegistrationStatu>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper1 = config1.CreateMapper();
                mapper1.Map<FranchiseRegistration, tblFranchiseRegistrationStatu>(fr, frStatusInfo);
                dbEntity.tblFranchiseRegistrationStatus.Add(frStatusInfo);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<FranchiseRegistration> GetFranchiseAgreements()
        {
            List<FranchiseRegistration> lstFranchises = new List<FranchiseRegistration>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblFranchiseRegistration, FranchiseRegistration>();
                });
                IMapper mapper = config.CreateMapper();
                lstFranchises = mapper.Map<List<tblFranchiseRegistration>, List<FranchiseRegistration>>(dbEntity.tblFranchiseRegistrations.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstFranchises;
        }

        public List<FranchiseBookings> GetFranchiseBookings()
        {
            List<FranchiseBookings> lstFranchises = new List<FranchiseBookings>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblFranchiseBooking, FranchiseBookings>();
                });
                IMapper mapper = config.CreateMapper();
                lstFranchises = mapper.Map<List<tblFranchiseBooking>, List<FranchiseBookings>>(dbEntity.tblFranchiseBookings.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return lstFranchises;
        }

        public bool UpdateFranchiseBookings(FranchiseBookings fb)
        {
            try
            {
                var franchiseBooking = dbEntity.tblFranchiseBookings.Where(x => x.FlatID == fb.FlatID).FirstOrDefault();
                if (fb.Status == "Approved")
                {
                    dbEntity.sp_FranchiseBookingApproval(fb.BookingID, fb.FlatID);
                    dbEntity.SaveChanges();
                    //var customer = dbEntity.tblCustomerInfoes.Where(x => x.BookingID == fb.BookingID).FirstOrDefault();
                }
                else if (fb.Status == "Rejected")
                {
                    dbEntity.sp_FranchiseBookingRejected(fb.BookingID, fb.FlatID);
                    dbEntity.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public FranchiseRegistration GetFranchiseAgreements(int regNo)
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblFranchiseRegistration, FranchiseRegistration>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<tblFranchiseRegistration, FranchiseRegistration>(dbEntity.tblFranchiseRegistrations.Where(x => x.RegisterNo == regNo).FirstOrDefault());

                //return mapper.Map<sp_GetDashboardParameters_Result>,<DashboardParameters>(dbEntity.sp_GetDashboardParameters().FirstOrDefault()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new FranchiseRegistration();
            }
        }

        public bool UpdateFranchiseAgreements(FranchiseRegistration fr)
        {
            try
            {

                var oldAgreement = dbEntity.tblFranchiseRegistrations.Where(x => x.RegisterNo == fr.RegisterNo).FirstOrDefault();
                oldAgreement.Status = fr.Status;
                oldAgreement.Comments = fr.Comments;
                oldAgreement.ModifiedBy = fr.CreatedBy;
                oldAgreement.ModifiedDate = fr.CreatedDate;
                tblFranchiseRegistrationStatu frStatusInfo = new tblFranchiseRegistrationStatu();
                var config1 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<FranchiseRegistration, tblFranchiseRegistrationStatu>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper1 = config1.CreateMapper();
                mapper1.Map<FranchiseRegistration, tblFranchiseRegistrationStatu>(fr, frStatusInfo);
                dbEntity.tblFranchiseRegistrationStatus.Add(frStatusInfo);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string GetFranchiseOwnerEmail(FranchiseRegistration fr)
        {
            try
            {
                var IBOID = dbEntity.tblFranchiseRegistrations.Where(x => x.RegisterNo == fr.RegisterNo).Select(x => x.IBOID).FirstOrDefault();
                //var agentFranchise = dbEntity.tblAgentMasters.Where(x => x.)


                var agent = dbEntity.tblAgentMasters.Where(x => x.AgentID == IBOID).FirstOrDefault();
                var userID = dbEntity.AspNetUsers.Where(x => x.Email == agent.AgenteMail).Select(x => x.Id).FirstOrDefault();
                var roleID = dbEntity.AspNetRoles.Where(x => x.Name == "Franchise Owner").Select(x => x.Id).FirstOrDefault();
                dbEntity.sp_CreateUserRole(userID, roleID);
                agent.FranchiseID = fr.RegisterNo;
                dbEntity.SaveChanges();
                return agent.AgenteMail;
            }
            catch
            {
                return "";
            }
        }

        public List<GetFranchiseStatus> GetFranchiseStatus(int regNO)
        {
            List<GetFranchiseStatus> franchiseStatuses = new List<GetFranchiseStatus>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetFranchiseStatus_Result, GetFranchiseStatus>();
                });
                IMapper mapper = config.CreateMapper();
                franchiseStatuses = mapper.Map<List<sp_GetFranchiseStatus_Result>, List<GetFranchiseStatus>>(dbEntity.sp_GetFranchiseStatus(regNO).ToList()).ToList();
                foreach (var item in franchiseStatuses)
                {
                    item.formattedDate = Convert.ToDateTime(item.date).ToString("dddd, dd MMMM yyyy");
                }
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }

            return franchiseStatuses;
        }

        public bool BulkUploadExpenses(List<DailyExpense> lstExpenses)
        {
            try
            {
                List<tblDailyExpens> newlstExpenses = new List<tblDailyExpens>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DailyExpense, tblDailyExpens>();
                });

                IMapper mapper = config.CreateMapper();
                newlstExpenses = mapper.Map<List<DailyExpense>, List<tblDailyExpens>>(lstExpenses);
                dbEntity.tblDailyExpenses.AddRange(newlstExpenses);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool SaveProjectGallery(ConstructionPic cp)
        {
            try
            {
                tblConstructionPic newPic = new tblConstructionPic();
                newPic.ProjectID = cp.ProjectID;
                newPic.URL = cp.URL;
                newPic.isVideo = cp.isVideo;
                newPic.Active = cp.Active;
                newPic.CreatedBy = cp.CreatedBy;
                newPic.CreatedDate = DateTime.Now;
                dbEntity.tblConstructionPics.Add(newPic);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ConstructionPic> GetProjectGallery(string username)
        {
            List<ConstructionPic> lstPics = new List<ConstructionPic>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetConstructionPics_Result, ConstructionPic>();
                });
                IMapper mapper = config.CreateMapper();
                lstPics = mapper.Map<List<sp_GetConstructionPics_Result>, List<ConstructionPic>>(dbEntity.sp_GetConstructionPics(username).ToList()).ToList();

            }
            catch (Exception)
            {

            }
            return lstPics;
        }

        public List<NewsDetails> GetNewsUpdates()
        {
            List<NewsDetails> lstNews = new List<NewsDetails>();

            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblNew, NewsDetails>();
                });
                IMapper mapper = config.CreateMapper();
                lstNews = mapper.Map<List<tblNew>, List<NewsDetails>>(dbEntity.tblNews.ToList()).ToList();

            }
            catch (Exception)
            {

            }

            return lstNews;
        }

        public List<GetPaymentReceiptApproval> GetPaymentReceiptsForApproval()
        {
            List<GetPaymentReceiptApproval> lstReceiptsForApproval = new List<GetPaymentReceiptApproval>();

            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetPaymentsForReceiptApproval_Result, GetPaymentReceiptApproval>();
                });
                IMapper mapper = config.CreateMapper();
                lstReceiptsForApproval = mapper.Map<List<sp_GetPaymentsForReceiptApproval_Result>, List<GetPaymentReceiptApproval>>(dbEntity.sp_GetPaymentsForReceiptApproval().ToList()).ToList();
            }
            catch (Exception ex)
            {

            }

            return lstReceiptsForApproval;
        }
        public bool UpdateNews(NewsDetails nd)
        {
            try
            {
                tblNew newsTable = dbEntity.tblNews.Where(x => x.NewsID == nd.NewsID).FirstOrDefault();
                newsTable.News = nd.News;
                newsTable.NewsDate = nd.NewsDate;
                //newsTable.CreatedDate = nd.CreatedDate;
                //newsTable.CreatedBy = nd.CreatedBy;
                //dbEntity.tblNews.Add(newsTable);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdatePaymentReceiptsForApproval(GetPaymentReceiptApproval payInfo)
        {
            try
            {
                tblPaymentInfo tblPaymentInfo = dbEntity.tblPaymentInfoes.Where(x => x.PaymentID == payInfo.PaymentID).FirstOrDefault();
                tblPaymentInfo.ViewReceipt = payInfo.ViewReceipt;

                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool SaveNews(NewsDetails nd)
        {
            try
            {
                tblNew newsTable = new tblNew();
                newsTable.News = nd.News;
                newsTable.NewsDate = nd.NewsDate;
                newsTable.CreatedDate = nd.CreatedDate;
                newsTable.CreatedBy = nd.CreatedBy;
                dbEntity.tblNews.Add(newsTable);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool CheckFranchiseRegistered(string username)
        {
            try
            {
                var existingFranchise = dbEntity.tblFranchiseRegistrations.Where(x => x.CreatedBy == username && x.Status == "MD Approved").FirstOrDefault();
                if (existingFranchise != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public int? GetFranchiseID(string username)
        {
            var franchise = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == username).FirstOrDefault();
            if (franchise != null)
            {
                return franchise.FranchiseID;
            }
            else
                return null;
        }

        public bool ADDIBOAdvance(IBOAdvanceForm advanceForm)
        {
            var result = false;
            try
            {

                tblIBOAdvanceForm advInfo = new tblIBOAdvanceForm();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<IBOAdvanceForm, tblIBOAdvanceForm>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<IBOAdvanceForm, tblIBOAdvanceForm>(advanceForm, advInfo);
                dbEntity.tblIBOAdvanceForms.Add(advInfo);
                dbEntity.SaveChanges();
                //var flatWiseAgentCommission = dbEntity.tblFlatWiseAgentCommissions.Where(x => x.AgentID == advanceForm.IBOID && x.FlatID == advanceForm.FlatID).FirstOrDefault();
                //if (flatWiseAgentCommission != null)
                //{
                //    flatWiseAgentCommission.AmountPaid = Convert.ToInt32(advanceForm.AmountPaid);
                //    flatWiseAgentCommission.NetBalance = flatWiseAgentCommission.NetBalance - Convert.ToInt32(advanceForm.AmountPaid);
                //    result = true;
                //    dbEntity.SaveChanges();
                //}
                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                result = false;
            }
            return result;
        }


        public bool AddCheque(Cheques cq)
        {
            var result = false;
            try
            {

                tblCheque cqInfo = new tblCheque();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Cheques, tblCheque>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<Cheques, tblCheque>(cq, cqInfo);
                dbEntity.tblCheques.Add(cqInfo);
                dbEntity.SaveChanges();
                result = true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                result = false;
            }
            return result;
        }


        public bool UpdateIBOAdvances(IBOAdvanceForm advanceForm)
        {
            var result = false;
            try
            {
                var existingForm = dbEntity.tblIBOAdvanceForms.Where(x => x.ID == advanceForm.ID).FirstOrDefault();
                existingForm.ProjectID = advanceForm.ProjectID;
                existingForm.TowerID = advanceForm.TowerID;
                existingForm.FlatID = advanceForm.FlatID;
                existingForm.Comment = advanceForm.Comment;



                var flatWiseAgentCommission = dbEntity.tblFlatWiseAgentCommissions.Where(x => x.AgentID == advanceForm.IBOID && x.FlatID == advanceForm.FlatID).FirstOrDefault();
                if (flatWiseAgentCommission != null)
                {
                    flatWiseAgentCommission.Advance = Convert.ToInt32(advanceForm.AmountPaid);
                    flatWiseAgentCommission.NetBalance = flatWiseAgentCommission.NetBalance - Convert.ToInt32(advanceForm.AmountPaid);
                    flatWiseAgentCommission.Comment = advanceForm.Comment;

                    dbEntity.SaveChanges();
                    result = true;
                }



            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                result = false;
            }
            return result;
        }


        public List<IBOAdvanceForm> GetIBOAdvances()
        {
            List<IBOAdvanceForm> lstIBOAdvances = new List<IBOAdvanceForm>();

            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblIBOAdvanceForm, IBOAdvanceForm>();
                });
                IMapper mapper = config.CreateMapper();
                lstIBOAdvances = mapper.Map<List<tblIBOAdvanceForm>, List<IBOAdvanceForm>>(dbEntity.tblIBOAdvanceForms.ToList()).ToList();
            }
            catch (Exception ex)
            {

            }

            return lstIBOAdvances;
        }

        public List<Cheques> GetCheques()
        {
            List<Cheques> lstCheques = new List<Cheques>();

            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblCheque, Cheques>();
                });
                IMapper mapper = config.CreateMapper();
                lstCheques = mapper.Map<List<tblCheque>, List<Cheques>>(dbEntity.tblCheques.ToList()).ToList();
            }
            catch (Exception ex)
            {

            }

            return lstCheques;
        }

        public bool UpdateCheque(Cheques cq)
        {
            try
            {
                tblCheque tblChequeInfo = dbEntity.tblCheques.Where(x => x.ID == cq.ID).FirstOrDefault();
                tblChequeInfo.Amount = cq.Amount;
                tblChequeInfo.Remarks = cq.Remarks;
                tblChequeInfo.Status = cq.Status;
                tblChequeInfo.ModifiedBy = cq.ModifiedBy;
                tblChequeInfo.ModifiedDate = cq.ModifiedDate;

                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<ProjectExpenseCategory> BindProjectExpenseCategory()
        {
            List<ProjectExpenseCategory> projectExpenseCategories = new List<ProjectExpenseCategory>();

            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblProjectExpenseCategory, ProjectExpenseCategory>();
                });
                IMapper mapper = config.CreateMapper();
                projectExpenseCategories = mapper.Map<List<tblProjectExpenseCategory>, List<ProjectExpenseCategory>>(dbEntity.tblProjectExpenseCategories.ToList()).ToList();
            }
            catch (Exception ex)
            {

            }

            return projectExpenseCategories;
        }


        public List<GetProjectCategoryWiseExpenses> GetProjectCategoryWiseExpenses()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetProjectCategoryWiseExpenses_Result, GetProjectCategoryWiseExpenses>();
                });
                IMapper mapper = config.CreateMapper();

                return mapper.Map<List<sp_GetProjectCategoryWiseExpenses_Result>, List<GetProjectCategoryWiseExpenses>>(dbEntity.sp_GetProjectCategoryWiseExpenses().ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<GetProjectCategoryWiseExpenses>();
            }
        }

        public bool GetProjectApprovalStatus(int PaymentID)
        {
            return Convert.ToBoolean(dbEntity.sp_GetProjectApprovalStatus(PaymentID).FirstOrDefault());
        }

    }
}
