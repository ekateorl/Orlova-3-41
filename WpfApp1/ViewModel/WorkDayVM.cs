using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace WpfApp1.ViewModel
{
    public class WorkDayVM
    {
        public DateTime Date { get; set; }
        public User Specialist { get; set; }
        public WorkDay workDay { get; set; }
        public bool Free { get { return workDay != null; } }
        public string Time
        {
            get
            {
                if (workDay != null)
                {
                    string time = workDay.beginning.ToString();
                    time = time.Substring(0, time.Length - 3);
                    time += "-" + workDay.ending.ToString();
                    time = time.Substring(0, time.Length - 3);
                    return time;
                }
                else
                    return "Выходной";
            }
        }
        public WorkDayVM(WorkDay wd, DateTime Date, User Specialist)
        {
            workDay = wd;
            this.Specialist = Specialist;
            this.Date = Date;
        }
    }
}
