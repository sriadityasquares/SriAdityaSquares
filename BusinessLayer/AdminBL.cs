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

        public bool UpdateFlat(Flats f)
        {
            return admin.UpdateFlat(f);
        }

        public bool AddFlat(Flats f)
        {
            return admin.AddFlat(f);
        }

        public bool AddAgentProjectLevel(AgentProjectLevel a)
        {
            return admin.AddAgentProjectLevel(a);
        }

        public bool UpdateAgentProjectLevel(AgentProjectLevel a)
        {
            return admin.UpdateAgentProjectLevel(a);
        }

        public bool UpdateLevel(LevelsMaster a)
        {
            return admin.UpdateLevel(a);
        }

        public bool AddLevel(LevelsMaster a)
        {
            return admin.AddLevel(a);
        }

        public bool UpdateScheme(Schemes a)
        {
            return admin.UpdateScheme(a);
        }

        public bool AddScheme(Schemes a)
        {
            return admin.AddScheme(a);
        }

        public List<GetUsersWithRoles> GetUsersWithRoles()
        {
            return admin.GetUsersWithRoles();
        }
    }
}
