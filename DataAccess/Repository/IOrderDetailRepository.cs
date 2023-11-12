using BusinessObject;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetOrderDetail();
        OrderDetail GetOrderDetailById(int OrderId);
        void InsertOrderDetail(OrderDetail order);
        void UpdateOrderDetail(OrderDetail order);
        void DeleteOrderDetail(int OrderId);

    }
}