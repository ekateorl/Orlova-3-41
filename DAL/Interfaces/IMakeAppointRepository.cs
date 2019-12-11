using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IMakeAppointRepository
    {
        List<Service> SelectServices(int CategoryId);
        List<User> SelectSpecialists(int ServiceId);
        List<TimeSlot> SelectTime(int WorkDayId, int ServiceId);
        List<TimeSlot> SelectTime(int WorkDayId, TimeSpan beginning);
        List<WorkDay> SelectWorkDay(int SpecialistId);
    }
}
