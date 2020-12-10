using BusinessLayer;
using ModelLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class SampleController : Controller
    {
        public static List<Projects> projectList;
        BookingBL booking = new BookingBL();
        // GET: Sample
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Details()
        {
            ReportBL report = new ReportBL();
            var reminders = report.GetDueReminders();
            foreach (var rem in reminders)
            {
                var message = @"Greetings from Sri Aditya Squares ,it's a Gentle Reminder about your due payment of your property  to avoid penality charges.Please ignore if you already paid ."+Environment.NewLine+
                               "Project: #Project"+Environment.NewLine+
                               "Tower: #Tower" + Environment.NewLine +
                               "Flat: #Flat" + Environment.NewLine +
                               "DueDate: #DueDate" + Environment.NewLine +
                               "DueAmount: #DueAmount" + Environment.NewLine;
                //rem.CustomerMobile = "9505055755";
                //rem.AgentMobile = 9492983529;
                message = message.Replace("#Project", rem.ProjectName).Replace("#Tower", rem.TowerName).Replace("#Flat", rem.FlatName).Replace("#DueDate", rem.DueDate).Replace("#DueAmount", rem.DueAmount);
                var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9951999884" + "&smsContentType=english");
                var request1 = new RestRequest(Method.GET);
                request1.AddHeader("Cache-Control", "no-cache");
                IRestResponse response1 = client1.Execute(request1);

                var client2 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9505055755" + "&smsContentType=english");
                var request2 = new RestRequest(Method.GET);
                request1.AddHeader("Cache-Control", "no-cache");
                IRestResponse response2 = client1.Execute(request2);

                var client3 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9566257752" + "&smsContentType=english");
                var request3 = new RestRequest(Method.GET);
                request1.AddHeader("Cache-Control", "no-cache");
                IRestResponse response3 = client1.Execute(request3);
            }
            projectList = booking.BindProjects();
            return Json(projectList, JsonRequestBehavior.AllowGet);
        }
        //// GET: Sample/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: Sample/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sample/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sample/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sample/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sample/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sample/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
