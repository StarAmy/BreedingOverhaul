using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreedingOverhaul
{
    public class IncubatorData
    {
        public Dictionary<string, List<string>> IncubatorItems;

        public IncubatorData()
        {
            IncubatorItems = new Dictionary<string, List<string>>();
        }
    }
}
