using ShopDbAccess.Models;
using System;
using System.Collections.Generic;

namespace ShopDbAccess.DAL
{
    public interface IMerchandiseRepository : IDisposable
    {
        IEnumerable<Merchandise> GetAllMerchandise();
        Merchandise GetMerchandiseByArticle(string merchandiseArticle);
        Merchandise GetMerchandiseByID(int id);
        void CreateMerchandise(Merchandise merchandise);
        void DeleteMerchandiseByArticle(string merchandiseArticle);
        void DeleteMerchandiseByID(int id);
        void UpdateMerchandise(Merchandise merchandise);
        void Save();
    }
}