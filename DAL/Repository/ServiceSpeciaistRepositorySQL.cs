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
    public class ServiceSpecialistRepositorySQL : IRepository<ServiceSpecialist>
    {
        private Context db;

        public ServiceSpecialistRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<ServiceSpecialist> GetList()
        {
            return db.ServiceSpecialist.ToList();
        }

        public ServiceSpecialist GetItem(int id)
        {
            return db.ServiceSpecialist.Find(id);
        }

        public void Create(ServiceSpecialist ServiceSpecialist)
        {
            db.ServiceSpecialist.Add(ServiceSpecialist);
        }

        public void Update(ServiceSpecialist ServiceSpecialist)
        {
            db.Entry(ServiceSpecialist).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            ServiceSpecialist ServiceSpecialist = db.ServiceSpecialist.Find(id);
            if (ServiceSpecialist != null)
                db.ServiceSpecialist.Remove(ServiceSpecialist);
        }

    }
}
