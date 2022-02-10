using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StatesCapitals.Models;
using StatesCapitals.Quiz_Dto;


namespace StatesCapitals.Controllers
{
    public class DashboardController : Controller
    {
        StatesAndCapitalsEntities db = new StatesAndCapitalsEntities();


        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View();

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

       
        public ActionResult TestResults()
        {
            TestResult testR = new TestResult()
            {
                UserId = int.Parse(Session["UserId"].ToString())
            };
            var obj = db.TestResults.Where(x => x.UserId == testR.UserId).ToList();
            return View(obj);
        }

     
        [HttpPost]
        public JsonResult getQuestions(int q)
        {
            List<State> states = new List<State>();

            if (ModelState.IsValid)
            {
                using (StatesAndCapitalsEntities db = new StatesAndCapitalsEntities())
                {
                    states = db.States.OrderBy(x => Guid.NewGuid()).Take(q).ToList();
                    states.Select(s => s.StateId);
                }
            }
            return this.Json(states.Select(s => s.State1), JsonRequestBehavior.AllowGet);
        }

       
        public JsonResult SendAnswers(List<QuizRequest> request, int TotalQuestions)
        {
            int goodAns = 0;

            foreach (QuizRequest q in request)
            {
                var assert = db.States.SingleOrDefault(x => x.State1 == q.state && x.Capital == q.capital);
                if (assert != null)
                {
                    goodAns++;
                }
            }

            TestResult result = new TestResult
            {
                UserId = Int16.Parse(Session["UserId"].ToString()),
                TestDateTime = DateTime.Now,
                TotalQuestions = TotalQuestions,
                NumberCorrect = goodAns
            };

            db.TestResults.Add(result);

            try
            {
                db.SaveChanges();

                QuizResponse response = new QuizResponse
                {
                    data = goodAns,
                    result = "success"
                };
                return this.Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception )
            {
                QuizResponse response = new QuizResponse
                {
                    data = goodAns,
                    result = "false"
                };
                return this.Json(response, JsonRequestBehavior.AllowGet);

            }

        }


    }
}