using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StatesCapitals.Models;

namespace StatesCapitals.Controllers
{
    public class UserRegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateUser(User objUser)
        {
            using (StatesAndCapitalsEntities db = new StatesAndCapitalsEntities())
            {
                var create = db.Users.Add(objUser);

                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index", "Login");
                }
                catch
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }
    }
}