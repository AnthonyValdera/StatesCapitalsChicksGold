using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StatesCapitals.Models;


namespace StatesCapitals.Controllers
{
    public class LoginController : Controller
    {
        private StatesAndCapitalsEntities db = new StatesAndCapitalsEntities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User objUser)
        {
            if (ModelState.IsValid)
            {
                using (StatesAndCapitalsEntities db = new StatesAndCapitalsEntities())
                {
                    var obj = db.Users.Where(x => x.UserName == objUser.UserName && x.Password == objUser.Password).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.UserId.ToString();
                        Session["UserName"] = obj.UserName.ToString();
                        return RedirectToAction("Index", "Dashboard");
                    }
                }
            }
            return View(objUser);
        }

    }
}