using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    
        public class SpecialistWorkTime
        {
            public string Name { get; set; }
            public TimeSpan AllTime { get; set; }
            public TimeSpan AppTime { get; set; }
        }

    public class ServPopularity
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}
