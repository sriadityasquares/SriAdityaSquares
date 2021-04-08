using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class GetProjectWiseArea
    {
        public Nullable<int> ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<int> TotalSft { get; set; }
        public Nullable<int> SoldSft { get; set; }
        public Nullable<int> RemSft { get; set; }
    }
}
