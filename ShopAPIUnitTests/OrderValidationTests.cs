using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopDbAccess.DAL;
using ShopDbAccess.Logic;
using ShopDbAccess.Models;
using System.Collections.Generic;

namespace ShopDbAccessUnitTests
{
    [TestClass]
    public class Tests
    {
        Order mixedSmallOrder = new Order()
        {
            OrdersMerchandise = new List<Merchandise> { ShopInitializer.MerchandiseList[0], ShopInitializer.MerchandiseList[1],
                ShopInitializer.MerchandiseList[2], ShopInitializer.MerchandiseList[3], ShopInitializer.MerchandiseList[4],
                ShopInitializer.MerchandiseList[5], ShopInitializer.MerchandiseList[6], ShopInitializer.MerchandiseList[7],
                ShopInitializer.MerchandiseList[8], ShopInitializer.MerchandiseList[9] }
        };
        Order minimalOrder = new Order { OrdersMerchandise = new List<Merchandise> { ShopInitializer.MerchandiseList[0] } };

        [TestMethod]
        public void IsOrderWithMaxArticlesAndQuantityValid_True()
        {
            var maxMerchandise = new List<Merchandise>();
            int i = 0;
            while (i < 10)
            {
                int j = 0;
                while (j < 99)
                {
                    maxMerchandise.Add(ShopInitializer.MerchandiseList[i]);
                    j++;
                }
                i++;
            }
            Assert.IsTrue(OrderValidator.IsOrderValid(new Order { OrdersMerchandise = maxMerchandise }));
        }

        [TestMethod]
        public void IsOrderWithTooManyArticlesValid_False()
        {
            var tooManyArticles = new List<Merchandise>();
            int i = 0;
            while (i < 11)
            {
                tooManyArticles.Add(ShopInitializer.MerchandiseList[i]);
                i++;
            }
            Assert.IsFalse(OrderValidator.IsOrderValid(new Order { OrdersMerchandise = tooManyArticles }));
        }

        [TestMethod]
        public void IsOrderWithTooManyItemsOfOneArticleValis_False()
        {
            var tooManyItems = new List<Merchandise>();
            int i = 0;
            while (i < 101)
            {
                tooManyItems.Add(ShopInitializer.MerchandiseList[0]);
                i++;
            }
            Assert.IsFalse(OrderValidator.IsOrderValid(new Order { OrdersMerchandise = tooManyItems }));
        }

        [TestMethod]
        public void IsMixedSmallOrderValid_True()
        {
            Assert.IsTrue(OrderValidator.IsOrderValid(mixedSmallOrder));
        }

        [TestMethod]
        public void isMinimalOrderValid_True()
        {
            Assert.IsTrue(OrderValidator.IsOrderValid(minimalOrder));
        }

        [TestMethod]
        public void IsOrderMixedSmallOrderTotalCalculatedCorrectly_True()
        {
            double expectedTotal = 0;
            OrderValidator.CalcTotal(mixedSmallOrder);
            foreach (Merchandise merchandise in mixedSmallOrder.OrdersMerchandise)
            {
                expectedTotal = +merchandise.Price;
            }
            Assert.AreEqual(expectedTotal, mixedSmallOrder.OrderTotal);
        }

        [TestMethod]
        public void IsNewMinimalOrderTotalCalculatedCorrectly_True()
        {
            double expectedTotal = ShopInitializer.MerchandiseList[0].Price;
            OrderValidator.CalcTotal(minimalOrder);
            Assert.AreEqual(expectedTotal, minimalOrder.OrderTotal);
        }
    }
}