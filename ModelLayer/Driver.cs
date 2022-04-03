using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class Driver
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }

        [Display(Name = "License Number")]
        [Required]
        public string LicenseNumber { get; set; }

        [Display(Name = "License Expiry")]
        [Required]
        public Nullable<System.DateTime> LicenseExpiry { get; set; }

        [Display(Name = "Current Address")]
        [Required]
        public string CurrentAddress { get; set; }

        [Display(Name = "Permanent Address")]
        [Required]
        public string PermanentAddress { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }
    }
}
