using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstacaoTratamentoApiController : ControllerBase
    {
        private readonly IRepository<EstacaoTratamento> _repository;

        public EstacaoTratamentoApiController(IRepository<EstacaoTratamento> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstacaoListagemDto>>> Get()
        {
            var estacoes = await _repository.GetAllAsync();

            var lista = estacoes.Select(e => new EstacaoListagemDto
            {
                Id = e.Id,
                Status = e.Status,
                DataInstalacao = e.DataInstalacao,
                CPF = e.CPF
            }).ToList();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstacaoDetalhadoDto>> GetById(int id)
        {
            var estacao = await _repository.GetByIdWithIncludesAsync(id,
                e => e.Sensors,
                e => e.Responsavel
            );

            if (estacao == null)
                return NotFound();

            return Ok(new EstacaoDetalhadoDto
            {
                Id = estacao.Id,
                Status = estacao.Status,
                DataInstalacao = estacao.DataInstalacao,
                Responsavel = new ResponsavelListagemDto
                {
                    CPF = estacao.Responsavel?.CPF,
                    Nome = estacao.Responsavel?.Nome
                },
                Sensors = estacao.Sensors.Select(s => new SensorListagemDto
                {
                    Id = s.Id,
                    TpSensor = s.TpSensor,
                    TpMedida = s.TpMedida,
                    IdEstacao = s.IdEstacao
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult<EstacaoListagemDto>> Post([FromBody] EstacaoListagemDto dto)
        {
            var estacao = new EstacaoTratamento
            {
                Status = dto.Status,
                DataInstalacao = dto.DataInstalacao,
                CPF = dto.CPF
            };

            await _repository.AddAsync(estacao);
            dto.Id = estacao.Id;

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EstacaoListagemDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var estacao = await _repository.GetByIdAsync(id);
            if (estacao == null)
                return NotFound();

            estacao.Status = dto.Status;
            estacao.DataInstalacao = dto.DataInstalacao;
            estacao.CPF = dto.CPF;

            await _repository.UpdateAsync(estacao);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var estacao = await _repository.GetByIdAsync(id);
            if (estacao == null)
                return NotFound();

            await _repository.DeleteAsync(estacao);
            return NoContent();
        }
    }
}
