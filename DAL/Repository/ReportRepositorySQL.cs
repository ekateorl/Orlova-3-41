using DAL.Entities;
using DAL.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class ReportRepositorySQL : IReportsRepository
    {
        Context db;
        public ReportRepositorySQL(Context db)
        {
            this.db = db;
        } 

        public List<Appointment> ApointmentsForPeriod(DateTime date1, DateTime date2)
        {
            var request = db.Appointment
             .Where(i => i.TimeSlot.WorkDay.Date >= date1 && i.TimeSlot.WorkDay.Date <= date2)
             .ToList();
            return request;
        }

        public List<Appointment> ApointmentsForPeriod(DateTime date1, DateTime date2, int SpecialistId)
        {
            var request = db.Appointment.Where(i => i.TimeSlot.WorkDay.Date >= date1 && i.TimeSlot.WorkDay.Date <= date2 &&
            i.TimeSlot.WorkDay.SpecialistFk == SpecialistId).ToList();
            return request;
        }

        public List<SpecialistWorkTime> WorkTimes(DateTime date1, DateTime date2)
        {
            var request = db.TimeSlot.Where(i => i.WorkDay.Date >= date1 && i.WorkDay.Date <= date1).GroupBy(i => i.WorkDay.SpecialistFk)
                .Select(j => new SpecialistWorkTime
                {
                    Name = j.First().WorkDay.User.Name,
                    AllTime = new TimeSpan(j.Sum(i => i.Duration.Ticks)),
                    AppTime = new TimeSpan(j.Where(i => i.AppointmentFk != null).Sum(i => i.Duration.Ticks))

                }).OrderBy(i => i.Name).ToList();
            return request;
        }

        public List<ServPopularity> ServicePopularity(DateTime date1, DateTime date2)
        {
            var request = db.Appointment
             .Where(i => i.TimeSlot.WorkDay.Date >= date1 && i.TimeSlot.WorkDay.Date <= date2).GroupBy(i => i.ServiceFk)
             .Select(j => new ServPopularity
             {
                 Name = j.First().Service.Name,
                 Quantity = j.Count()
             }).OrderBy(i => i.Quantity).ToList();
            return request;
        }

    }
}
