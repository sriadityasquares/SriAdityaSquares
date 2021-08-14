using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ReconBL
    {
        ReconDL db = new ReconDL();
        public bool AddSuppliers(Suppliers s)
        {
            return db.AddSuppliers(s);
        }

        public List<Suppliers> GetSuppliers()
        {
            return db.GetSuppliers();
        }

        public bool UpdateSuppliers(Suppliers s)
        {
            return db.UpdateSuppliers(s);
        }
    }
}
