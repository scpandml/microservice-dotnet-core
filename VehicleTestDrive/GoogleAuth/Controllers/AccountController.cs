using GoogleAuth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleAuth.Controllers
{
    [AllowAnonymous, Route("account")]
    public class AccountController : Controller
    {
        [Route("google-login")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Identities.FirstOrDefault()
                .Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            List<VehicleDetails> vehicles = new List<VehicleDetails>();
            if (claims != null)
            {
                //vehicles = await HttpHelper.Get<List<VehicleDetails>>("https://localhost:7200/", "/api/Vehicles");
                vehicles = await HttpHelper.Get<List<VehicleDetails>>("https://localhost:7003/", "/gateway/vehicles");
            }

            return View(vehicles);
        }

        public async Task<IActionResult> VehicalDetails(string id)
        {

            //vehicles = await HttpHelper.Get<List<VehicleDetails>>("https://localhost:7200/", "/api/Vehicles" + id);
            var vehicles = await HttpHelper.Get<VehicleDetails>("https://localhost:7003/", "/gateway/vehicles/" + id);

            return View(vehicles);
        }
    }
}
