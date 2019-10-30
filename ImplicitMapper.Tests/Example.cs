using ImplicitMapper.Tests.Classes;
using ImplicitMapper.Tests.DbEntity;
using ImplicitMapper.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ImplicitMapper.Tests
{
    [TestClass]
    public class Example
    {
        /// <summary>
        /// Db entity'sini bir model'e cast ederken bu yöntem uygulanmalýdýr.
        /// Db'de Customer tablom var. Bu tablo verilerini custom yaptýðým CustomerModel'a cast etmek istiyorum. Bu senaryoda aþaðýdaki yöntem kullanýlabilir.
        /// </summary>
        [TestMethod]
        public void DbEntity_Mapper()
        {
            //Kaydýn db'den geldiðini düþünelim...
            var dbData = new Customer
            {
                Id = 1,
                Email = "mehmet.aktas@outlook.com.tr",
                Name = "Mehmet",
                Surname = "Aktaþ"
            };

            var model = dbData.CloneTo<CustomerModel>();
            Assert.IsTrue(model.Email == dbData.Email);
            Assert.IsTrue(model.Name == dbData.Name);
            Assert.IsTrue(model.Surname == dbData.Surname);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void DbEntity_Mapper_With_Join()
        {
            //Kaydýn db'den içindeki Product'la joinli þekilde geldiðini düþünelim...
            var dbData = new OrderProduct
            {
                Id = 1,
                ProductId = 1,
                Quantity = 1,
                Product = new Product
                {
                    Id = 1,
                    Name = "Test ürünü",
                    Price = 12.90M
                }
            };

            //OrderProduct içerisindeki Product otomatik map'lenmiyor. Bunun için OrderProductModel.cs içindeki ctor'u incelemelisin.
            //Hiçbir þey kullanmadan da bu özellik kullanýlabilir.

            var model = (OrderProductModel)dbData;
            Assert.IsTrue(model.ProductId == dbData.ProductId);
            Assert.IsTrue(model.ProductName == dbData.Product.Name);
            Assert.IsTrue(model.ProductPrice == dbData.Product.Price);
            Assert.IsTrue(model.Quantity == dbData.Quantity);
        }

        [TestMethod]
        public void DbEntity_Mapper_With_List()
        {
            var dataList = new List<OrderProduct>
            {
                new OrderProduct
                {
                    Id = 1,
                    ProductId = 1,
                    Quantity = 1,
                    Product = new Product
                    {
                        Id = 1,
                        Name = "Test ürünü",
                        Price = 12.90M
                    }
                },
                new OrderProduct
                {
                    Id = 2,
                    ProductId = 2,
                    Quantity = 1,
                    Product = new Product
                    {
                        Id = 2,
                        Name = "Test ürünü",
                        Price = 12.90M
                    }
                },
                new OrderProduct
                {
                    Id = 3,
                    ProductId = 1,
                    Quantity = 1,
                    Product = new Product
                    {
                        Id = 3,
                        Name = "Test ürünü",
                        Price = 12.90M
                    }
                }
            };

            var model = dataList.ToViewModel<OrderProduct, OrderProductModel>().ToList();
            Assert.IsTrue(model.FirstOrDefault().ProductId == dataList.FirstOrDefault().ProductId);
            Assert.IsTrue(model.FirstOrDefault().ProductName == dataList.FirstOrDefault().Product.Name);
            Assert.IsTrue(model.FirstOrDefault().ProductPrice == dataList.FirstOrDefault().Product.Price);
            Assert.IsTrue(model.FirstOrDefault().Quantity == dataList.FirstOrDefault().Quantity);
        }
    }
}