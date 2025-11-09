using BookStore.DataAccess.Repository.IRepository;
using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(Product obj)
        {
            var productFromDb = _context.Products.FirstOrDefault(p => p.Id == obj.Id);
            if (productFromDb != null)
            {
                productFromDb.Name = obj.Name;
                productFromDb.Description = obj.Description;
                productFromDb.ISBN = obj.ISBN;
                productFromDb.Author = obj.Author;
                productFromDb.ListPrice = obj.ListPrice;
                productFromDb.Price = obj.Price;
                productFromDb.Price50 = obj.Price50;
                productFromDb.Price100 = obj.Price100;
                productFromDb.CategoryId = obj.CategoryId;
                productFromDb.CoverTypeId = obj.CoverTypeId;

                if (obj.ImageUrl != null)
                {
                    productFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}