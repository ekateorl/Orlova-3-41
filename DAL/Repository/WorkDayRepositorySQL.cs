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
    public class WorkDayRepositorySQL : IRepository<WorkDay>
    {
        private Context db;

        public WorkDayRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<WorkDay> GetList()
        {
            return db.WorkDay.ToList();
        }

        public WorkDay GetItem(int id)
        {
            return db.WorkDay.Find(id);
        }

        public void Create(WorkDay WorkDay)
        {
            db.WorkDay.Add(WorkDay);
        }

        public void Update(WorkDay WorkDay)
        {
            db.Entry(WorkDay).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            WorkDay WorkDay = db.WorkDay.Find(id);
            if (WorkDay != null)
                db.WorkDay.Remove(WorkDay);
        }

    }
}
