using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class TreeObject
    {
        public string AgentCode { get; set; }
        public string AgentSponserCode { get; set; }
        public string AgentName { get; set; }

        public string colorScheme { get; set; }
        public IList<TreeObject> Children { get; set; } = new List<TreeObject>();


        public static IEnumerable<TreeObject> FlatToHierarchy(List<TreeObject> list)
        {
            // hashtable lookup that allows us to grab references to containers based on id
            var lookup = new Dictionary<string, TreeObject>();
            // actual nested collection to return
            var nested = new List<TreeObject>();

            foreach (TreeObject item in list)
            {
                lookup.Add(item.AgentCode, item);
            }
            foreach (TreeObject item in list)
            {
                if (lookup.ContainsKey(item.AgentSponserCode))
                {
                    // add to the parent's child list 
                    lookup[item.AgentSponserCode].Children.Add(item);
                }
                else
                {
                    // no parent added yet (or this is the first time)
                    nested.Add(item);
                }
            }
            return nested;
        }
    }
}
