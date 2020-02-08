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
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<Projects, tblProject>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    });
                    IMapper mapper = config.CreateMapper();
                    //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                    mapper.Map<Projects, tblProject>(p, projectOld);
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
            catch(Exception ex)
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
                towerNew.CreatedDate = DateTime.Now;

                //var config = new MapperConfiguration(cfg =>
                //{
                //    cfg.CreateMap<Towers, tblTower>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                //});
                //IMapper mapper = config.CreateMapper();
                ////mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                //mapper.Map<Towers, tblTower>(p, towerNew);
                dbEntity.tblTowers.Add(towerNew);
                dbEntity.SaveChanges();

                return true;
            }
            catch(Exception ex)
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
                    flatOld.Floor = p.Floor;
                    flatOld.Bhk = p.Bhk;
                    flatOld.Sft = p.Sft;
                    flatOld.Facing = p.Facing.ToUpper();
                    flatOld.BookingStatus = p.BookingStatus;

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
                flatNew.Facing = p.Facing.ToUpper();
                flatNew.BookingStatus = p.BookingStatus;

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
            catch(Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public bool AddAgentProjectLevel(AgentProjectLevel a)
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
    }
}
