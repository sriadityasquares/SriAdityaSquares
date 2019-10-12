using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoginApp.Models
{
    public class BookingInformation
    {
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is Required")]
        public int CountryID { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is Required")]
        public int stateID { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is Required")]
        public int CityID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        [Display(Name = "Project")]
        public int ProjectID { get; set; }

        [Display(Name = "Tower")]
        public int TowerID { get; set; }

        [Display(Name = "Flat")]
        public int FlatID { get; set; }

        public string Nominee { get; set; }
        public string Relation { get; set; }
        public int Age { get; set; }
        public DateTime DOB { get; set; }

        [Display(Name = "Aadhaar")]
        public Nullable<int> AadhaarNo { get; set; }

        [Display(Name = "PanCard")]
        public string PanCardNo { get; set; }

        public Nullable<int> PinCode { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Floor { get; set; }
        public string BHK { get; set; }
        public string Facing { get; set; }

        public int Area { get; set; }

        [Display(Name = "High Rise")]
        public Nullable<int> HighRiseCharges { get; set; }

        [Display(Name = "Maintenance")]
        public Nullable<int> MaintenanceCharges { get; set; }
        [Display(Name = "Club House")]
        public Nullable<int> ClubHouseCharges { get; set; }

        [Display(Name = "SFT Rate")]
        public Nullable<int> SqftRate { get; set; }



        [Display(Name = "Total Rate")]
        public Nullable<int> TotalRate { get; set; }

        [Display(Name = "Agent")]
        public int AgentID { get; set; }
        [Display(Name = "Agent %")]
        public Nullable<int> AgentCharges { get; set; }


        public Nullable<int> Discount { get; set; }

        [Display(Name = "Final")]
        public Nullable<int> FinalRate { get; set; }

        [Display(Name = "Grand Total")]
        public Nullable<int> GrandRate { get; set; }

        [Display(Name = "Booking Amount")]
        public Nullable<int> BookingAmount { get; set; }
        [Display(Name = "Balance Amount")]
        public Nullable<int> BalanceAmount { get; set; }

        [Display(Name = "Mode")]
        public string PaymentMode { get; set; }

        [Display(Name = "Ref No")]
        public string ReferenceNo { get; set; }



    }
}