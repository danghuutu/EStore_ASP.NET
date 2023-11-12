using BusinessObject;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrder();
        Order GetOrderById(int OrderId);
        void InsertOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(int OrderId);
        Order GetOrderByMemberId(int MemberId);
    }
}