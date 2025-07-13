using Microsoft.AspNetCore.Mvc;

namespace invProj.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : Controller
{
    // GET: M
    public ActionResult Index()
    {
        return View();
    }

    // GET: M/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: M/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: M/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: M/Edit/5
    public ActionResult Edit(int id)
    {
        return View();
    }

    // POST: M/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: M/Delete/5
    public ActionResult Delete(int id)
    {
        return View();
    }

    // POST: M/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, IFormCollection collection)
    {
        try
        {
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}