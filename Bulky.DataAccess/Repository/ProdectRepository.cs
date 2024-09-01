using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProdeuctRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProdeuctRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


        public void Update(Product product)
        {
            
            var objProduct=_context.Products.FirstOrDefault(u => u.Id ==product.Id);
            if (objProduct != null) 
            { 
               objProduct.Title = product.Title;
                objProduct.Description = product.Description;
                objProduct.Price = product.Price;
                objProduct.ISBN = product.ISBN;
                objProduct.ListPrice = product.ListPrice;
                objProduct.Price100 = product.Price100;
                objProduct.Price50 = product.Price50;
                objProduct.Category = product.Category;
                objProduct.Author = product.Author;

                if (objProduct.ImageUrl !=null) 
                {

                    objProduct.ImageUrl=product.ImageUrl;


                }

            
            
            }

        }
    }
}
