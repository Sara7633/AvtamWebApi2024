using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AvtamWebApi2024Context orderContext;

        public OrderRepository(AvtamWebApi2024Context orderContext)
        {
            this.orderContext = orderContext;
        }

        public async Task<Order> AddOrder(Order order, List<OrderItem> orderItems)
        {
            try
            {
                await orderContext.Orders.AddAsync(order);
                await orderContext.SaveChangesAsync();

                foreach (var item in orderItems)
                {
                    item.OrderId = order.OrderId;
                    await orderContext.OrderItems.AddAsync(item);
                }

                await orderContext.SaveChangesAsync();
                return order;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}