using FonteViva.Controllers.Api;
using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Mvc
{
    public class FornecedorController : Controller
    {
        private readonly IRepository<Fornecedor> _repository;

        private readonly EnderecoApiController _enderecoApiController;

        public FornecedorController(IRepository<Fornecedor> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var fornecedores = await _repository.GetAllAsync();

            var listDto = fornecedores.Select(f => new FornecedorListagemDto
            {
                IdEndereco = f.IdEndereco,
                CNPJ = f.CNPJ,
                Nome = f.Nome,
            });
            return View(listDto);
        }

        public async Task<IActionResult> Details(string cnpj)
        {
            var fornecedor = await _repository.GetByIdWithIncludesAsync(cnpj,
                f => f.Contatos,
                f => f.Materials
            );

            if (fornecedor == null)
                return NotFound();

            var fornecedorDetalhado = new FornecedorDetalhadoDto
            {
                CNPJ = fornecedor.CNPJ,
                Nome = fornecedor.Nome,
                Contatos = fornecedor.Contatos.Select(c => new ContatoDto
                {
                    Id = c.Id,
                    Email = c.Email,
                    Telefone = c.Telefone
                }).ToList(),
                Endereco = (await _enderecoApiController.GetById(fornecedor.IdEndereco)).Value!,
                Materials = fornecedor.Materials.Select(m => new MaterialDto
                {
                    Id = m.Id,
                    Nome = m.Nome,
                    Tipo = m.Tipo,
                    Quantidade = m.Quantidade,
                    Preco = m.Preco,
                    CNPJ = m.CNPJ
                }).ToList()
            };

            return View(fornecedorDetalhado);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(fornecedor);
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        public async Task<IActionResult> Edit(string cnpj)
        {
            var fornecedor = await _repository.GetByIdAsync(cnpj);
            if (fornecedor == null)
                return NotFound();
            return View(fornecedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string cnpj, Fornecedor fornecedor)
        {
            if (cnpj != fornecedor.CNPJ)
                return BadRequest();
            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(fornecedor);
                return RedirectToAction(nameof(Index));
            }
            return View(fornecedor);
        }

        public async Task<IActionResult> Delete(string cnpj)
        {
            var fornecedor = await _repository.GetByIdAsync(cnpj);
            if (fornecedor == null)
                return NotFound();
            return View(fornecedor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string cnpj)
        {
            var fornecedor = await _repository.GetByIdAsync(cnpj);
            if (fornecedor == null)
                return NotFound();
            await _repository.DeleteAsync(fornecedor);
            return RedirectToAction(nameof(Index));
        }
    }
}
