﻿using AutoFixture;
using AutoFixture.AutoMoq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using WebEngine.Data;
using WebEngine.Entities;
using WebEngine.Repositories;
using Xunit;

namespace WebEngine.Test
=======
using Xunit;

namespace WebEngine
>>>>>>> gw/25/ceneoControllerTests
{
    public class CeneoControllerTests
    {
        private IList<Product> _products;

        [Fact]
        public void ShouldReturnAllProducts()
        {
            //Arange
            var options = new DbContextOptionsBuilder<ProductContext>()
                .UseInMemoryDatabase(databaseName: "ShouldGetAllProducts")
                .Options;

            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            _products = fixture.Create<IList<Product>>();

            using (var context = new ProductContext(options))
            {
                foreach (var product in _products)
                {
                    context.Add(product);
                    context.SaveChanges();
                }
            }

            //Act
            IList<Product> actualProducts;
            using (var context = new ProductContext(options))
            {
                var repository = new ProductRepository(context);
                actualProducts = repository.GetAll().ToList();
            }

            //Assert
            Assert.Equal(_products.Count, actualProducts.Count);
        }
    }
}
