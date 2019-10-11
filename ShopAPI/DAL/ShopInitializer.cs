using ShopDbAccess.Models;
using System.Collections.Generic;

namespace ShopDbAccess.DAL
{
    public class ShopInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<ShopContext>
    {
        public static List<Merchandise> MerchandiseList { get; } = new List<Merchandise>
            {
                new Merchandise{Article="001", Name="Carrot", Price=9.4 },
                new Merchandise{Article="002", Name="Tomato", Price=15.4 },
                new Merchandise{Article="003", Name="Salt", Price=3.4 },
                new Merchandise{Article="004", Name="Pumpkin", Price=10 },
                new Merchandise{Article="005", Name="Potato", Price=8 },
                new Merchandise{Article="006", Name="Pepper", Price=40.5},
                new Merchandise{Article="007", Name="Paprika", Price=23.3},
                new Merchandise{Article="008", Name="Onion", Price=8.5},
                new Merchandise{Article="009", Name="Garlic", Price=30.8},
                new Merchandise{Article="010", Name="Tea", Price=46.56},
                new Merchandise{Article="011", Name="Coffee", Price=67.54},
                new Merchandise{Article="012", Name="Squash", Price=6.7},
                new Merchandise{Article="013", Name="Eggplant", Price=12.4}
            };
        protected override void Seed(ShopContext context)
        {
            MerchandiseList.ForEach(m => context.Merchandise.Add(m));
            context.SaveChanges();

            List<Merchandise> maxMerchandise = new List<Merchandise>();
            int i = 0;
            while (i < 10)
            {
                int j = 0;
                while (j < 99)
                {
                    maxMerchandise.Add(MerchandiseList[i]);
                    j++;
                }
                i++;
            }

            var orders = new List<Order>
            {
                new Order{OrdersMerchandise=new List<Merchandise> { MerchandiseList[0], MerchandiseList[0], MerchandiseList[0], MerchandiseList[0], MerchandiseList[0], MerchandiseList[0]}},
                new Order{OrdersMerchandise=new List<Merchandise> { MerchandiseList[0], MerchandiseList[1], MerchandiseList[1], MerchandiseList[2], MerchandiseList[2], MerchandiseList[4]}},
                new Order{OrdersMerchandise=new List<Merchandise> { MerchandiseList[0], MerchandiseList[1], MerchandiseList[2], MerchandiseList[3], MerchandiseList[4], MerchandiseList[5], MerchandiseList[6], MerchandiseList[7], MerchandiseList[8], MerchandiseList[9]}},
                new Order{OrdersMerchandise=new List<Merchandise> { MerchandiseList[10], MerchandiseList[11], MerchandiseList[4], MerchandiseList[5], MerchandiseList[5], MerchandiseList[5], MerchandiseList[5], MerchandiseList[5]}},
                new Order{OrdersMerchandise=maxMerchandise}
            };
            orders.ForEach(o =>
            {
                double total = 0;
                foreach (Merchandise merchandise1 in o.OrdersMerchandise)
                {
                    total = +merchandise1.Price;
                }
                o.OrderTotal = total;
            });
            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();
        }
    }
}