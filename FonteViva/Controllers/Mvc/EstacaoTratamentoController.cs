using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers
{
    public class EstacaoTratamentoController : Controller
    {
        private readonly IRepository<EstacaoTratamento> _repository;

        public EstacaoTratamentoController(IRepository<EstacaoTratamento> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _repository.GetAllAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(int id)
        {
            var estacao = await _repository.GetByIdWithIncludesAsync(id,
                e => e.Responsavel,
                e => e.Sensors
            );

            if (estacao == null)
                return NotFound();

            return View(estacao);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EstacaoTratamento estacao)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(estacao);
                return RedirectToAction(nameof(Index));
            }
            return View(estacao);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var estacao = await _repository.GetByIdAsync(id);
            if (estacao == null)
                return NotFound();

            return View(estacao);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EstacaoTratamento estacao)
        {
            if (id != estacao.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(estacao);
                return RedirectToAction(nameof(Index));
            }

            return View(estacao);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var estacao = await _repository.GetByIdAsync(id);
            if (estacao == null)
                return NotFound();

            return View(estacao);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estacao = await _repository.GetByIdAsync(id);
            if (estacao == null)
                return NotFound();

            await _repository.DeleteAsync(estacao);
            return RedirectToAction(nameof(Index));
        }
    }
}
