using ShopDbAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace ShopDbAccess.DAL
{
    public class MerchandiseRepository : IMerchandiseRepository, IDisposable
    {
        private ShopContext context;

        public MerchandiseRepository(ShopContext context)
        {
            this.context = context;
        }

        public IEnumerable<Merchandise> GetAllMerchandise()
        {
            return context.Merchandise.ToList();
        }

        public Merchandise GetMerchandiseByArticle(string merchandiseArticle)
        {
            return context.Merchandise.SingleOrDefault(merchandise => merchandise.Article == merchandiseArticle);
        }

        public Merchandise GetMerchandiseByID(int ID)
        {
            return context.Merchandise.Find(ID);
        }

        public void CreateMerchandise(Merchandise merchandise)
        {
            context.Merchandise.Add(merchandise);
        }

        public void DeleteMerchandiseByArticle(string merchandiseArticle)
        {
            Merchandise merchandise = GetMerchandiseByArticle(merchandiseArticle);
            context.Merchandise.Remove(merchandise);
        }

        public void DeleteMerchandiseByID(int ID)
        {
            Merchandise merchandise = GetMerchandiseByID(ID);
            context.Merchandise.Remove(merchandise);
        }

        public void UpdateMerchandise(Merchandise merchandise)
        {
            context.Entry(merchandise).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}