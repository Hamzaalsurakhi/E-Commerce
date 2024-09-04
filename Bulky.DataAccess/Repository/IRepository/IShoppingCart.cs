
using Bulky.Models;

namespace Bulky.DataAccess.Repository.IRepository
{
    public interface IShoppingCart :IRepository<ShoppingCart>
    {
        void Update(ShoppingCart cart);
    }
}
