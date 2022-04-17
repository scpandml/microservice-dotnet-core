using GoogleAuth.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAuth.Controllers
{
    [AllowAnonymous, Route("customer")]
    public class CustomerController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> AddCustomer(Customer customer)
        {
            customer.Vehicle = new Vehicle { Id = 3, Name = "Model Y" };
            StringContent content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
            //await HttpHelper.Post<Customer>("https://localhost:7003/", "/gateway/customers/", customer);
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsync("https://localhost:7003/gateway/customers/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }

            return View(customer);
        }
    }
}
