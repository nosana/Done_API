using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class ErrorPageController : Controller
    {
        
        /// kalo null unAthorized
        
        public IActionResult UnAunAthorized()
        {
            return View();
        }
       
        /// kalo beda roll itu forbiden
        
        public IActionResult Forbidden()
        {
            return View();
        }
    }
}
