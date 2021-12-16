using BusinessLayer;
using ModelLayer;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LoginApp.Controllers
{
    public class SampleController : Controller
    {
        public static List<Projects> projectList;
        BookingBL booking = new BookingBL();
        CommonBL common = new CommonBL();
        static int flag = 0;
        const string SmtpServer = "smtp.live.com";
        const int SmtpPort = 587;
        const string FromAddress = "manoj8@live.com";
        const string Password = "ma29hu94";
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
            //SendMail("manojvenkat8@gmail.com", "Vaccine Opened Venkat Test", "Vaccine Opened for 18+");
            SendMail("venkata.godithi@evolutyz.com", "Vaccine Opened for Venkat Test", "Vaccine Opened for 18+");
           
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

        public static void SendMail(string toAddress, string subject, string body)
        {
           
            
            var client = new SmtpClient(SmtpServer, SmtpPort)
            {
                Credentials = new NetworkCredential(FromAddress, Password),
                EnableSsl = true,
               

            };
            MailMessage mailMessage = new MailMessage();
            mailMessage.To.Add(toAddress);
            mailMessage.Body = body;
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            string Username = "Evolutyz";
            mailMessage.From = new MailAddress("Venkat <noreply@evolutyz.com>");
            //"\\SomeOne\\");
            //mailMessage.From.User = "Evolutyz Team";
            client.Send(mailMessage);
        }
    }
}
