using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLayer
{
    public class ConstructionPic
    {
        public int PicID { get; set; }

        [Display(Name = "Project")]
        [Required]
        public Nullable<int> ProjectID { get; set; }
        public string URL { get; set; }

        [Display(Name = "Video")]
        [Required]
        public bool isVideo { get; set; }
        public Nullable<bool> Active { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string videoID { get; set; }
    }
}
