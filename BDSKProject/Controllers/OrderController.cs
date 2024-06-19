
using AutoMapper;
using DTO;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace BDSKProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]


    public class OrderController : ControllerBase
    {
        public readonly IOrderService orderService;
        public readonly IMapper mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> AddOrder([FromBody] OrderDTO orderDTO)
        {
            
            Order o = new Order() { 
                Price=orderDTO.Price,
                UserId=orderDTO.UserId

            };
            List<OrderItem> orderItems = mapper.Map<List<OrderItemDTO>, List< OrderItem>>(orderDTO.OrderItems.ToList());

            Order orderRes = await orderService.AddOrder(o,orderItems);

            if (orderRes != null)
            {
                return Ok(orderRes);
            }
            else if (orderRes == null)
            {
                return NoContent();
            }
            return BadRequest();
        }
      
    }
}
