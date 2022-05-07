using LoginApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using ModelLayer;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoginApp.Startup))]
namespace LoginApp
{
    public partial class Startup
    {
        internal UserManager<ApplicationUser> UserManager { get; private set; }

        public void Configuration(IAppBuilder app)
        {
           ConfigureAuth(app);
            createRolesandUsers();
           
        }
        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            try
            {
                // In Startup iam creating first Admin Role and creating a default Admin User    
                if (!roleManager.RoleExists("Admin"))
                {
                    // first we create Admin rool   
                    var role = new IdentityRole();
                    role.Name = "Admin";
                    roleManager.Create(role);
                }

                // creating Creating Client role    
                if (!roleManager.RoleExists("Client"))
                {
                    var role = new IdentityRole();
                    role.Name = "Client";
                    roleManager.Create(role);

                }
                if(!roleManager.RoleExists("Customer"))
                {
                    var role = new IdentityRole();
                    role.Name = "Customer";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Agent"))
                {
                    var role = new IdentityRole();
                    role.Name = "Agent";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("DataEntry"))
                {
                    var role = new IdentityRole();
                    role.Name = "DataEntry";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Manager"))
                {
                    var role = new IdentityRole();
                    role.Name = "Manager";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Employee"))
                {
                    var role = new IdentityRole();
                    role.Name = "Employee";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Franchise Owner"))
                {
                    var role = new IdentityRole();
                    role.Name = "Franchise Owner";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("CRM"))
                {
                    var role = new IdentityRole();
                    role.Name = "CRM";
                    roleManager.Create(role);
                }
                if (!roleManager.RoleExists("Super Admin"))
                {
                    var role = new IdentityRole();
                    role.Name = "Super Admin";
                    roleManager.Create(role);
                }


            }
            catch { }
        }
    }
}
