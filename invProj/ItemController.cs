using Microsoft.AspNetCore.Mvc;

namespace invProj;

public class ItemController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}