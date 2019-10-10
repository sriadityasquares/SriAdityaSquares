using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public partial class FlatDetails
    {
        public Nullable<int> Floor { get; set; }
        public Nullable<int> Bhk { get; set; }
        public string Facing { get; set; }
        public Nullable<int> Sft { get; set; }
        public Nullable<int> ClubHouseCharges { get; set; }
    }
}
