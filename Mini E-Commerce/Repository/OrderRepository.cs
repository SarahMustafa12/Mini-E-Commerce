using Microsoft.EntityFrameworkCore;
using Mini_E_Commerce.Data_Access;
using Mini_E_Commerce.Models;
using Mini_E_Commerce.Repository.IRepository;

namespace Mini_E_Commerce.Repository
{
    public class OrderRepository : Repository<Order>,IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }

        public async Task<Order?> GetOrderWithItemsAndProducts(int orderId) => await _context.Orders
                .Include(e => e.Items)
                    .ThenInclude(e => e.Product)
                .FirstOrDefaultAsync(e => e.Id == orderId);



    }
}
