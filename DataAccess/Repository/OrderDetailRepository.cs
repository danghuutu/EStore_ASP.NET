using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(int OrderId) => OrderDetailDAO.Instance.Remove(OrderId);
        public IEnumerable<OrderDetail> GetOrderDetail() => OrderDetailDAO.Instance.GetOrderDetailList();
        public OrderDetail GetOrderDetailById(int OrderId) => OrderDetailDAO.Instance.GetOrderDetailByOrderID(OrderId);
        public void InsertOrderDetail(OrderDetail order) => OrderDetailDAO.Instance.AddNew(order);
        public void UpdateOrderDetail(OrderDetail order) => OrderDetailDAO.Instance.Update(order);

    }


}
