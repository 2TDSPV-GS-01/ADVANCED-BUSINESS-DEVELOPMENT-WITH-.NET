using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResponsavelApiController : ControllerBase
    {
        private readonly IRepository<Responsavel> _repository;

        public ResponsavelApiController(IRepository<Responsavel> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponsavelListagemDto>>> Get()
        {
            var responsaveis = await _repository.GetAllAsync();
            var lista = responsaveis.Select(r => new ResponsavelListagemDto
            {
                CPF = r.CPF,
                Nome = r.Nome
            }).ToList();

            return Ok(lista);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<ResponsavelDetalhadoDto>> GetById(string cpf)
        {
            var responsavel = await _repository.GetByIdWithIncludesAsync(cpf,
                r => r.Contatos,
                r => r.EstacaoTratamentos
            );

            if (responsavel == null)
                return NotFound();

            return Ok(new ResponsavelDetalhadoDto
            {
                CPF = responsavel.CPF,
                Nome = responsavel.Nome,
                Contatos = responsavel.Contatos.Select(c => new ContatoDto
                {
                    Id = c.Id,
                    Email = c.Email,
                    Telefone = c.Telefone,
                }).ToList(),
                EstacaoTratamentos = responsavel.EstacaoTratamentos.Select(e => new EstacaoTratamentoDto
                {
                    Id = e.Id,
                    Status = e.Status,
                    DataInstalacao = e.DataInstalacao
                }).ToList()
            });
        }


        [HttpPost]
        public async Task<ActionResult<ResponsavelListagemDto>> Post([FromBody] ResponsavelListagemDto dto)
        {
            var entity = new Responsavel
            {
                CPF = dto.CPF,
                Nome = dto.Nome
            };

            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { cpf = dto.CPF }, dto);
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> Put(string cpf, [FromBody] ResponsavelListagemDto dto)
        {
            if (cpf != dto.CPF)
                return BadRequest();

            var responsavel = await _repository.GetByIdAsync(cpf);
            if (responsavel == null)
                return NotFound();

            responsavel.Nome = dto.Nome;
            await _repository.UpdateAsync(responsavel);
            return NoContent();
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> Delete(string cpf)
        {
            var responsavel = await _repository.GetByIdAsync(cpf);
            if (responsavel == null)
                return NotFound();

            await _repository.DeleteAsync(responsavel);
            return NoContent();
        }
    }
}
