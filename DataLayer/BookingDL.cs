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
                lstProjects = mapper.Map<List<tblProject>, List<Projects>>(dbEntity.tblProjects.Where(a => a.ProjectLocation == locationName).ToList()).ToList();
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
                    cfg.CreateMap<tblFlat, Flats>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID).OrderBy(x => new { x.Floor, x.FlatName }).ToList()).ToList();
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
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID && (a.BookingStatus == "P" || a.BookingStatus == "S" || a.BookingStatus == "C")).ToList()).ToList();
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
                lstAgents = mapper.Map<List<sp_GetAgentsByProjectID_Result>, List<AgentProjectLevel>>(dbEntity.sp_GetAgentsByProjectID(projectID).ToList()).ToList();
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

        public bool SaveNewBooking(BookingInformation bookingInfo)
        {
            try
            {
                //var highestLevel = dbEntity.tblAgentProjectLevels.OrderByDescending(e => e.LevelID).Where(z => z.ProjectID == bookingInfo.ProjectID).Select(z => z.LevelID).FirstOrDefault();
                //var highestPercentage = dbEntity.tblLevelsMasters.Where(z => z.LevelID == highestLevel).Select(z => z.Percentage).FirstOrDefault();
                var highestPercentage = dbEntity.sp_GetHighestPercetageInProject(bookingInfo.ProjectID).FirstOrDefault();
                var levelPercentage = dbEntity.tblLevelsMasters.Where(z => z.LevelID == bookingInfo.Level).Select(z => z.Percentage).FirstOrDefault();
                var TotalComm = (bookingInfo.FinalRate * Convert.ToDouble(highestPercentage)) / 100;
                bookingInfo.TotalComm = TotalComm;
                if (Convert.ToDouble(levelPercentage) == Convert.ToDouble(highestPercentage))
                {
                    bookingInfo.SASComm = TotalComm;
                    bookingInfo.AgentComm = 0;
                }
                else
                {
                    bookingInfo.AgentComm = (bookingInfo.FinalRate * Convert.ToDouble(levelPercentage)) / 100;
                    bookingInfo.SASComm = TotalComm - bookingInfo.AgentComm;
                }
                bookingInfo.SASTDS = (bookingInfo.SASComm * 5) / 100;
                bookingInfo.SASNet = bookingInfo.SASComm - bookingInfo.SASTDS;
                bookingInfo.AgentTDS = (bookingInfo.AgentComm * 5) / 100;
                bookingInfo.AgentNet = bookingInfo.AgentComm - bookingInfo.AgentTDS;
                bookingInfo.BookingID = Guid.NewGuid();
                //bookingInfo.CreatedBy = "";
                //bookingInfo.CreatedDate = System.DateTime.Now.Date;
                bookingInfo.Day = Convert.ToDateTime(bookingInfo.CreatedDate).Day;
                bookingInfo.Month = Convert.ToDateTime(bookingInfo.CreatedDate).Month;
                bookingInfo.Year = Convert.ToDateTime(bookingInfo.CreatedDate).Year;
                int noOfDays = Convert.ToInt32(bookingInfo.PaymentTimePeriod);
                bookingInfo.DueDate = DateTime.Now.AddDays(noOfDays);

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
                    bookingstatus = "P";
                flat.BookingStatus = bookingstatus;

                //Flat Wise Agent Commissions
                List<tblFlatWiseAgentCommission> lstFwac = new List<tblFlatWiseAgentCommission>();
                if (!string.IsNullOrEmpty(bookingInfo.AgentParent))
                {
                    var allAgentList = dbEntity.sp_GetAgentsByProjectID(bookingInfo.ProjectID).ToList();
                    bookingInfo.AgentParent = bookingInfo.AgentParent + ',' + bookingInfo.AgentID;
                    var agentList = bookingInfo.AgentParent.Split(',');
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
                        fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == currentAgent.LevelID).Select(y => y.Percentage).FirstOrDefault();
                        lstFwac.Add(fwac);
                    }
                    lstFwac = lstFwac.OrderBy(x => x.Percentage).ToList();
                    double oldPercentage = 0;
                    foreach (var item in lstFwac)
                    {
                        var difference = item.Percentage - oldPercentage;
                        item.AgentCommission = Convert.ToInt32((bookingInfo.FinalRate * difference) / 100);
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
                    fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == bookingInfo.Level).Select(y => y.Percentage).FirstOrDefault();
                    fwac.AgentCommission = Convert.ToInt32((bookingInfo.FinalRate * fwac.Percentage) / 100);
                    fwac.AgentCommission = Convert.ToInt32(fwac.AgentCommission - (0.05 * fwac.AgentCommission));
                    fwac.AmountPaid = 0;
                    fwac.Discount = 0;
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
                //var highestLevel = dbEntity.tblAgentProjectLevels.OrderByDescending(e => e.LevelID).Where(z => z.ProjectID == bookingInfo.ProjectID).Select(z => z.LevelID).FirstOrDefault();
                //var highestPercentage = dbEntity.tblLevelsMasters.Where(z => z.LevelID == highestLevel).Select(z => z.Percentage).FirstOrDefault();
                var highestPercentage = dbEntity.sp_GetHighestPercetageInProject(bookingInfo.ProjectID).FirstOrDefault();

                var levelPercentage = dbEntity.tblLevelsMasters.Where(z => z.LevelID == bookingInfo.Level).Select(z => z.Percentage).FirstOrDefault();
                var TotalComm = (bookingInfo.FinalRate * Convert.ToDouble(highestPercentage)) / 100;
                bookingInfo.TotalComm = TotalComm;
                if (Convert.ToDouble(levelPercentage) == Convert.ToDouble(highestPercentage))
                {
                    bookingInfo.SASComm = TotalComm;
                    bookingInfo.AgentComm = 0;
                }
                else
                {
                    bookingInfo.AgentComm = (bookingInfo.FinalRate * Convert.ToDouble(levelPercentage)) / 100;
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

                tblBookingInformation bookingOld = dbEntity.tblBookingInformations.Where(x => x.BookingID == bookingInfo.BookingID).FirstOrDefault();

                //Update Booking Info
                if (bookingOld != null)
                {
                    //tblBookingInformation_backup bookingBackup = new tblBookingInformation_backup();
                    //var config99 = new MapperConfiguration(cfg =>
                    //{
                    //    cfg.CreateMap<tblBookingInformation, tblBookingInformation_backup>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    //});
                    //IMapper mapper99 = config99.CreateMapper();
                    //mapper99.Map<tblBookingInformation, tblBookingInformation_backup>(bookingOld, bookingBackup);
                    //dbEntity.tblBookingInformation_backup.Add(bookingBackup);
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
                    //tblCustomerInfo_backup customerBackup = new tblCustomerInfo_backup();
                    //var config33 = new MapperConfiguration(cfg =>
                    //{
                    //    cfg.CreateMap<tblCustomerInfo, tblCustomerInfo_backup>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    //});
                    //IMapper mapper33 = config33.CreateMapper();
                    //mapper33.Map<tblCustomerInfo, tblCustomerInfo_backup>(customerOld, customerBackup);
                    //dbEntity.tblCustomerInfo_backup.Add(customerBackup);

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
                if (!string.IsNullOrEmpty(bookingInfo.AgentParent))
                {
                    var allAgentList = dbEntity.sp_GetAgentsByProjectID(bookingInfo.ProjectID).ToList();
                    bookingInfo.AgentParent = bookingInfo.AgentParent + ',' + bookingInfo.AgentID;
                    var agentList = bookingInfo.AgentParent.Split(',');
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
                        fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == currentAgent.LevelID).Select(y => y.Percentage).FirstOrDefault();
                        lstFwac.Add(fwac);
                    }
                    lstFwac = lstFwac.OrderBy(x => x.Percentage).ToList();
                    double oldPercentage = 0;
                    foreach (var item in lstFwac)
                    {
                        var difference = item.Percentage - oldPercentage;
                        item.AgentCommission = Convert.ToInt32((bookingInfo.FinalRate * difference) / 100);
                        item.AgentCommission = Convert.ToInt32(item.AgentCommission - (0.05 * item.AgentCommission));
                        item.NetBalance = item.AgentCommission;
                        item.Discount = 0;
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
                    fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == bookingInfo.Level).Select(y => y.Percentage).FirstOrDefault();
                    fwac.AgentCommission = Convert.ToInt32((bookingInfo.FinalRate * fwac.Percentage) / 100);
                    fwac.AgentCommission = Convert.ToInt32(fwac.AgentCommission - (0.05 * fwac.AgentCommission));
                    fwac.AmountPaid = 0;
                    fwac.Discount = 0;
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

                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblPaymentInfo, PaymentInformation>();
                });
                IMapper mapper = config.CreateMapper();
                lstPayDetails = mapper.Map<List<tblPaymentInfo>, List<PaymentInformation>>(dbEntity.tblPaymentInfoes.Where(x => x.FlatID == FlatId).ToList()).ToList();
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
                    flat.BookingStatus = "C";
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
            List<AgentMaster> lstAgents = new List<AgentMaster>();
            var result = new List<TreeObject>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentCommissionNdBalanceByAgentLogins_Result, AgentMaster>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentCommissionNdBalanceByAgentLogins_Result>, List<AgentMaster>>(dbEntity.sp_GetAgentCommissionNdBalanceByAgentLogins(email).OrderBy(x => x.AgentID).ToList()).ToList();

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
                    cfg.CreateMap<tblAgentProjectLevel, AgentProjectLevel>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<tblAgentProjectLevel>, List<AgentProjectLevel>>(dbEntity.tblAgentProjectLevels.ToList()).ToList();
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
                    cfg.CreateMap<tblFlatWiseAgentCommission, FlatWiseAgentCommission>();
                });
                IMapper mapper = config.CreateMapper();
                fwac = mapper.Map<List<tblFlatWiseAgentCommission>, List<FlatWiseAgentCommission>>(dbEntity.tblFlatWiseAgentCommissions.Where(x => x.FlatID == flatID).ToList()).ToList();
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

        public bool AddSiteVisit(SiteVisitInfo svi)
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
                    var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + agent + "&smsContentType=english");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("Cache-Control", "no-cache");
                    IRestResponse response = client.Execute(request);
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
                return mapper.Map<List<tblDailyExpens>, List<DailyExpense>>(dbEntity.tblDailyExpenses.ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<DailyExpense>();
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

        public bool UpdateSiteVisitApproval(SiteVisitInfo svi)
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
                        var client = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" + agent + "&smsContentType=english");
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("Cache-Control", "no-cache");
                        IRestResponse response = client.Execute(request);
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
    }
}
