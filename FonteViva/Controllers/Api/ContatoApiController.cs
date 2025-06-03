using FonteViva.DTO;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatoApiController : ControllerBase
    {
        private readonly IRepository<Contato> _repository;

        public ContatoApiController(IRepository<Contato> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContatoDto>>> Get()
        {
            var contatos = await _repository.GetAllAsync();

            var listaDto = contatos.Select(c => new ContatoListagemDto
            {
                Id = c.Id,
                Email = c.Email,
                Telefone = c.Telefone,
                CPF = c.CPF,
                CNPJ = c.CNPJ
            }).ToList();

            return Ok(listaDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContatoDetalhadoDto>> GetById(int id)
        {
            var contato = await _repository.GetByIdWithIncludesAsync(id,
                c => c.Responsavel,
                c => c.Fornecedor);
            if (contato == null)
                return NotFound();

            return Ok(new ContatoDetalhadoDto
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

            });
        }

        [HttpPost]
        public async Task<ActionResult<ContatoListagemDto>> Post([FromBody] ContatoListagemDto dto)
        {
            var contato = new Contato
            {
                Email = dto.Email,
                Telefone = dto.Telefone,
                CPF = dto.CPF,
                CNPJ = dto.CNPJ
            };

            await _repository.AddAsync(contato);
            dto.Id = contato.Id;

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ContatoListagemDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var contato = await _repository.GetByIdAsync(id);
            if (contato == null)
                return NotFound();

            contato.Email = dto.Email;
            contato.Telefone = dto.Telefone;
            contato.CPF = dto.CPF;
            contato.CNPJ = dto.CNPJ;

            await _repository.UpdateAsync(contato);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contato = await _repository.GetByIdAsync(id);
            if (contato == null)
                return NotFound();

            await _repository.DeleteAsync(contato);
            return NoContent();
        }
    }
}
