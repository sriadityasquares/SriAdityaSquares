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
        // GET: Sample
        public ActionResult Index()
        {
            var client = new RestClient("http://msg.msgclub.net/rest/services/transaction/transactionLog?AUTH_KEY=YourAuthKey");
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");
            IRestResponse response = client.Execute(request);
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

                //var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9951999884" + "&smsContentType=english");
                //var request1 = new RestRequest(Method.GET);
                //request1.AddHeader("Cache-Control", "no-cache");
                //IRestResponse response1 = client1.Execute(request1);

                //var client2 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9505055755" + "&smsContentType=english");
                //var request2 = new RestRequest(Method.GET);
                //request1.AddHeader("Cache-Control", "no-cache");
                //IRestResponse response2 = client1.Execute(request2);

                //var client3 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=TBTSMS&routeId=1&mobileNos=9566257752" + "&smsContentType=english");
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
