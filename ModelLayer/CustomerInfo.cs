using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    
    public  class CustomerInfo
    {
        public int ID { get; set; }
        public int Aadhar { get; set; }
        public string Pan { get; set; }
        public string Name { get; set; }
        public string Nominee { get; set; }
        public string Relationship { get; set; }
        public Nullable<int> Age { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public Nullable<int> Pincode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public Nullable<int> Country { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> City { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.Guid> BookingID { get; set; }
    }
}
