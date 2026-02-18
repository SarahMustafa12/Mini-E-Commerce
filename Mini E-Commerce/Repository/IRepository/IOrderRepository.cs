using Mini_E_Commerce.Models;

namespace Mini_E_Commerce.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order?> GetOrderWithItemsAndProducts(int orderId);

    }
}
