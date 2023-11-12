using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class OrderController : Controller
    {
        IOrderRepository orderRepository = null;
        public OrderController() => orderRepository = new OrderRepository();
        IMemberRepository memberRepository = new MemberRepository();
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();

        // GET: HomeController
        public ActionResult Index()
        {
            string email = Request.Cookies["Email"];
            string password = Request.Cookies["Password"];
            string admin = Request.Cookies["Admin"];
            var OrderListByMember = new List<Order>();
            if (admin == "false")
            {
                foreach (var i in memberRepository.GetMember())
                {
                    if (email == i.Email)
                    {
                        Order order = orderRepository.GetOrderByMemberId(i.MemberId);
                        if (order != null) // check if the order is not null
                        {
                            OrderListByMember.Add(order); // add the order to the list
                        }
                    }
                }
                if (OrderListByMember != null)
                {
                    return View(OrderListByMember);
                }
            }

            var OrderList = orderRepository.GetOrder();
            return View(OrderList);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            var order = new Order()
            {
                MemberId = 123,
                OrderDate = DateTime.Now
            };
            return View(order);
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    orderRepository.InsertOrder(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(order);
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Order order)
        {
            try
            {
                if (id != order.OrderId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    orderRepository.UpdateOrder(order);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = orderRepository.GetOrderById(id.Value);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                orderRepository.GetOrderById(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
    }
}
