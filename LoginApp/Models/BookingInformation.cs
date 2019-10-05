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
        public int CountryID { get; set; }

        [Display(Name = "State")]
        public int stateID { get; set; }

        [Display(Name = "City")]
        public int CityID { get; set; }

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
        public int AadhaarNo { get; set; }

        [Display(Name = "PanCard")]
        public int PanCardNo { get; set; }

        public int PinCode { get; set; }

        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Floor { get; set; }
        public string BHK { get; set; }
        public int Facing { get; set; }

        public int Area { get; set; }

        [Display(Name = "High Rise")]
        public int HighRiseCharges { get; set; }

        [Display(Name = "Maintenance")]
        public int MaintenanceCharges { get; set; }
        [Display(Name = "Club House")]
        public int ClubHouseCharges { get; set; }

        [Display(Name = "Sqft Rate")]
        public int SqftRate { get; set; }

        [Display(Name = "Total Rate")]
        public int TotalRate { get; set; }

        [Display(Name = "Agent")]
        public int AgentID { get; set; }
        [Display(Name = "Agent %")]
        public int AgentCharges { get; set; }


        public int Discount { get; set; }

        [Display(Name = "Final Rate")]
        public int FinalRate { get; set; }
        [Display(Name = "Booking Amount")]
        public int BookingAmount { get; set; }
        [Display(Name = "Balance Amount")]
        public int BalanceAmount { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; }

        public string ReferenceNo { get; set; }



    }
}