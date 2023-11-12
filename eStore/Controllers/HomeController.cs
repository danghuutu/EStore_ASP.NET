using BusinessObject;
using DataAccess.Repository;
using eStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace eStore.Controllers
{
    public class HomeController : Controller
    {
        IMemberRepository memberRepository = null;
        public HomeController() => memberRepository = new MemberRepository();
        // GET: HomeController
        public ActionResult Index()
        {
            Member member = new Member();
            var email = Request.Cookies["Email"];
            foreach (var i in memberRepository.GetMember())
            {
                if (i.Email == email) member = i;
            }
            return View(member);
        }
    }
}