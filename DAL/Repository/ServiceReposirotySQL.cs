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
    public class ServiceRepositorySQL : IRepository<Service>
    {
        private Context db;

        public ServiceRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Service> GetList()
        {
            return db.Service.ToList();
        }

        public Service GetItem(int id)
        {
            return db.Service.Find(id);
        }

        public void Create(Service Service)
        {
            db.Service.Add(Service);
        }

        public void Update(Service Service)
        {
            db.Entry(Service).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Service Service = db.Service.Find(id);
            if (Service != null)
                db.Service.Remove(Service);
        }

    }
}
