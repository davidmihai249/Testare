using System;
using System.Collections.Generic;
using System.Configuration;
using NUnit.Framework;
using ShoppingCart.Model;
using ShoppingCart.Repository;

namespace ShoppingCart.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestTotal()
        {
            IDictionary<String, string> props = new SortedList<String, String>();
            props.Add("ConnectionString", GetConnectionStringByName("ShoppingCartDB"));
            IRepositoryProduct repositoryProduct = new ProductRepository(props);
            IRepositoryShippingRate repositoryShippingRate = new ShippingRateRepository(props);
            Service service = new Service(repositoryProduct, repositoryShippingRate);
            Product product = service.findProductByName("Monitor");
            service.addToCart(product);
            Assert.AreEqual(221.99,service.calculateTotal());
            service.addToCart(product);
            Product product2 = service.findProductByName("Mouse");
            service.addToCart(product2);
            Assert.AreEqual(456.97, service.calculateTotal());

        }
        
        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            
            ConnectionStringSettings settings =ConfigurationManager.ConnectionStrings[name];
            
            if (settings != null)
                returnValue = settings.ConnectionString;

            return returnValue;
        }
    }
}