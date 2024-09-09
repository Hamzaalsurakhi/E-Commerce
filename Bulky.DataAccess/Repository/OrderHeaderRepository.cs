using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;


namespace Bulky.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader> , IOrderHeaderRepository
    {
     
        private  readonly ApplicationDbContext _context;
        public OrderHeaderRepository(ApplicationDbContext context) :base(context) 
        {
            _context = context;
        }
       

        public void Update(OrderHeader orderHeader)
        {
            _context.OrderHeaders.Update(orderHeader);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFormDb = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (orderFormDb != null)
            {
                orderFormDb.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus)) { 
                  
                orderFormDb.PaymentStatus= paymentStatus;
                }

            }
        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntnId)
        {
            var orderFormDb = _context.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if (!string.IsNullOrEmpty(sessionId))
            {

                orderFormDb.SessionId= sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntnId))
            {

                orderFormDb.PaymentIntenId= paymentIntnId;
                orderFormDb.PaymentDate=DateTime.Now;

            }
        }
    }
}
