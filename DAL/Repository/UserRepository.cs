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
    public class UserRepositorySQL : IRepository<User>
    {
        private Context db;

        public UserRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<User> GetList()
        {
            return db.User.ToList();
        }

        public User GetItem(int id)
        {
            return db.User.Find(id);
        }

        public void Create(User User)
        {
            db.User.Add(User);
        }

        public void Update(User User)
        {
            db.Entry(User).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User User = db.User.Find(id);
            if (User != null)
                db.User.Remove(User);
        }

    }
}
