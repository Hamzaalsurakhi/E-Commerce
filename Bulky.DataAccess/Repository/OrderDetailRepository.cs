﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Bulky.Models;


namespace Bulky.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail> , IOrderDetailRepository
    {
     
        private  readonly ApplicationDbContext _context;
        public OrderDetailRepository(ApplicationDbContext context) :base(context) 
        {
            _context = context;
        }
       

        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
        }
    }
}
