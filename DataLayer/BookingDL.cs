﻿using AutoMapper;
using log4net;
using ModelLayer;
using System;
using System.Collections.Generic;
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
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID && a.BookingStatus == "O").ToList()).ToList();
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
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID).ToList()).ToList();
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
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.ToList()).ToList();
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
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID && (a.BookingStatus == "H" || a.BookingStatus == "S")).ToList()).ToList();
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
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblFlat, Flats>();
                });
                IMapper mapper = config.CreateMapper();
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID && (a.BookingStatus == "H" || a.BookingStatus == "S" || a.BookingStatus == "C")).ToList()).ToList();
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
                lstSchemes = mapper.Map<List<tblSchemeMaster>, List<Schemes>>(dbEntity.tblSchemeMasters.Where(z=> z.ProjectID == projectID).ToList()).ToList();
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
                var highestLevel = dbEntity.tblAgentProjectLevels.OrderByDescending(e => e.LevelID).Where(z => z.ProjectID == bookingInfo.ProjectID).Select(z => z.LevelID).FirstOrDefault();
                var highestPercentage = dbEntity.tblLevelsMasters.Where(z => z.LevelID == highestLevel).Select(z => z.Percentage).FirstOrDefault();
                var levelPercentage = dbEntity.tblLevelsMasters.Where(z => z.LevelID == bookingInfo.Level).Select(z => z.Percentage).FirstOrDefault();
                var TotalComm = (bookingInfo.FinalRate * highestPercentage) / 100;
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
                    bookingstatus = "H";
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
                        fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == currentAgent.LevelID).Select(y => y.Percentage).FirstOrDefault();
                        lstFwac.Add(fwac);
                    }
                    lstFwac = lstFwac.OrderBy(x => x.Percentage).ToList();
                    double oldPercentage = 0;
                    foreach (var item in lstFwac)
                    {
                        var difference = item.Percentage - oldPercentage;
                        item.AgentCommission = Convert.ToInt32((bookingInfo.FinalRate * difference) / 100);
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
                    dbEntity.tblFlatWiseAgentCommissions.Add(fwac);
                    //lstFwac.Add(fwac);
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
                var highestLevel = dbEntity.tblAgentProjectLevels.OrderByDescending(e => e.LevelID).Where(z => z.ProjectID == bookingInfo.ProjectID).Select(z => z.LevelID).FirstOrDefault();
                var highestPercentage = dbEntity.tblLevelsMasters.Where(z => z.LevelID == highestLevel).Select(z => z.Percentage).FirstOrDefault();
                var levelPercentage = dbEntity.tblLevelsMasters.Where(z => z.LevelID == bookingInfo.Level).Select(z => z.Percentage).FirstOrDefault();
                var TotalComm = (bookingInfo.FinalRate * highestPercentage) / 100;
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
                List<tblPaymentInfo> paymentListOld = dbEntity.tblPaymentInfoes.Where(x => x.BookingID == bookingInfo.BookingID).OrderBy(x=>x.PaymentID).ToList();
                for(int i=0;i<paymentListOld.Count;i++)
                {
                    if(i == 0)
                    {
                        paymentListOld[i].BookingAmount = bookingInfo.BookingAmount;
                        paymentListOld[i].BalanceAmount = bookingInfo.BalanceAmount;
                        paymentListOld[i].CreatedDate = bookingInfo.CreatedDate;
                        paymentListOld[i].Day = Convert.ToDateTime(bookingInfo.CreatedDate).Day;
                        paymentListOld[i].Month = Convert.ToDateTime(bookingInfo.CreatedDate).Month;
                        paymentListOld[i].Year = Convert.ToDateTime(bookingInfo.CreatedDate).Year;
                    }
                    else
                    {
                        paymentListOld[i].BalanceAmount = paymentListOld[i-1].BalanceAmount - paymentListOld[i].BookingAmount;
                    }
                }

                //Update Agent Payment Info
                bookingInfo.SASNetBalance = bookingInfo.SASNet;
                bookingInfo.AgentNetBalance = bookingInfo.AgentNet;
                bookingInfo.SASNetPaid = 0;
                bookingInfo.AgentNetPaid = 0;
                List<tblAgentPaymentInfo> agentPaymentListOld = dbEntity.tblAgentPaymentInfoes.Where(x => x.BookingID == bookingInfo.BookingID).OrderBy(x => x.AgentPaymentID).ToList();
                for(int j=0;j<agentPaymentListOld.Count;j++)
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
                        agentPaymentListOld[j].SASNetBalance = agentPaymentListOld[j-1].SASNetBalance - agentPaymentListOld[j].SASNetPaid;
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
                    bookingstatus = "H";
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
                        fwac.Percentage = dbEntity.tblLevelsMasters.Where(x => x.LevelID == currentAgent.LevelID).Select(y => y.Percentage).FirstOrDefault();
                        lstFwac.Add(fwac);
                    }
                    lstFwac = lstFwac.OrderBy(x => x.Percentage).ToList();
                    double oldPercentage = 0;
                    foreach (var item in lstFwac)
                    {
                        var difference = item.Percentage - oldPercentage;
                        item.AgentCommission = Convert.ToInt32((bookingInfo.FinalRate * difference) / 100);
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
                    dbEntity.tblFlatWiseAgentCommissions.Add(fwac);
                    //lstFwac.Add(fwac);
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
                for(int i =0;i<lstFlatLifeCycle.Count;i++ )
                {
                    if(i ==0)
                    {
                        lstFlatLifeCycle[i].FlatEvent = "START";
                    }
                    else
                    {
                        if(lstFlatLifeCycle[i].BalanceAmount == 0)
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
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentCommissionByAgentLogins_Result, FlatWiseAgentCommission>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentCommissionByAgentLogins_Result>, List<FlatWiseAgentCommission>>(dbEntity.sp_GetAgentCommissionByAgentLogins(email).ToList()).ToList();
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
            catch(Exception ex)
            {
                return new BookingInformation();
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
            catch(Exception ex)
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
                if(list[0] == null)
                {
                    list[0] = 0;
                }
                agentPayDetails.TotalPaid = list[0];
                agentPayDetails.TotalBalance = list[1] - list[0];
            }
            return agentPayDetails;
        }
    }
}
