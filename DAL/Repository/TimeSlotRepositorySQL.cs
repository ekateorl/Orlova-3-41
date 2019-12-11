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
    public class TimeSlotRepositorySQL : IRepository<TimeSlot>
    {
        private Context db;

        public TimeSlotRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<TimeSlot> GetList()
        {
            return db.TimeSlot.ToList();
        }

        public TimeSlot GetItem(int id)
        {
            return db.TimeSlot.Find(id);
        }

        public void Create(TimeSlot TimeSlot)
        {
            db.TimeSlot.Add(TimeSlot);
        }

        public void Update(TimeSlot TimeSlot)
        {
            db.Entry(TimeSlot).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            TimeSlot TimeSlot = db.TimeSlot.Find(id);
            if (TimeSlot != null)
                db.TimeSlot.Remove(TimeSlot);
        }

    }
}
