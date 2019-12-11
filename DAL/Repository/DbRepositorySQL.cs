using DAL.Interfaces;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class DbRepositorySQL : IDbRepository
    {
        private Context db;
        private AppointmentRepositorySQL appointmentRepository;
        private CategoryRepositorySQL categoryRepository;
        private ClientRepositorySQL clientRepository;
        private ServiceRepositorySQL serviceRepository;
        private ServiceSpecialistRepositorySQL serviceSpecialistRepository;
        private TimeSlotRepositorySQL timeSlotRepository;
        private UserRepositorySQL userRepository;
        private UserTypeRepositorySQL userTypeRepository;
        private WorkDayRepositorySQL workDayRepository;
        private MakeAppointRepositorySQL makeAppointRepository;

        public DbRepositorySQL()
        {
            db = new Context();
        }

        public IRepository<Appointment> Appointments
        {
            get
            {
                if (appointmentRepository == null)
                    appointmentRepository = new AppointmentRepositorySQL(db);
                return appointmentRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepositorySQL(db);
                return categoryRepository;
            }
        }

        public IRepository<Service> Services
        {
            get
            {
                if (serviceRepository == null)
                    serviceRepository = new ServiceRepositorySQL(db);
                return serviceRepository;
            }
        }

        public IRepository<ServiceSpecialist> ServiceSpecialists
        {
            get
            {
                if (serviceSpecialistRepository == null)
                    serviceSpecialistRepository = new ServiceSpecialistRepositorySQL(db);
                return serviceSpecialistRepository;
            }
        }

        public IRepository<TimeSlot> TimeSlots
        {
            get
            {
                if (timeSlotRepository == null)
                    timeSlotRepository = new TimeSlotRepositorySQL(db);
                return timeSlotRepository;
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                if (clientRepository == null)
                    clientRepository = new ClientRepositorySQL(db);
                return clientRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepositorySQL(db);
                return userRepository;
            }
        }

        public IRepository<UserType> UserTypes
        {
            get
            {
                if (userTypeRepository == null)
                    userTypeRepository = new UserTypeRepositorySQL(db);
                return userTypeRepository;
            }
        }

        public IRepository<WorkDay> WorkDays
        {
            get
            {
                if (workDayRepository == null)
                    workDayRepository = new WorkDayRepositorySQL(db);
                return workDayRepository;
            }
        }
        public IMakeAppointRepository MakeAppointments
        {
            get
            {
                if (makeAppointRepository == null)
                    makeAppointRepository = new MakeAppointRepositorySQL(db);
                return makeAppointRepository;
            }
        }

      

        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
