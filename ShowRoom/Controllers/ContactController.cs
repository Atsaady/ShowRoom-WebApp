using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShowRoom.Models;
using System.Net.Mail;

namespace ShowRoom.Controllers
{
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(Contact vm)
        {
            return View();
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}


