using AutoMapper;
using log4net;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class AdminDL
    {
        salesDBEntities dbEntity = new salesDBEntities();
        private static readonly ILog log =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool UpdateProject(Projects p)
        {
            try
            {
                p.BookingStatusName = null;
                tblProject projectOld = dbEntity.tblProjects.Where(x => x.ProjectID == p.ProjectID).FirstOrDefault();

                if (projectOld != null)
                {
                    projectOld.ProjectName = p.ProjectName;
                    projectOld.ProjectLocation = p.ProjectLocation;
                    projectOld.BookingStatus = p.BookingStatus;
                    projectOld.ClubHouseCharges = p.ClubHouseCharges;
                    projectOld.Description = p.Description;
                    projectOld.LocationURL = p.LocationURL;
                    projectOld.ApprovalStatus = p.ApprovalStatus;
                    projectOld.Pricing = p.Pricing;
                    projectOld.UpdatedDate = DateTime.Now;
                    projectOld.BSP = p.BSP;
                    projectOld.ProjectType = p.ProjectType;
                    projectOld.ProjectApproved = p.ProjectApproved;
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


        public bool UpdateProjectExpenseCategory(ProjectExpenseCategory p)
        {
            try
            {
                
                tblProjectExpenseCategory projectOld = dbEntity.tblProjectExpenseCategories.Where(x => x.ID == p.ID).FirstOrDefault();

                if (projectOld != null)
                {
                    projectOld.SubCategory = p.SubCategory;
                    projectOld.CreatedDate = DateTime.Now;
                    projectOld.CreatedBy = p.CreatedBy;
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

        public bool AddProject(Projects p)
        {
            try
            {
                p.BookingStatusName = null;
                p.CreatedDate = DateTime.Now;
                tblProject projectNew = new tblProject();


                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Projects, tblProject>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                mapper.Map<Projects, tblProject>(p, projectNew);
                dbEntity.tblProjects.Add(projectNew);
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }


        public bool AddProjectExpenseCategory(ProjectExpenseCategory pec)
        {
            try
            {
              
                pec.CreatedDate = DateTime.Now;
                tblProjectExpenseCategory pecNew = new tblProjectExpenseCategory();


                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ProjectExpenseCategory, tblProjectExpenseCategory>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                mapper.Map<ProjectExpenseCategory, tblProjectExpenseCategory>(pec, pecNew);
                dbEntity.tblProjectExpenseCategories.Add(pecNew);
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }
        public bool AddTower(Towers p)
        {
            try
            {
                //p.BookingStatusName = null;
                tblTower towerNew = new tblTower();
                towerNew.TowerID = p.TowerID;
                towerNew.TowerName = p.TowerName;
                towerNew.ProjectID = p.ProjectID;
                towerNew.FlatCount = p.FlatCount;
                towerNew.BookingStatus = p.BookingStatus;
                towerNew.CreatedBy = p.CreatedBy;
                towerNew.CreatedDate = DateTime.Now;
                
                dbEntity.tblTowers.Add(towerNew);
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool UpdateTower(Towers p)
        {
            try
            {
                //p.BookingStatusName = null;
                tblTower towerOld = dbEntity.tblTowers.Where(x => x.TowerID == p.TowerID).FirstOrDefault();
                if (towerOld != null)
                {
                    towerOld.BookingStatus = p.BookingStatus;
                    towerOld.FlatCount = p.FlatCount;
                    towerOld.UpdatedBy = p.CreatedBy;
                    towerOld.UpdatedDate = DateTime.Now;
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

        public bool UpdateFlat(Flats p)
        {
            try
            {
                //p.BookingStatusName = null;
                tblFlat flatOld = dbEntity.tblFlats.Where(x => x.FlatID == p.FlatID).FirstOrDefault();
                if (flatOld != null)
                {
                    flatOld.FlatName = p.FlatName;
                    flatOld.Floor = p.Floor;
                    flatOld.Bhk = p.Bhk;
                    flatOld.Sft = p.Sft;
                    flatOld.SqYds = p.SqYds;
                    flatOld.Facing = p.Facing.ToUpper();
                    flatOld.BookingStatus = p.BookingStatus;
                    flatOld.UpdatedBy = p.UpdatedBy;
                    flatOld.UpdatedDate = p.UpdatedDate;
                    //towerOld.BookingStatus = p.BookingStatus;
                    //towerOld.FlatCount = p.FlatCount;
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

        public bool AddFlat(Flats p)
        {
            try
            {
                //p.BookingStatusName = null;
                tblFlat flatNew = new tblFlat();
                flatNew.FlatName = p.FlatName;
                flatNew.ProjectID = p.ProjectID;
                flatNew.TowerID = p.TowerID;
                flatNew.Floor = p.Floor;
                flatNew.Bhk = p.Bhk;
                flatNew.Sft = p.Sft;
                flatNew.SqYds = p.SqYds;
                flatNew.Facing = p.Facing.ToUpper();
                flatNew.BookingStatus = p.BookingStatus;
                flatNew.CreatedBy = p.CreatedBy;
                flatNew.CreatedDate = p.CreatedDate;
                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<Towers, tblTower>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                //});
                //IMapper mapper = config.CreateMapper();
                ////mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                //mapper.Map<Towers, tblTower>(p, towerNew);
                dbEntity.tblFlats.Add(flatNew);
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool AddAgentProjectLevel(AgentProjectLevel a)
        {
            try
            {
                tblAgentProjectLevel agentLevelNew = new tblAgentProjectLevel();


                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AgentProjectLevel, tblAgentProjectLevel>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                mapper.Map<AgentProjectLevel, tblAgentProjectLevel>(a, agentLevelNew);
                dbEntity.tblAgentProjectLevels.Add(agentLevelNew);
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool UpdateAgentProjectLevel(AgentProjectLevel a)
        {
            tblAgentProjectLevel agentLevelOld = dbEntity.tblAgentProjectLevels.Where(x => x.AgentID == a.AgentID && x.ProjectID == a.ProjectID).FirstOrDefault();

            if (agentLevelOld != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<AgentProjectLevel, tblAgentProjectLevel>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                mapper.Map<AgentProjectLevel, tblAgentProjectLevel>(a, agentLevelOld);
                dbEntity.SaveChanges();
            }
            return true;
        }


        public bool AddLevel(LevelsMaster a)
        {
            try
            {
                tblLevelsMaster levelNew = new tblLevelsMaster();
                levelNew.LevelName = a.LevelName;
                levelNew.Percentage = a.Percentage;
                levelNew.Status = a.BookingStatus;
                levelNew.CreatedBy = a.CreatedBy;
                levelNew.CreatedDate = a.CreatedDate;
                dbEntity.tblLevelsMasters.Add(levelNew);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }

        }


        public bool UpdateLevel(LevelsMaster p)
        {
            try
            {
                //p.BookingStatusName = null;
                tblLevelsMaster levelsOld = dbEntity.tblLevelsMasters.Where(x => x.LevelID == p.LevelID).FirstOrDefault();
                if (levelsOld != null)
                {
                    levelsOld.LevelID = p.LevelID;
                    levelsOld.LevelName = p.LevelName;
                    levelsOld.Percentage = p.Percentage;
                    levelsOld.Status = p.BookingStatus;
                    levelsOld.UpdatedBy = p.UpdatedBy;
                    levelsOld.UpdatedDate = p.UpdatedDate;
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

        public bool UpdateScheme(Schemes p)
        {
            try
            {
                //p.BookingStatusName = null;
                tblSchemeMaster schemesOld = dbEntity.tblSchemeMasters.Where(x => x.SchemeID == p.SchemeID).FirstOrDefault();
                if (schemesOld != null)
                {
                    schemesOld.SchemeID = p.SchemeID;
                    schemesOld.ProjectID = p.ProjectID;
                    schemesOld.SchemeName = p.SchemeName;
                    schemesOld.SqFtCost = p.SqFtCost;
                    schemesOld.SchemeStatus = p.BookingStatus;
                    schemesOld.UpdatedBy = p.UpdatedBy;
                    schemesOld.UpdatedDate = p.UpdatedDate;
                    schemesOld.PaymentTimePeriod = p.PaymentTimePeriod;
                    schemesOld.PaymentPercentage = p.PaymentPercentage;
                    schemesOld.DownPaymentTimePeriod = p.DownPaymentTimePeriod;
                    schemesOld.DownPayment = p.DownPayment;
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

        public bool AddScheme(Schemes p)
        {
            try
            {
                tblSchemeMaster schemesNew = new tblSchemeMaster();
                schemesNew.SchemeName = p.SchemeName;
                schemesNew.ProjectID = p.ProjectID;
                schemesNew.SqFtCost = p.SqFtCost;
                schemesNew.SchemeStatus = p.BookingStatus;
                schemesNew.CreatedBy = p.CreatedBy;
                schemesNew.CreatedDate = p.CreatedDate;
                schemesNew.PaymentTimePeriod = p.PaymentTimePeriod;
                schemesNew.PaymentPercentage = p.PaymentPercentage;
                schemesNew.DownPaymentTimePeriod = p.DownPaymentTimePeriod;
                schemesNew.DownPayment = p.DownPayment;
                dbEntity.tblSchemeMasters.Add(schemesNew);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }

        }

        public List<GetUsersWithRoles> GetUsersWithRoles()
        {
            List<GetUsersWithRoles> lstUsers = new List<GetUsersWithRoles>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetUsersWithRoles_Result, GetUsersWithRoles>();
                });
                IMapper mapper = config.CreateMapper();
                lstUsers = mapper.Map<List<sp_GetUsersWithRoles_Result>, List<GetUsersWithRoles>>(dbEntity.sp_GetUsersWithRoles().ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);

            }
            return lstUsers;
        }

        public List<LandLord> BindLandlords()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<LandLord> lstLandlords = new List<LandLord>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblLandLord, LandLord>();
                });
                IMapper mapper = config.CreateMapper();
                lstLandlords = mapper.Map<List<tblLandLord>, List<LandLord>>(dbEntity.tblLandLords.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstLandlords;
        }

        public bool AddLandLord(LandLord a)
        {
            try
            {
                tblLandLord landlords = new tblLandLord();

                a.ProjectName = dbEntity.tblProjects.Where(x => x.ProjectID == a.ProjectID).Select(x => x.ProjectName).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LandLord, tblLandLord>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<LandLord, tblLandLord>(a, landlords);
                dbEntity.tblLandLords.Add(landlords);
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool UpdateLandLord(LandLord a)
        {
            tblLandLord landlordOld = dbEntity.tblLandLords.Where(x => x.ID == a.ID).FirstOrDefault();
            a.ProjectName = dbEntity.tblProjects.Where(x => x.ProjectID == a.ProjectID).Select(x => x.ProjectName).FirstOrDefault();

            if (landlordOld != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LandLord, tblLandLord>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                mapper.Map<LandLord, tblLandLord>(a, landlordOld);
                dbEntity.SaveChanges();
            }
            return true;
        }

        public bool AddLandLordPayments(LandlordPayments a)
        {
            try
            {
                tblLandlordPayment landlords = new tblLandlordPayment();

                var landlord = dbEntity.tblLandLords.Where(x => x.ID == a.LandlordID).FirstOrDefault();
                a.ProjectID = landlord.ProjectID;
                a.ProjectName = landlord.ProjectName;
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LandlordPayments, tblLandlordPayment>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<LandlordPayments, tblLandlordPayment>(a, landlords);
                dbEntity.tblLandlordPayments.Add(landlords);
                dbEntity.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public List<LandlordPayments> GetPayments()
        {
            this.dbEntity.Configuration.ProxyCreationEnabled = false;

            List<LandlordPayments> lstLandlords = new List<LandlordPayments>();
            try
            {
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblLandlordPayment, LandlordPayments>();
                });
                IMapper mapper = config.CreateMapper();
                lstLandlords = mapper.Map<List<tblLandlordPayment>, List<LandlordPayments>>(dbEntity.tblLandlordPayments.ToList()).ToList();
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
            }
            return lstLandlords;
        }

        public bool UpdateLandLordPayments(LandlordPayments a)
        {
            tblLandlordPayment landlordOld = dbEntity.tblLandlordPayments.Where(x => x.PaymentID == a.PaymentID).FirstOrDefault();
            //a.ProjectName = dbEntity.tblProjects.Where(x => x.ProjectID == a.ProjectID).Select(x => x.ProjectName).FirstOrDefault();

            if (landlordOld != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<LandlordPayments, tblLandlordPayment>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                mapper.Map<LandlordPayments, tblLandlordPayment>(a, landlordOld);
                dbEntity.SaveChanges();
            }
            return true;
        }

    }
}
