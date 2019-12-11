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
    public class AppointmentRepositorySQL : IRepository<Appointment>
    {
        private Context db;

        public AppointmentRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Appointment> GetList()
        {
            return db.Appointment.ToList();
        }

        public Appointment GetItem(int id)
        {
            return db.Appointment.Find(id);
        }

        public void Create(Appointment Appointment)
        {
            db.Appointment.Add(Appointment);
        }

        public void Update(Appointment Appointment)
        {
            db.Entry(Appointment).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Appointment Appointment = db.Appointment.Find(id);
            if (Appointment != null)
                db.Appointment.Remove(Appointment);
        }

    }
}
