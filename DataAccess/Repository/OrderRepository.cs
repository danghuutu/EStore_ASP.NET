using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(int OrderId) => OrderDAO.Instance.Remove(OrderId);
        public IEnumerable<Order> GetOrder() => OrderDAO.Instance.GetOrderList();
        public Order GetOrderById(int OrderId) => OrderDAO.Instance.GetOrderByID(OrderId);
        public void InsertOrder(Order order) => OrderDAO.Instance.AddNew(order);
        public void UpdateOrder(Order order) => OrderDAO.Instance.Update(order);
        public Order GetOrderByMemberId(int MemberId) => OrderDAO.Instance.GetOrderByMemberID(MemberId);
    }

}
