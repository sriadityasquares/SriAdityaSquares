using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class AgentDropdown
    {
        [Display(Name = "IBO")]
        public int AgentID { get; set; }
        public string AgentName { get; set; }

        //public string isSelected { get; set; }

        public Nullable<bool> selected { get; set; }
    }
}
