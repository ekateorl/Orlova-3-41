using DAL.Entities;
using DAL.Interfaces;

using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            /*var request = db.TimeSlot.Where(i => i.WorkDay.Date >= date1 && i.WorkDay.Date <= date2).GroupBy(i => i.WorkDay.SpecialistFk)
                .Select(j => new SpecialistWorkTime
                {
                    Name = j.FirstOrDefault().WorkDay.User.Name,
                    AllTime = (TimeSpan)DbFunctions.CreateTime(0, 0, j.Sum(p => DbFunctions.DiffSeconds(p.Duration, TimeSpan.Zero))),
                    AppTime = (TimeSpan)DbFunctions.CreateTime(0, 0, j.Where(i => i.AppointmentFk != null).Sum(p => DbFunctions.DiffSeconds(p.Duration, TimeSpan.Zero)))
                }).OrderBy(i => i.Name).ToList();*/
            List<SpecialistWorkTime> request = db.User.Where(i => i.UserType.Name == "Мастер").Select(j => new SpecialistWorkTime
            {
                Specialist = j,
                AllTime=new TimeSpan(0,0,0),
                AppTime= new TimeSpan(0, 0, 0)
            }).ToList();
            foreach (SpecialistWorkTime s in request)
            {
                List<TimeSlot> timeSlots = db.TimeSlot.Where(i => i.WorkDay.Date >= date1 && i.WorkDay.Date <= date2 &&
                i.WorkDay.SpecialistFk == s.Specialist.UserId).ToList();
                foreach(TimeSlot t in timeSlots)
                {
                    s.AllTime += t.Duration;
                    if (t.AppointmentFk != null)
                        s.AppTime+=t.Duration;
                }
            }
            return request.OrderBy(i => i.AllTime).ToList();
        }

        public List<ServiceInfo> ServiceInfos(DateTime date1, DateTime date2)
        {
            var request = db.Appointment
             .Where(i => i.TimeSlot.WorkDay.Date >= date1 && i.TimeSlot.WorkDay.Date <= date2 && i.Done).GroupBy(i => i.ServiceFk)
             .Select(j => new ServiceInfo
             {
                 Name = j.FirstOrDefault().Service.Name,
                 Quantity = j.Count(),
                 CountCost = j.Sum(i=>i.Price)
             }).OrderByDescending(i => i.Quantity).ToList();
            return request;
        }

        public List<ServiceInfo> ServiceInfos(DateTime date1, DateTime date2, int SpecialistId)
        {
            var request = db.Appointment
             .Where(i => i.TimeSlot.WorkDay.Date >= date1 && i.TimeSlot.WorkDay.Date <= date2 && i.TimeSlot.WorkDay.SpecialistFk == SpecialistId &&
             i.Done).GroupBy(i => i.ServiceFk).Select(j => new ServiceInfo
             {
                 Name = j.FirstOrDefault().Service.Name,
                 Quantity = j.Count(),
                 CountCost = j.Sum(i => i.Price)
             }).OrderBy(i => i.Quantity).ToList();
            return request;
        }

    }
}
