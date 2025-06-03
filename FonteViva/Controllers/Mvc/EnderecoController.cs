using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Mvc
{
    public class EnderecoController : Controller
    {
        private readonly IRepository<Endereco> _repository;

        public EnderecoController(IRepository<Endereco> repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> Index()
        {
            var enderecos = await _repository.GetAllAsync();

            var listaDto = enderecos.Select(e => new EnderecoDto
            {
                Id = e.Id,
                Pais = e.Pais,
                Estado = e.Estado,
                Cidade = e.Cidade,
                Rua = e.Rua,
                CEP = e.CEP
            });
            return View(listaDto);
        }

        public async Task<IActionResult> Details(int id)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null) return NotFound();

            return View(endereco);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Endereco endereco)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(endereco);
                return RedirectToAction(nameof(Index));
            }

            return View(endereco);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null)
                return NotFound();

            return View(endereco);
        }

        // POST: /Endereco/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Endereco endereco)
        {
            if (id != endereco.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(endereco);
                return RedirectToAction(nameof(Index));
            }

            return View(endereco);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null)
                return NotFound();

            return View(endereco);
        }

        // POST: /Endereco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null)
                return NotFound();

            await _repository.DeleteAsync(endereco);
            return RedirectToAction(nameof(Index));
        }


    }
}
