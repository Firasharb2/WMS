using System;
using System.Linq;
using System.Web.Mvc;

namespace WMS.Controllers
{
    public class AccessController : Controller
    {
        WMSdbEntities db = new WMSdbEntities();

        // GET: Access
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(SystemUser user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(user.Name) && !string.IsNullOrEmpty(user.Password))
                    {

                        var u = db.SystemUsers.Where(x => x.Name == user.Name && x.Password == user.Password).FirstOrDefault();

                        if (u != null)
                        {
                            user.SystemUserID = u.SystemUserID;
                            Session["userId"] = user.SystemUserID;
                            Session["userName"] = user.Name;
                            Session["userPassword"] = user.Password;
                            return RedirectToAction("index", "home");
                        }
                        else
                        {
                            TempData["Message"] = "Login Failed";
                        }
                        return View(user);
                    }
                }
            }

            catch (Exception ex)
            {
                TempData["Message"] = "Login Failed" + ex.InnerException.ToString();
            }
            return RedirectToAction("login");
        }

        public ActionResult Logout(SystemUser user)
        {
            return View();
        }
    }
}