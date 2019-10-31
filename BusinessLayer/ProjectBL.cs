using DataLayer;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class ProjectBL
    {
        ProjectDL project = new ProjectDL();
        public bool UpdateProject(Projects p)
        {
            return project.UpdateProject(p);
        }

        public bool AddProject(Projects p)
        {
            return project.AddProject(p);
        }

    }
}
