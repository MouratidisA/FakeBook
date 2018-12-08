using FakeBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FakeBook.Startup))]

namespace FakeBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolesandUsers();
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                //create Admin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //create an Admin user                 
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@fakebook.com";

                string userPWD = "Password123!";
                var chkUser = UserManager.Create(user, userPWD);

                //Add User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating BasicUser role    
            if (!roleManager.RoleExists("BasicUser"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "BasicUser";
                roleManager.Create(role);
            }
        }
    }
}