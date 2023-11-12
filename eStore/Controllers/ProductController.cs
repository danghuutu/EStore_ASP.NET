using BusinessObject;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository productRepository = null;
        public ProductController() => productRepository = new ProductRepository();
        IMemberRepository memberRepository = new MemberRepository();
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        IOrderRepository orderRepository = new OrderRepository();
        // GET: HomeController
        public ActionResult Index()
        {
            var ProductList = productRepository.GetProduct();
            return View(ProductList);
        }

        // GET: HomeController/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: HomeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    productRepository.InsertProduct(product);
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View(product);
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            try
            {
                if (id != product.ProductId)
                {
                    return NotFound();
                }
                if (ModelState.IsValid)
                {
                    productRepository.UpdateProduct(product);
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
            var product = productRepository.GetProductById(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                productRepository.DeleteProduct(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public ActionResult AddToOrder(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                var product = productRepository.GetProductById(id.Value);
                var email = Request.Cookies["Email"];
                Random random = new Random();
                Member member = new Member();
                foreach (var i in memberRepository.GetMember())
                {
                    if (i.Email == email) member = i;
                }
                Order order;
                do
                {
                    order = new Order()
                    {
                        OrderId = random.Next(0, 1000),
                        MemberId = member.MemberId,
                        OrderDate = DateTime.Now
                    };
                } while (!isAvailable(order));
                OrderDetail orderDetail = new OrderDetail()
                {
                    OrderId = order.OrderId,
                    ProductId = product.ProductId,
                    UnitPrice = product.UnitPrice,
                    Quantity = 1,
                    Discount = 0
                };
                orderRepository.InsertOrder(order);
                orderDetailRepository.InsertOrderDetail(orderDetail);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
                return View();
            }
        }
        public Boolean isAvailable(Order order)
        {
            foreach (var i in orderRepository.GetOrder())
            {
                if (order.OrderId == i.OrderId) return false;
            }
            return true;
        }
    }
}
