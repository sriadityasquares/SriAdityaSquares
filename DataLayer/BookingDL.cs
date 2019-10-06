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
                lstProjects = mapper.Map<List<tblProject>, List<Projects>>(dbEntity.tblProjects.Where(a => a.BookingStatus == 1).ToList()).ToList();
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
                lstTowers = mapper.Map<List<tblTower>, List<Towers>>(dbEntity.tblTowers.Where(a => a.ProjectID == projectID).ToList()).ToList();
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
                    cfg.CreateMap<tblFlat, Towers>();
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
    }
}
