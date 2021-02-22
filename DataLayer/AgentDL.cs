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
    public class AgentDL
    {
        salesDBEntities dbEntity = new salesDBEntities();
        private static readonly ILog log =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool UpdateAgent(AgentMaster a)
        {
            try
            {
                a.BookingStatusName = null;
                tblAgentMaster agentOld = dbEntity.tblAgentMasters.Where(x => x.AgentID == a.AgentID).FirstOrDefault();

                if (agentOld != null)
                {
                    if (agentOld.AgenteMail != a.AgenteMail)
                    {
                        var oldUser = dbEntity.AspNetUsers.Where(x => x.Email == agentOld.AgenteMail).FirstOrDefault();
                        oldUser.Email = a.AgenteMail;
                        dbEntity.SaveChanges();
                    }
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AgentMaster, tblAgentMaster>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    });
                    IMapper mapper = config.CreateMapper();
                    //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                    mapper.Map<AgentMaster, tblAgentMaster>(a, agentOld);

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

        public bool AddAgent(AgentMaster a)
        {
            try
            {
                var checkDuplicateAgentCode = dbEntity.tblAgentMasters.Where(x => x.AgentCode == a.AgentCode).FirstOrDefault();
                if (checkDuplicateAgentCode == null)
                {
                    a.BookingStatusName = null;
                    tblAgentMaster agentNew = new tblAgentMaster();


                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<AgentMaster, tblAgentMaster>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                    });
                    IMapper mapper = config.CreateMapper();
                    //mapper.Map(p, projectOld, typeof(Projects), typeof(tblProject));
                    mapper.Map<AgentMaster, tblAgentMaster>(a, agentNew);
                    dbEntity.tblAgentMasters.Add(agentNew);

                    dbEntity.SaveChanges();

                    ///Code To Be Implemented
                    ///
                    string code = a.AgentSponserCode;
                    string parentIDs = "";
                    int i = 10;
                    while (i > 0)
                    {
                        tblAgentMaster ag = dbEntity.tblAgentMasters.Where(x => x.AgentCode == code).ToList().FirstOrDefault();
                        if (ag.AgentSponserCode != "SAS")
                        {
                            parentIDs = parentIDs + ag.AgentID + ",";
                            code = ag.AgentSponserCode;
                            if (code == "SS1560")
                            {
                                i = 0;
                            }
                        }
                        else
                        {
                            i = 0;
                        }
                    }
                    tblAgentMaster newlyAddedAgent = dbEntity.tblAgentMasters.Where(x => x.AgentCode == a.AgentCode).ToList().FirstOrDefault();
                    if (parentIDs.Length > 1)
                    {
                        newlyAddedAgent.AgentParent = parentIDs.Substring(0, parentIDs.Length - 1);
                        dbEntity.SaveChanges();
                    }
                    var parentArray = parentIDs.Split(',');
                    foreach (var parent in parentArray)
                    {
                        if (parent != "")
                        {
                            int agentCompareID = Convert.ToInt32(parent);
                            tblAgentMaster parentAgent = dbEntity.tblAgentMasters.Where(x => x.AgentID == agentCompareID).ToList().FirstOrDefault();
                            if (String.IsNullOrEmpty(parentAgent.AgentChild))
                                parentAgent.AgentChild = newlyAddedAgent.AgentID.ToString();
                            else
                                parentAgent.AgentChild = parentAgent.AgentChild + "," + newlyAddedAgent.AgentID.ToString();
                            dbEntity.SaveChanges();
                        }
                    }



                    var agentID = dbEntity.tblAgentMasters.Where(x => x.AgenteMail == a.AgenteMail).Select(x => x.AgentID).FirstOrDefault();
                    var Projects = dbEntity.tblProjects.Where(x => x.BookingStatus == "O").ToList();
                    foreach (var project in Projects)
                    {
                        tblAgentProjectLevel agentProjectLevel = new tblAgentProjectLevel();
                        agentProjectLevel.AgentID = agentID;
                        agentProjectLevel.ProjectID = project.ProjectID;
                        agentProjectLevel.LevelID = 27;
                        agentProjectLevel.Status = "I";
                        agentProjectLevel.CreatedBy = a.CreatedBy;
                        agentProjectLevel.CreatedDate = a.CreatedDate;
                        dbEntity.tblAgentProjectLevels.Add(agentProjectLevel);
                    }
                    //tblAgentTotalPayment
                    dbEntity.SaveChanges();
                    a.isDuplicateAgentCode = false;
                    return true;
                }
                else
                {
                    a.isDuplicateAgentCode = true;
                    return false;
                }
            }
            catch(Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public List<AgentDropdown> GetAgentMapping(int AgentID, int option)
        {
            List<AgentDropdown> lstAgents = new List<AgentDropdown>();
            try
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<sp_GetAgentMapping_Result, AgentDropdown>();
                });
                IMapper mapper = config.CreateMapper();
                lstAgents = mapper.Map<List<sp_GetAgentMapping_Result>, List<AgentDropdown>>(dbEntity.sp_GetAgentMapping(AgentID,option).ToList()).ToList();

            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                ex.ToString();
            }
            return lstAgents;
        }

        public bool UpdateAgentMapping(int AgentID, int option, string agentList,string username)
        {
            try
            {
                //a.BookingStatusName = null;
                tblAgentMaster agentOld = dbEntity.tblAgentMasters.Where(x => x.AgentID == AgentID).FirstOrDefault();
                
                if (agentOld != null)
                {
                    if (option == 1)
                    {
                        agentOld.AgentParent = agentList;
                    }
                    else
                    {
                        agentOld.AgentChild = agentList;
                    }
                    agentOld.UpdatedBy = username;
                    agentOld.UpdatedDate = DateTime.Now;
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
    }
}
