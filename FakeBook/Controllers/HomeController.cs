using System.Linq;
using System.Web.Mvc;

namespace FakeBook.Controllers
{
    public class HomeController : Controller
    {
        private FakeBookEntities db = new FakeBookEntities();

        public ActionResult Index()
        {
            
            Session["AuthStatus"] = User.Identity.IsAuthenticated;           
            var currentUser = db.AspNetUsers.Where(u => u.Email == User.Identity.Name).FirstOrDefault();

            /* There is no current user*/
            if (currentUser == null)
            {
                ViewBag.FullName = "Unknown";
                ViewBag.Role = "Coming soon";
            }
            else
            {                
                ViewBag.FullName = currentUser.LastName + " " + currentUser.FirstName;                                               
                var admin = User.IsInRole("Admin");
                Session["Role"] = admin ? "Admin" : "BasicUser";
                ViewBag.Role = admin ? "Admin" : "BasicUser";
            }
          
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}