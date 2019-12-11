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
    public class UserTypeRepositorySQL : IRepository<UserType>
    {
        private Context db;

        public UserTypeRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<UserType> GetList()
        {
            return db.UserType.ToList();
        }

        public UserType GetItem(int id)
        {
            return db.UserType.Find(id);
        }

        public void Create(UserType UserType)
        {
            db.UserType.Add(UserType);
        }

        public void Update(UserType UserType)
        {
            db.Entry(UserType).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UserType UserType = db.UserType.Find(id);
            if (UserType != null)
                db.UserType.Remove(UserType);
        }

    }
}
