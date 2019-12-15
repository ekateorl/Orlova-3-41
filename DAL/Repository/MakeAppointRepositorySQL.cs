using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    class MakeAppointRepositorySQL : IMakeAppointRepository
    {
        private Context db;

        public MakeAppointRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Service> SelectServices(int CategoryId)
        {
            var request = db.Service.Where(i => i.CategoryFk == CategoryId).ToList();
            return request;
        }

        public List<User> SelectSpecialists(int ServiceId)
        {
            var request = db.ServiceSpecialist
                .Where(i => i.ServiceFk == ServiceId)
                .Join(db.User, i => i.SpecialistFk, j => j.UserId, (i, j) => j)
                .Join(db.User, i => i.UserId, j => j.UserId, (i, j) => j).ToList();
            return request;
        }

        public List<TimeSlot> SelectTime(int WorkDayId, int ServiceId)
        {
            Service s = db.Service.Where(i => i.ServiceId == ServiceId).ToList().First();
            var request = db.TimeSlot.Where(i => i.WorkDayFk == WorkDayId && i.AppointmentFk == null).OrderBy(i => i.Beginning).ToList();
            List<TimeSlot> result = new List<TimeSlot>();
            for (int i = 0; i < request.Count(); i++)
            {
                TimeSpan time = request.ElementAt(i).Duration;
                for (int j = i + 1; j < request.Count() && time <= s.Duration; j++)
                {
                    if (request.ElementAt(j).Beginning == request.ElementAt(j-1).Beginning + request.ElementAt(j-1).Duration)
                        time += request.ElementAt(j).Duration;
                    else break;
                }
                if (time >= s.Duration)
                    result.Add(request.ElementAt(i));
            }
            return result;
        }

        public List<TimeSlot> SelectTime(int WorkDayId, TimeSpan beginning)
        {
            var ts = db.TimeSlot.Where(i => i.WorkDayFk == WorkDayId && i.Beginning >= beginning)
                .OrderBy(i => i.Beginning).ToList();
            return ts;
        }

        public List<WorkDay> SelectWorkDay(int SpecialistId)
        {
            Context db = new Context();
            var request = db.WorkDay.Where(i => i.SpecialistFk == SpecialistId).ToList();
            return request;
        }
    }
}
