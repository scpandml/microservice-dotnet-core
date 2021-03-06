using GoogleAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleAuth.Controllers
{
    [AllowAnonymous, Route("admin")]
    public class AdminController : Controller
    {
        public async Task<IActionResult> AdminPage()
        {
            var reservations = await HttpHelper.Get<List<Reservation>>("https://localhost:7003/", "/gateway/reservations");

            return View(reservations);
        }

        [HttpPost]
        public async Task<IActionResult> AddBooking(string id)
        {
            await HttpHelper.Put<int>("https://localhost:7003/", "/gateway/reservations/", Convert.ToInt32(id));
            return View();
        }
    }
}
