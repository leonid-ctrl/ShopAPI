using ShopDbAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopDbAccess.Logic
{
    public static class OrderValidator
    {
        public static bool IsOrderValid(Order order)
        {
            if (order == null)
            {
                Console.WriteLine("Order was null");
                return false;
            }

            ICollection<Merchandise> ordersMerchandise = order.OrdersMerchandise;
            IEnumerable<string> uniqueArticles = ordersMerchandise.Select(merchandise => merchandise.Article).Distinct();
            int nmbOfUniqueArticles = uniqueArticles.Count();
            if (order.OrdersMerchandise != null && (nmbOfUniqueArticles > 0 && nmbOfUniqueArticles < 11))
            {
                foreach (string article in uniqueArticles)
                {
                    int nmbOfOneArticleGoods = ordersMerchandise.Where(merchandise => merchandise.Article == article).Count();
                    if (nmbOfOneArticleGoods > 99)
                    {
                        Console.WriteLine("Cannot create order with more than 99 the same merchandise");
                        return false;
                    }
                }
                return true;
            }
            Console.WriteLine("Can only create non-empty orders with up to 10 goods in it.");
            return false;
        }


        public static void CalcTotal(Order order)
        {
            foreach (Merchandise merchandise in order.OrdersMerchandise)
            {
                order.OrderTotal =+ merchandise.Price;
            }
        }
    }
}