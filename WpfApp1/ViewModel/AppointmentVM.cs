using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;

namespace WpfApp1.ViewModel
{
    public class AppointmentVM
    {
        public int AppointmentId { get; set; }

        public bool Done { get; set; }

        public string Paid { get; set; }

        public decimal Price { get; set; }

        public string PriceString { get; set; }

        public virtual Client Client { get; set; }

        public virtual Service Service { get; set; }

        public virtual TimeSlot TimeSlot { get; set; }

        public virtual User User { get; set; }

        public string Time { get; set; }

        public AppointmentVM(Appointment a, IDbRepository crud)
        {
            AppointmentId = a.AppointmentId;
            Done = a.Done;
            if (Done)
                Paid = "Оплачено";
            else
                Paid = "Не оплачено";
            Price = a.Price;
            PriceString = Price.ToString();
            PriceString = PriceString.Substring(0, PriceString.Length - 2);
            PriceString += " руб.";
            Client = a.Client;
            Service = a.Service;
            TimeSlot = a.TimeSlot;
            User = a.User;
            Time = TimeSlot.Beginning.ToString();
            Time = Time.Substring(0, Time.Length - 3);
            TimeSlot slot2 = crud.TimeSlots.GetList().Where(i => i.AppointmentFk == AppointmentId).OrderBy(i => i.Beginning).ToList().Last();
            Time += "-" + (slot2.Beginning + slot2.Duration).ToString();
            Time = Time.Substring(0, Time.Length - 3);
        }
    }
}
