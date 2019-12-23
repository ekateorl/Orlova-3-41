using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    
        public class SpecialistWorkTime
        {
            public User Specialist { get; set; }
            public TimeSpan AllTime { get; set; }
            public TimeSpan AppTime { get; set; }
        }

    public class ServiceInfo
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal CountCost { get; set; }
        public string CountCostString
        {
            get
            {
                string String = CountCost.ToString();
                return String.Substring(0, String.Length-5);
            }
        }
    }
}
