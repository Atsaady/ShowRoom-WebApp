using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShowRoom.Models;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using ShowRoom.Data;


namespace ShowRoom.Controllers
{
    public class MainController : Controller
    {

        private readonly ShowRoomContext _context;
        public MainController(ShowRoomContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == "Email")?.Value;
            try
            {
                Account ac = _context.Account.First(s => s.Email == userEmail);
                if (ac.Type == userType.Admin)
                {
                    return RedirectToAction("index", "AdminData");
                }
                else
                    return View();
            }
            catch (Exception e)
            {
                return View();
            }
        }

        public IActionResult ModalPopUp()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }


    }
}