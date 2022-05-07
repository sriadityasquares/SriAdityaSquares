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


        public bool AddProjectExpenseCategory(ProjectExpenseCategory p)
        {
            return admin.AddProjectExpenseCategory(p);
        }

        public bool UpdateProjectExpenseCategory(ProjectExpenseCategory p)
        {
            return admin.UpdateProjectExpenseCategory(p);
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

        public List<LandLord> BindLandlords()
        {
            return admin.BindLandlords();
        }

        public bool AddLandLord(LandLord landlord)
        {
            return admin.AddLandLord(landlord);
        }

        public bool UpdateLandLord(LandLord landlord)
        {
            return admin.UpdateLandLord(landlord);
        }

        public bool AddLandLordPayments(LandlordPayments lp)
        {
            return admin.AddLandLordPayments(lp);
        }

        public bool UpdateLandLordPayments(LandlordPayments landlord)
        {
            return admin.UpdateLandLordPayments(landlord);
        }

        public List<LandlordPayments> GetPayments()
        {
            return admin.GetPayments();
        }

        public bool UpdateUserLockOut(GetUsersWithRoles gs)
        {
            return admin.UpdateUserLockOut(gs);
        }

        public List<Investor> BindInvestors()
        {
            return admin.BindInvestors();
        }

        public bool AddInvestor(Investor landlord)
        {
            return admin.AddInvestor(landlord);
        }

        public bool UpdateInvestor(Investor landlord)
        {
            return admin.UpdateInvestor(landlord);
        }

        public bool SaveProjectImages(ProjectPics cp)
        {
            return admin.SaveProjectImages(cp);
        }

        public List<ProjectPics> BindProjectGallery(string section = "")
        {
            return admin.BindProjectGallery(section);
        }

        public bool UpdateProjectImage(ProjectPics pp)
        {
            return admin.UpdateProjectImage(pp);
        }

    }
}
