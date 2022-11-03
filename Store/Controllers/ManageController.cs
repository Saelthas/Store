using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Products.Service.Controllers
{
    public class ManageController : Controller
    {
        // GET: ManageProducts
        public IActionResult Index()
        {
            return View();
        }

        // GET: ManageProducts/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: ManageProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManageProducts/Create
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

        // GET: ManageProducts/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManageProducts/Edit/5
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

        // GET: ManageProducts/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManageProducts/Delete/5
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
}
