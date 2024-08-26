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
            _context.Products.Update(product);
        }
    }
}
