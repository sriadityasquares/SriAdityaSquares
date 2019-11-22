using AutoMapper;
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
                ex.ToString();
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
                ex.ToString();
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
                ex.ToString();
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
                ex.ToString();
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
                ex.ToString();
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
                ex.ToString();
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
                lstFlats = mapper.Map<List<tblFlat>, List<Flats>>(dbEntity.tblFlats.Where(a => a.TowerID == towerID && a.BookingStatus == "H" || a.BookingStatus == "S").ToList()).ToList();
            }
            catch (Exception ex)
            {
                ex.ToString();
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
                ex.ToString();
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
                ex.ToString();
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
                ex.ToString();
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
                bookingInfo.CreatedBy = "";
                bookingInfo.CreatedDate = System.DateTime.Now.Date;
                bookingInfo.Day = System.DateTime.Now.Day;
                bookingInfo.Month = System.DateTime.Now.Month;
                bookingInfo.Year = System.DateTime.Now.Year;
                int noOfDays = Convert.ToInt32(bookingInfo.PaymentTimePeriod);
                bookingInfo.DueDate = DateTime.Now.AddDays(noOfDays);
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblBookingInformation>();
                });
                tblBookingInformation bookingDetails = new tblBookingInformation();
                IMapper mapper = config.CreateMapper();
                mapper.Map<BookingInformation, tblBookingInformation>(bookingInfo, bookingDetails);

                dbEntity.tblBookingInformations.Add(bookingDetails);

                var config1 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblCustomerInfo>();
                });
                tblCustomerInfo customerDetails = new tblCustomerInfo();
                IMapper mapper1 = config1.CreateMapper();
                mapper1.Map<BookingInformation, tblCustomerInfo>(bookingInfo, customerDetails);
                dbEntity.tblCustomerInfoes.Add(customerDetails);

                var config2 = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<BookingInformation, tblPaymentInfo>();
                });
                tblPaymentInfo paymentDetails = new tblPaymentInfo();
                IMapper mapper2 = config2.CreateMapper();
                mapper2.Map<BookingInformation, tblPaymentInfo>(bookingInfo, paymentDetails);
                dbEntity.tblPaymentInfoes.Add(paymentDetails);

                tblFlat flat = dbEntity.tblFlats.Where(x => x.FlatID == bookingInfo.FlatID).FirstOrDefault();
                var bookingstatus = "";
                if (bookingInfo.BookingAmount >= (0.25) * bookingInfo.FinalRate)
                {
                    bookingstatus = "S";
                }
                else
                    bookingstatus = "H";
                flat.BookingStatus = bookingstatus;
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                ex.ToString();
            }
            return lstPayDetails;
        }

        public bool SaveNewPayment(PaymentInformation payInfo)
        {
            try
            {
                payInfo.CreatedBy = "";
                payInfo.CreatedDate = System.DateTime.Now.Date;
                payInfo.Day = System.DateTime.Now.Day;
                payInfo.Month = System.DateTime.Now.Month;
                payInfo.Year = System.DateTime.Now.Year;
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
                ex.ToString();
            }
            return lstTowers;
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
                ex.ToString();
            }
            return lstAgents;
        }
    }
}
