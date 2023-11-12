using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class OrderDAO
    {
        //-----------------------------------------------------------------
        //Using Singleton Pattern
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                {
                    lock (instanceLock)
                        if (instance == null)
                        {
                            instance = new OrderDAO();
                        }
                    return instance;
                }
            }
        }
        //-----------------------------------------------------------------
        public IEnumerable<Order> GetOrderList()
        {
            var orders = new List<Order>();
            try
            {
                using var context = new EstoreContext();
                orders = context.Orders.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return orders;
        }
        //-----------------------------------------------------------------
        public Order GetOrderByID(int orderID)
        {
            Order order = null;
            try
            {
                using var context = new EstoreContext();
                order = context.Orders.SingleOrDefault(o => o.OrderId == orderID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }
        public Order GetOrderByMemberID(int MemberID)
        {
            Order order = null;
            try
            {
                using var context = new EstoreContext();
                order = context.Orders.SingleOrDefault(o => o.MemberId == MemberID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }
        //-----------------------------------------------------------------
        //Add new a car
        public void AddNew(Order order)
        {
            try
            {
                Order _order = GetOrderByID(order.OrderId);
                if (_order == null)
                {
                    using var context = new EstoreContext();
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order is already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-----------------------------------------------------------------
        //Update a car
        public void Update(Order order)
        {
            try
            {
                Order _order = GetOrderByID(order.OrderId);
                if (_order != null)
                {
                    using var context = new EstoreContext();
                    context.Orders.Update(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order is not already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //-----------------------------------------------------------------
        //Remove a car
        public void Remove(int orderID)
        {
            try
            {
                Order order = GetOrderByID(orderID);
                if (order != null)
                {
                    using var context = new EstoreContext();
                    context.Orders.Remove(order);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Order is not already exists.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
