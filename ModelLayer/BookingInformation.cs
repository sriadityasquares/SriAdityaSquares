using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public partial class BookingInformation
    {
        public Guid BookingID { get; set; }
        [Required]
        [Display(Name = "Project")]
        public Nullable<int> ProjectID { get; set; }
        [Required]
        [Display(Name = "Tower")]
        public Nullable<int> TowerID { get; set; }

        [Required]
        [Display(Name = "Flat")]
        public Nullable<int> FlatID { get; set; }
        public Nullable<int> Floor { get; set; }
        public Nullable<int> Bhk { get; set; }
        public Nullable<int> Area { get; set; }
        public string Facing { get; set; }
        [Required]
        [Display(Name = "SFT Rate")]
        public Nullable<int> SftRate { get; set; }
        [Display(Name = "High Rise")]
        public Nullable<int> HighRiseCharges { get; set; }
        [Required]
        public Nullable<int> Discount { get; set; }
        public Nullable<int> TotalRate { get; set; }
        [Display(Name = "Final")]
        public Nullable<int> FinalRate { get; set; }
        [Display(Name = "Club House")]
        public Nullable<int> ClubHouseCharges { get; set; }
        [Display(Name = "Grand Total")]
        public Nullable<int> GrandRate { get; set; }
        [Display(Name = "Mode")]
        public string PaymentModeID { get; set; }
        public string PaymentMode { get; set; }
        [Display(Name = "Ref No")]
        public string ReferenceNo { get; set; }
        [Display(Name = "Booking Amount")]
        public Nullable<int> BookingAmount { get; set; }
        [Display(Name = "Balance Amount")]
        public Nullable<int> BalanceAmount { get; set; }
        [Required]
        [Display(Name = "Agent")]
        public Nullable<int> AgentID { get; set; }

        public Nullable<double> AgentComm { get; set; }
        public Nullable<double> SASComm { get; set; }
        public Nullable<double> SASTDS { get; set; }
        public Nullable<double> SASNet { get; set; }
        [Required]
        public Nullable<int> Aadhar { get; set; }
        [Required]
        public string Name { get; set; }

        public int ID { get; set; }
        [Required]
        [Display(Name = "Pan card")]
        public string Pan { get; set; }
        public string Nominee { get; set; }
        [Required]
        [Display(Name = "Relation")]
        public string Relationship { get; set; }
        [Required]
        public Nullable<int> Age { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public Nullable<int> Pincode { get; set; }
        [Required]
        public Nullable<int> Mobile { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public Nullable<int> Country { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> City { get; set; }
        public int PaymentID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public string ProjectName { get; set; }
        public string TowerName { get; set; }
        public string FlatName { get; set; }
        public string AgentName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }

        public Nullable<int> Year { get; set; }
        public Nullable<int> Month { get; set; }
        public Nullable<int> Day { get; set; }

    }
}
