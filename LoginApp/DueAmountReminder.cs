//using Quartz;
using BusinessLayer;
using ModelLayer;
using Quartz;
using RestSharp;
//using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace LoginApp
{
    public class DueAmountReminder : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            
        }

        Task IJob.Execute(IJobExecutionContext context)
        {
            ReportBL report = new ReportBL();
            var reminders = report.GetDueReminders();
            foreach (var rem in reminders)
            {
                var message = @"Greetings from Sri Aditya Squares ,it's a Gentle Reminder about your due payment of your property  to avoid penality charges.Please ignore if you already paid ." + Environment.NewLine +
                               "Project: #Project" + Environment.NewLine +
                               "Tower: #Tower" + Environment.NewLine +
                               "Flat: #Flat" + Environment.NewLine +
                               "DueDate: #DueDate" + Environment.NewLine +
                               "DueAmount: #DueAmount" + Environment.NewLine;
                message = message.Replace("#Project", rem.ProjectName).Replace("#Tower", rem.TowerName).Replace("#Flat", rem.FlatName).Replace("#DueDate", rem.DueDate).Replace("#DueAmount", rem.DueAmount);
                var path = System.AppContext.BaseDirectory + @"Templates\DueAmountReminder.html";
                //var fullPath = System.IO.Path.Combine(path, @"\Templates\DueAmountReminder.html");
                var htmlText = System.IO.File.ReadAllText(path);
                htmlText = htmlText.Replace("#Project", rem.ProjectName).Replace("#Tower", rem.TowerName).Replace("#Flat", rem.FlatName).Replace("#DueDate", rem.DueDate).Replace("#DueAmount", rem.DueAmount);

                var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos="+rem.CustomerMobile + "&smsContentType=english");
                var request1 = new RestRequest(Method.GET);
                request1.AddHeader("Cache-Control", "no-cache");
                IRestResponse response1 = client1.Execute(request1);

                var client2 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=" +rem.AgentMobile + "&smsContentType=english");
                var request2 = new RestRequest(Method.GET);
                request2.AddHeader("Cache-Control", "no-cache");
                IRestResponse response2 = client2.Execute(request2);

                var client3 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=8121751751" + "&smsContentType=english");
                var request3 = new RestRequest(Method.GET);
                request3.AddHeader("Cache-Control", "no-cache");
                IRestResponse response3 = client3.Execute(request3);


                var client4 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=05423a92390551e9ff5b1b8836a187f&message=" + message + "&senderId=SIGNUP&routeId=1&mobileNos=9505055755" + "&smsContentType=english");
                var request4 = new RestRequest(Method.GET);
                request4.AddHeader("Cache-Control", "no-cache");
                IRestResponse response4 = client4.Execute(request4);

                SMS sms = new SMS();
                sms.MessageType = "Payment Reminder";
                sms.Message = message;
                sms.Recipients = rem.AgentMobile + "," + rem.CustomerMobile + "," + "8121751751";
                sms.CreatedBy = "Automated Reminder";
                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                var indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                //var sysDate = Convert.ToDateTime(indianTime).ToString("dd/MM/yyyy");
                sms.CreatedDate = indianTime;
                CommonBL common = new CommonBL();
                common.LogSMS(sms);


                try
                {
                    MailMessage msg = new MailMessage();
                    SmtpClient smtp = new SmtpClient();
                    msg.From = new MailAddress("Info@sasinfra.in");
                    msg.To.Add(new MailAddress(rem.AgentEmail));
                    msg.To.Add(new MailAddress(rem.CustomerEmail));
                    msg.To.Add(new MailAddress("nsrinivas78@gmail.com"));
                    msg.To.Add(new MailAddress("manojvenkat8@gmail.com"));
                    msg.To.Add(new MailAddress("Info@sasinfra.in"));
                    msg.Subject = "SAS : Payment Due Reminder";
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
            //var message = "Hello, we got a new enquiry from the below Customer :\n  Customer Name :" + Environment.NewLine + "Mobile no : 9505055755" + Environment.NewLine + "Location :" + Environment.NewLine + "Requirement :";

            return null;
        }
    }



}