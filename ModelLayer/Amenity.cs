using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
        public partial class Amenity
        {
            public int AmenityID { get; set; }
            public Nullable<int> ProjectID { get; set; }
            public string Amenities { get; set; }
            public string AmenitesPicUrl { get; set; }
        }
    
}
