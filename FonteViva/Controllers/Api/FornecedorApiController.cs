using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FornecedorApiController : ControllerBase
    {
        private readonly IRepository<Fornecedor> _repository;

        private readonly EnderecoApiController _enderecoApiController;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorListagemDto>>> Get()
        {
            var forncecedores = await _repository.GetAllAsync();

            var listaDto = forncecedores.Select(e => new FornecedorListagemDto
            {
                CNPJ = e.CNPJ,
                IdEndereco = e.IdEndereco,
                Nome = e.Nome,

            });

            return Ok(listaDto);
        }

        [HttpGet("{cnpj}")]
        public async Task<ActionResult<FornecedorDetalhadoDto>> GetById(string cnpj)
        {
            var fornecedor = await _repository.GetByIdWithIncludesAsync(cnpj,
                f => f.Contatos,
                f => f.Materials
            );
            if (fornecedor == null) return NotFound();

            return Ok(value: new FornecedorDetalhadoDto
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
            });
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorListagemDto>> Post([FromBody] FornecedorListagemDto dto)
        {
            var fornecedor = new Fornecedor
            {
                CNPJ = dto.CNPJ,
                Nome = dto.Nome,
                IdEndereco = dto.IdEndereco,
            };

            await _repository.AddAsync(fornecedor);

            return CreatedAtAction(nameof(GetById), new { id = dto.CNPJ }, dto);
        }

        [HttpPut("{cnpj}")]
        public async Task<IActionResult> Put(string cnpj, [FromBody] FornecedorListagemDto dto)
        {
            var fornecedor = await _repository.GetByIdAsync(cnpj);
            if (fornecedor == null)
                return NotFound();

            fornecedor.CNPJ = dto.CNPJ;
            fornecedor.Nome = dto.Nome;
            fornecedor.IdEndereco = dto.IdEndereco;

            await _repository.UpdateAsync(fornecedor);

            return NoContent();
        }

        [HttpDelete("{cnpj}")]
        public async Task<IActionResult> Delete(string cnpj)
        {
            var fornecedor = await _repository.GetByIdAsync(cnpj);
            if (fornecedor == null)
                return NotFound();

            await _repository.DeleteAsync(fornecedor);
            return NoContent();
        }
    }
}
