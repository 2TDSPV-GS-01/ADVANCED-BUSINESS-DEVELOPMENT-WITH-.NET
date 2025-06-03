using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IRepository<Material> _repository;

        public MaterialController(IRepository<Material> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var materiais = await _repository.GetAllAsync();
            return View(materiais);
        }

        public async Task<IActionResult> Details(int id)
        {
            var material = await _repository.GetByIdWithIncludesAsync(id, m => m.Fornecedor);
            if (material == null)
                return NotFound();

            return View(material);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Material material)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(material);
                return RedirectToAction(nameof(Index));
            }

            return View(material);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var material = await _repository.GetByIdAsync(id);
            if (material == null)
                return NotFound();

            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Material material)
        {
            if (id != material.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(material);
                return RedirectToAction(nameof(Index));
            }

            return View(material);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var material = await _repository.GetByIdAsync(id);
            if (material == null)
                return NotFound();

            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var material = await _repository.GetByIdAsync(id);
            if (material == null)
                return NotFound();

            await _repository.DeleteAsync(material);
            return RedirectToAction(nameof(Index));
        }
    }
}
