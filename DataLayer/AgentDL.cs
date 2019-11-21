using AutoMapper;
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
        public bool UpdateAgent(AgentMaster a)
        {
            try
            {
                a.BookingStatusName = null;
                tblAgentMaster agentOld = dbEntity.tblAgentMasters.Where(x => x.AgentID == a.AgentID).FirstOrDefault();

                if (agentOld != null)
                {
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
                return false;
            }
        }

        public bool AddAgent(AgentMaster a)
        {
            try
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

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
