using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IDbRepository
    {
        IRepository<Appointment> Appointments { get; }
        IRepository<Category> Categories { get; }
        IRepository<Client> Clients { get; }
        IRepository<Service> Services { get; }
        IRepository<ServiceSpecialist> ServiceSpecialists { get; }
        IRepository<TimeSlot> TimeSlots { get; }
        IRepository<User> Users { get; }
        IRepository<UserType> UserTypes { get; }
        IRepository<WorkDay> WorkDays { get; }
        IMakeAppointRepository MakeAppointments { get; }
        int Save();
    }
}
