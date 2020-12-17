using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class DashboardParameters
    {
        public Nullable<int> ProjectCount { get; set; }
        public Nullable<int> LocationCount { get; set; }
        public Nullable<int> BookingCount { get; set; }
        public Nullable<int> IBOCount { get; set; }
        public Nullable<int> TodayBooking { get; set; }
        public Nullable<int> YesterdayBookingCount { get; set; }
        public Nullable<double> BookingGrowth { get; set; }
        public Nullable<int> TodayPaymentCount { get; set; }
        public Nullable<int> YesterdayPaymnetCount { get; set; }
        public Nullable<double> PaymentGrowth { get; set; }
        public Nullable<int> TodayCustomerCount { get; set; }
        public Nullable<int> YesterdayCustomerCount { get; set; }
        public Nullable<double> CustomerGrowth { get; set; }

        public Nullable<int> TodayIBOCount { get; set; }
        public Nullable<int> YesterdayIBOCount { get; set; }
        public Nullable<double> IBOGrowth { get; set; }
    }
}
