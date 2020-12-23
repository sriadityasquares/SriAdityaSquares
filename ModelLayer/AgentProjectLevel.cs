﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public partial class AgentProjectLevel
    {

        public int AgentID { get; set; }
        public int ProjectID { get; set; }
        public Nullable<int> LevelID { get; set; }
        public string Status { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedDate { get; set; }

        public string AgentName { get; set; }

        public string ProjectName { get; set; }

        public string AgentParent { get; set; }
        public string AgentChild { get; set; }
    }
}
