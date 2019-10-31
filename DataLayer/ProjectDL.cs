using AutoMapper;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProjectDL
    {
        salesDBEntities dbEntity = new salesDBEntities();
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
            catch(Exception ex)
            {
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
            catch
            {
                return false;
            }
        }
    }
}
