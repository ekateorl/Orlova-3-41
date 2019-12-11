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
    public class ClientRepositorySQL : IRepository<Client>
    {
        private Context db;

        public ClientRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Client> GetList()
        {
            return db.Client.ToList();
        }

        public Client GetItem(int id)
        {
            return db.Client.Find(id);
        }

        public void Create(Client Client)
        {
            db.Client.Add(Client);
        }

        public void Update(Client Client)
        {
            db.Entry(Client).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Client Client = db.Client.Find(id);
            if (Client != null)
                db.Client.Remove(Client);
        }

    }
}
