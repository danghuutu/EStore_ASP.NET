using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDetailDAO
    {

        //-----------------------------------------------------------------
        //Using Singleton Pattern
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                {
                    lock (instanceLock)
                        if (instance == null)
                        {
                            instance = new OrderDetailDAO();
                        }
                    return instance;
                }
            }
        }
        //-----------------------------------------------------------------
        public IEnumerable<OrderDetail> GetOrderDetailList()
        {
            var orderDetails = new List<OrderDetail>();
            try
            {
                using var context = new EstoreContext();
                orderDetails = context.OrderDetails.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetails;
        }
        //-----------------------------------------------------------------
        public OrderDetail GetOrderDetailByOrderID(int orderDetailID)
        {
            OrderDetail orderDetail = null;
            try
            {
                using var context = new EstoreContext();
                orderDetail = context.OrderDetails.SingleOrDefault(c => c.OrderId == orderDetailID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orderDetail;
        }
        //-----------------------------------------------------------------
        //Add new a car
        public void AddNew(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail _orderDetail = GetOrderDetailByOrderID(orderDetail.OrderId);
                if (_orderDetail == null)
                {
                    using var context = new EstoreContext();
                    context.OrderDetails.Add(orderDetail);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("OrderDetail is already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-----------------------------------------------------------------
        //Update a car
        public void Update(OrderDetail orderDetail)
        {
            try
            {
                OrderDetail _orderDetail = GetOrderDetailByOrderID(orderDetail.OrderId);
                if (_orderDetail != null)
                {
                    using var context = new EstoreContext();
                    context.OrderDetails.Update(orderDetail);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("OrderDetail is not already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-----------------------------------------------------------------
        //Remove a car
        public void Remove(int orderDetailID)
        {
            try
            {
                OrderDetail orderDetail = GetOrderDetailByOrderID(orderDetailID);
                if (orderDetail != null)
                {
                    using var context = new EstoreContext();
                    context.OrderDetails.Remove(orderDetail);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("OrderDetail is not already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
