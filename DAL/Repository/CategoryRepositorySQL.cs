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
    public class CategoryRepositorySQL : IRepository<Category>
    {
        private Context db;

        public CategoryRepositorySQL(Context dbcontext)
        {
            this.db = dbcontext;
        }

        public List<Category> GetList()
        {
            return db.Category.ToList();
        }

        public Category GetItem(int id)
        {
            return db.Category.Find(id);
        }

        public void Create(Category Category)
        {
            db.Category.Add(Category);
        }

        public void Update(Category Category)
        {
            db.Entry(Category).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Category Category = db.Category.Find(id);
            if (Category != null)
                db.Category.Remove(Category);
        }

    }
}
