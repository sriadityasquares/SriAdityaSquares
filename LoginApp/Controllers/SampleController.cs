using BusinessLayer;
using ModelLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class SampleController : Controller
    {
        public static List<Projects> projectList;
        BookingBL booking = new BookingBL();
        CommonBL common = new CommonBL();
        // GET: Sample
        public ActionResult Index()
        {
            //var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + "Greetings from Sri Aditya squares, Don't  miss this golden opportunity. Pay all your entire due amounts before Feb 28 and get 5gm gold as a gift.This offer valid upto Feb28th only." + "&senderId=TBTSMS&routeId=1&mobileNos=7095534898,7337464159,7569988974,7680986723,7989573027,8008210389,8008252091,8008288769,8142662288,8186019234,8237313576,8499965888,85260996464,9000018146,9000529214,9010667867,9177964674,9346128099,9441071862,9490140265,9490239613,9490348317,9515132030,9515282876,9550669954,9618297789,9642069126,9642245231,9642444001,9701376241,9703248345,9703966977,9789081577,9848252496,9849655012,9948082935,9948423303,9959484003,9959907983,9963979071,9966282602" + " & smsContentType=english");
            //var request1 = new RestRequest(Method.GET);
            //request1.AddHeader("Cache-Control", "no-cache");
            //IRestResponse response1 = client1.Execute(request1);

            return View();
        }

        public ActionResult Test()
        {
            var agentList = common.BindAgents2();
            TempData["AgentList"] = new SelectList(agentList, "AgentID", "AgentName");
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
                var htmlText = System.IO.File.ReadAllText(HttpContext.Server.MapPath("~/Templates/DueAmountReminder.html"));
                htmlText = htmlText.Replace("#Project", rem.ProjectName).Replace("#Tower", rem.TowerName).Replace("#Flat", rem.FlatName).Replace("#DueDate", rem.DueDate).Replace("#DueAmount", rem.DueAmount);

                //var path = Path.Combine(Environment.CurrentDirectory, "/Templates/DueAmountReminder.html");
                //var htmlText = System.IO.File.ReadAllText(path);

                //var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9951999884" + "&smsContentType=english");
                //var request1 = new RestRequest(Method.GET);
                //request1.AddHeader("Cache-Control", "no-cache");
                //IRestResponse response1 = client1.Execute(request1);

                //var client2 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9505055755" + "&smsContentType=english");
                //var request2 = new RestRequest(Method.GET);
                //request1.AddHeader("Cache-Control", "no-cache");
                //IRestResponse response2 = client1.Execute(request2);

                //var client3 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9566257752" + "&smsContentType=english");
                //var request3 = new RestRequest(Method.GET);
                //request1.AddHeader("Cache-Control", "no-cache");
                //IRestResponse response3 = client1.Execute(request3);
                //var emails = 
                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    msg.From = new MailAddress("Info@sasinfra.in");
                    msg.To.Add(new MailAddress(rem.AgentEmail));
                    msg.To.Add(new MailAddress(rem.CustomerEmail));
                    msg.To.Add(new MailAddress("nsrinivas78@gmail.com"));
                    msg.Subject = "Pay Due Reminder";
                    msg.IsBodyHtml = true; //to make message body as html  
                    msg.Body = htmlText;
                    smtp.Port = 587;
                    smtp.Host = "mail.privateemail.com"; //for gmail host  
                    smtp.EnableSsl = true;
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("Info@sasinfra.in", "sasinfo@123");
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtp.Send(msg);
                }
                catch (Exception) { }
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
