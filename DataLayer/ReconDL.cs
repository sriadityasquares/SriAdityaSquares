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
    public class ReconDL
    {
        salesDBEntities dbEntity = new salesDBEntities();
        private static readonly ILog log =
           LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public bool AddSuppliers(Suppliers s)
        {
            try
            {
                tblSupplier sinfo = new tblSupplier();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Suppliers, tblSupplier>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<Suppliers, tblSupplier>(s, sinfo);
                dbEntity.tblSuppliers.Add(sinfo);
                dbEntity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error :" + ex);
                return false;
            }
        }

        public List<Suppliers> GetSuppliers()
        {
            try
            {
                //var roleID = dbEntity.AspNetUserLogins.Where(x=>x.)
                //lstCountry = dbEntity.tblProjects.ToList();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<tblSupplier, Suppliers>();
                });
                IMapper mapper = config.CreateMapper();
                return mapper.Map<List<tblSupplier>, List<Suppliers>>(dbEntity.tblSuppliers.ToList()).ToList();
            }
            catch (Exception ex)
            {
                return new List<Suppliers>();
            }
        }

        public bool UpdateSuppliers(Suppliers s)
        {
            try
            {
                var oldSupplier = dbEntity.tblSuppliers.Where(x => x.SupplierID == s.SupplierID).FirstOrDefault();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Suppliers, tblSupplier>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
                });
                IMapper mapper = config.CreateMapper();
                mapper.Map<Suppliers, tblSupplier>(s, oldSupplier);

                dbEntity.SaveChanges();
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
