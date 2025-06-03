using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers
{
    public class ResponsavelController : Controller
    {
        private readonly IRepository<Responsavel> _repository;

        public ResponsavelController(IRepository<Responsavel> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _repository.GetAllAsync();
            return View(lista);
        }

        public async Task<IActionResult> Details(string cpf)
        {
            var responsavel = await _repository.GetByIdWithIncludesAsync(cpf,
                r => r.Contatos,
                r => r.EstacaoTratamentos
            );

            if (responsavel == null)
                return NotFound();

            var detalhado = new ResponsavelDetalhadoDto
            {
                CPF = responsavel.CPF,
                Nome = responsavel.Nome,
                Contatos = responsavel.Contatos.Select(c => new ContatoDto
                {
                    Id = c.Id,
                    Email = c.Email,
                    Telefone = c.Telefone
                }).ToList(),
                EstacaoTratamentos = (ICollection<EstacaoTratamentoDto>)responsavel.EstacaoTratamentos.Select(e => new EstacaoListagemDto
                {
                    Id = e.Id,
                    Status = e.Status,
                    DataInstalacao = e.DataInstalacao
                }).ToList()
            };

            return View(detalhado);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Responsavel responsavel)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(responsavel);
                return RedirectToAction(nameof(Index));
            }
            return View(responsavel);
        }

        public async Task<IActionResult> Edit(string cpf)
        {
            var responsavel = await _repository.GetByIdAsync(cpf);
            if (responsavel == null)
                return NotFound();

            return View(responsavel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string cpf, Responsavel responsavel)
        {
            if (cpf != responsavel.CPF)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(responsavel);
                return RedirectToAction(nameof(Index));
            }

            return View(responsavel);
        }

        public async Task<IActionResult> Delete(string cpf)
        {
            var responsavel = await _repository.GetByIdAsync(cpf);
            if (responsavel == null)
                return NotFound();

            return View(responsavel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string cpf)
        {
            var responsavel = await _repository.GetByIdAsync(cpf);
            if (responsavel == null)
                return NotFound();

            await _repository.DeleteAsync(responsavel);
            return RedirectToAction(nameof(Index));
        }
    }
}
