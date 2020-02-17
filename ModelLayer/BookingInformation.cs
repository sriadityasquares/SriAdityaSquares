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
        [Required]
        public Nullable<int> Floor { get; set; }
        [Required]
        public Nullable<int> Bhk { get; set; }
        [Required]
        public Nullable<int> Area { get; set; }
        [Required]
        public string Facing { get; set; }
        [Required]
        [Display(Name = "SFT Rate")]
        public Nullable<int> SftRate { get; set; }
        [Required]
        [Display(Name = "High Rise")]
        public Nullable<int> HighRiseCharges { get; set; }
        [Required]
        public Nullable<int> Discount { get; set; }
        [Required]
        public Nullable<int> TotalRate { get; set; }
        [Required]
        [Display(Name = "Final")]
        public Nullable<int> FinalRate { get; set; }
        [Required]
        [Display(Name = "Club House")]
        public Nullable<int> ClubHouseCharges { get; set; }
        [Required]
        [Display(Name = "Grand Total")]
        public Nullable<int> GrandRate { get; set; }
        [Required]
        [Display(Name = "Mode")]
        public string PaymentModeID { get; set; }
        [Required]
        public string PaymentMode { get; set; }

        [Required]
        [Display(Name = "Scheme")]
        public string SchemeID { get; set; }
        [Required]
        public string SchemeName { get; set; }
        [Display(Name = "Ref No")]
        [Required]
        public string ReferenceNo { get; set; }
        [Required]
        [Display(Name = "Booking Amount")]
        public Nullable<int> BookingAmount { get; set; }
        [Required]
        [Display(Name = "Balance Amount")]
        public Nullable<int> BalanceAmount { get; set; }
        [Required]
        [Display(Name = "Agent")]
        public int AgentID { get; set; }

        public Nullable<double> AgentComm { get; set; }
        public Nullable<double> SASComm { get; set; }
        public Nullable<double> SASTDS { get; set; }
        public Nullable<double> SASNet { get; set; }
        public Nullable<int> Aadhar { get; set; }
        [Required]
        public string Name { get; set; }

        public int ID { get; set; }
        [Display(Name = "Pan card")]
        public string Pan { get; set; }
        public string Nominee { get; set; }

        [Display(Name = "Relation")]
        public string Relationship { get; set; }
        public Nullable<int> Age { get; set; }
        public string Dob { get; set; }
        public string Address { get; set; }
        public Nullable<int> Pincode { get; set; }
        public Nullable<int> Mobile { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public Nullable<int> Country { get; set; }
        public Nullable<int> State { get; set; }
        public Nullable<int> City { get; set; }
        public int PaymentID { get; set; }
        public int AgentPaymentID { get; set; }
        public string CreatedBy { get; set; }
        
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Display(Name="Date")]
        public string BookingDate { get; set; }

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

        public Nullable<int> Level { get; set; }

        public Nullable<int> PaymentTimePeriod { get; set; }
        public Nullable<int> DownPaymentTimePeriod { get; set; }
        public Nullable<System.DateTime> DueDate { get; set; }

        public Nullable<double> AgentTDS { get; set; }
        public Nullable<double> AgentNet { get; set; }
        public Nullable<double> TotalComm { get; set; }
        public Nullable<double> AgentNetPaid { get; set; }
        public Nullable<double> AgentNetBalance { get; set; }

        public Nullable<double> SASNetPaid { get; set; }
        public Nullable<double> SASNetBalance { get; set; }

        public string AgentParent { get; set; }
    }
}
