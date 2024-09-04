using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;


namespace Bulky.DataAccess.Repository
{
    public class ShoppingCartRepository :Repository<ShoppingCart>,IShoppingCart
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(ShoppingCart cart)
        { 
             _context.ShoppingCarts.Update(cart);
        }
    }
}
