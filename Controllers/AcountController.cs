using System.Linq;
using System.Web.Mvc;
using techsolution.Models;
using System.Web.Security;

namespace techsolution.Controllers
{
    [AllowAnonymous]
    public class AcountController : Controller
    {
        // GET: Acount
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership Models)
        {
            using (var context = new Tech_EmpireEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == Models.UserName && x.Password == Models.Password);

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(Models.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid user name and password");
                       return View();
            }

        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User Models)
        {
            using (var context = new Tech_EmpireEntities())
            {
                context.User.Add(Models);
                context.SaveChanges();
            }
            return RedirectToAction("Login");

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}