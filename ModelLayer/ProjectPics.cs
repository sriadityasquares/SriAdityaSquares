using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ModelLayer
{
    public class ProjectPics
    {
        public int Id { get; set; }
        public string URL { get; set; }
        public Nullable<bool> Active { get; set; }
        public string Section { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public HttpPostedFileBase File { get; set; }

        [Display(Name = "Project")]
        [Required]
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
}
