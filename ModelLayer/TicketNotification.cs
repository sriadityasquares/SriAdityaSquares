using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace ModelLayer
{
    
    public class TicketNotification
    {
        static int flag = 0;
       
        
        public void Notification()
        {
            try
            {
                for (; ; )
                {
                    Thread.Sleep(8000);
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://in.bookmyshow.com/buytickets/rrr-vizag-visakhapatnam/movie-viza-ET00094579-MT/20220325");
                    //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://in.bookmyshow.com/buytickets/mathu-vadalara-hyderabad/movie-hyd-ET00121183-MT/20200104");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);
                    string result = sr.ReadToEnd();
                    result = result.Replace("\"", "");
                    var isOpened = result.Contains("INOX: Vizag Chitralaya Mall") || result.Contains("INOX: Varun Beach, Beach Road") || result.Contains("Cinepolis: Sreekanya Cineglitz, Madhurawada") || result.Contains("INOX: CMR Central, Maddilapalem");
                    if (isOpened)
                    {
                        var client1 = new RestClient("http://msg.msgclub.net/rest/services/sendSMS/sendGroupSms?AUTH_KEY=9dd349655bd3f82fb1b2fbe12ca8cbb&message=" + "RRR Opened in Inox/Cinepolis" + "&senderId=SIGNUP&routeId=1&mobileNos=" + 9505055755 + "&smsContentType=english");
                        var req = new RestRequest(Method.GET);
                        req.AddHeader("Cache-Control", "no-cache");
                        IRestResponse res = client1.Execute(req);
                        
                        flag++;
                       

                    }
                    Thread.Sleep(2000);
                    
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                if (flag == 0)
                {
                    Notification();
                }

            }
        }

       

    }
}
