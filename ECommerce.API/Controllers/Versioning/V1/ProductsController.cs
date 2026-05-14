using Asp.Versioning;
using ECommerce.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers.Versioning.V1
{

    [ApiController]
    [ApiVersion("1.0",Deprecated =true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private static List<Product> products = new List<Product>
                    {
                        new Product { Id = 1, Name = "Laptop", Price = 1000 },
                        new Product { Id = 2, Name = "Phone", Price = 500 },
                        new Product { Id = 3, Name = "Book", Price = 1500 },

                    };

        [HttpGet]
        public ActionResult GetAll()
        {
            //throw new Exception("Database connection failed");

            return Ok("This is version 1");
        }

        [HttpGet("{id}")]

        public Product GetById(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        [HttpPost]

        public Product Create([FromHeader] Product product)
        {
            product.Id = products.Max(p => p.Id) + 1;
            products.Add(product);
            return product;
        }

        [HttpPut("{id}")]

        public Product Update(int id,Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is null) return null;

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            return product;
        }

        [HttpDelete("{id}")]

        public List<Product> Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product is null) return null;

            products.Remove(product);

            return products;
        }
    }
}
