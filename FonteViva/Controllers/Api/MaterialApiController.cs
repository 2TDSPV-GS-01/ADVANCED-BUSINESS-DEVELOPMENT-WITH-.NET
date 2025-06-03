using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class MaterialApiController : ControllerBase
    {
        private readonly IRepository<Material> _repository;

        public MaterialApiController(IRepository<Material> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> Get()
        {
            var materiais = await _repository.GetAllAsync();

            var lista = materiais.Select(m => new MaterialDto
            {
                Id = m.Id,
                Nome = m.Nome,
                Tipo = m.Tipo,
                Quantidade = m.Quantidade,
                Preco = m.Preco,
                CNPJ = m.CNPJ
            }).ToList();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDetalhadoDto>> GetById(int id)
        {
            var material = await _repository.GetByIdWithIncludesAsync(id, m => m.Fornecedor);
            if (material == null)
                return NotFound();

            return Ok(new MaterialDetalhadoDto
            {
                Id = material.Id,
                Nome = material.Nome,
                Tipo = material.Tipo,
                Quantidade = material.Quantidade,
                Preco = material.Preco,
                Fornecedor = new FornecedorListagemDto
                {
                    CNPJ = material.Fornecedor.CNPJ,
                    Nome = material.Fornecedor.Nome,
                    IdEndereco = material.Fornecedor?.IdEndereco ?? 0
                }
            });
        }

        [HttpPost]
        public async Task<ActionResult<MaterialDto>> Post([FromBody] MaterialDto dto)
        {
            var material = new Material
            {
                Nome = dto.Nome,
                Tipo = dto.Tipo,
                Quantidade = dto.Quantidade,
                Preco = dto.Preco,
                CNPJ = dto.CNPJ
            };

            await _repository.AddAsync(material);
            dto.Id = material.Id;

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] MaterialDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var material = await _repository.GetByIdAsync(id);
            if (material == null)
                return NotFound();

            material.Nome = dto.Nome;
            material.Tipo = dto.Tipo;
            material.Quantidade = dto.Quantidade;
            material.Preco = dto.Preco;
            material.CNPJ = dto.CNPJ;

            await _repository.UpdateAsync(material);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var material = await _repository.GetByIdAsync(id);
            if (material == null)
                return NotFound();

            await _repository.DeleteAsync(material);
            return NoContent();
        }
    }
}
