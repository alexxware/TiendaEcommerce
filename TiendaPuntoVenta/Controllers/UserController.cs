using Microsoft.AspNetCore.Mvc;

namespace TiendaPuntoVenta.Controllers;

public class UserController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}