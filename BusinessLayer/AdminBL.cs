using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class AdminBL
    {
        AdminDL admin = new AdminDL();
        public bool UpdateProject(Projects p)
        {
            return admin.UpdateProject(p);
        }

        public bool AddProject(Projects p)
        {
            return admin.AddProject(p);
        }

        public bool UpdateTower(Towers p)
        {
            return admin.UpdateTower(p);
        }

        public bool AddTower(Towers p)
        {
            return admin.AddTower(p);
        }
    }
}
