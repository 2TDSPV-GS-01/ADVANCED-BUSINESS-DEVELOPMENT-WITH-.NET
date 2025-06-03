using FonteViva.DTO;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IRepository<Contato> _repository;

        public ContatoController(IRepository<Contato> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var contatos = await _repository.GetAllAsync();
            return View(contatos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var contato = await _repository.GetByIdWithIncludesAsync(id,
                c => c.Responsavel,
                c => c.Fornecedor);


            if (contato == null)
                return NotFound();

            var contatoDetalhado = new ContatoDetalhadoDto
            {
                Id = contato.Id,
                Email = contato.Email,
                Telefone = contato.Telefone,
                Fornecedor = contato.Fornecedor == null ? null : new FornecedorListagemDto
                {
                    CNPJ = contato.Fornecedor.CNPJ,
                    Nome = contato.Fornecedor.Nome,
                    IdEndereco = contato.Fornecedor.IdEndereco
                },
                Responsavel = contato.Responsavel == null ? null : new ResponsavelListagemDto
                {
                    CPF = contato.Responsavel.CPF,
                    Nome = contato.Responsavel.Nome
                }

            };

            return View(contatoDetalhado);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contato contato)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(contato);
                return RedirectToAction(nameof(Index));
            }
            return View(contato);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var contato = await _repository.GetByIdAsync(id);
            if (contato == null)
                return NotFound();

            return View(contato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contato contato)
        {
            if (id != contato.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(contato);
                return RedirectToAction(nameof(Index));
            }

            return View(contato);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var contato = await _repository.GetByIdAsync(id);
            if (contato == null)
                return NotFound();

            return View(contato);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contato = await _repository.GetByIdAsync(id);
            if (contato == null)
                return NotFound();

            await _repository.DeleteAsync(contato);
            return RedirectToAction(nameof(Index));
        }
    }
}
