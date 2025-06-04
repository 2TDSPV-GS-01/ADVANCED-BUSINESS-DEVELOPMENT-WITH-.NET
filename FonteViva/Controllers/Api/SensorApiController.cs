using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class SensorApiController : ControllerBase
    {
        private readonly IRepository<Sensor> _repository;

        public SensorApiController(IRepository<Sensor> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorListagemDto>>> Get()
        {
            var sensores = await _repository.GetAllAsync();

            var lista = sensores.Select(s => new SensorListagemDto
            {
                Id = s.Id,
                TpSensor = s.TpSensor,
                TpMedida = s.TpMedida,
                IdEstacao = s.IdEstacao
            }).ToList();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SensorDetalhadoDto>> GetById(int id)
        {
            var sensor = await _repository.GetByIdWithIncludesAsync(id,
                s => s.EstacaoTratamento,
                s => s.RegistroMedidas
            );

            if (sensor == null)
                return NotFound();

            return Ok(new SensorDetalhadoDto
            {
                Id = sensor.Id,
                TpSensor = sensor.TpSensor,
                TpMedida = sensor.TpMedida,
                Estacao = new EstacaoListagemDto
                {
                    Id = sensor.EstacaoTratamento.Id,
                    Status = sensor.EstacaoTratamento.Status,
                    DataInstalacao = sensor.EstacaoTratamento.DataInstalacao,
                    CPF = sensor.EstacaoTratamento.CPF,
                },
                Registros = sensor.RegistroMedidas.Select(r => new RegistroMedidaDto
                {
                    Id = r.Id,
                    Resultado = r.Resultado,
                    DtRegistro = r.DtRegistro
                }).ToList()
            });
        }

        [HttpPost]
        public async Task<ActionResult<SensorListagemDto>> Post([FromBody] SensorListagemDto dto)
        {
            var sensor = new Sensor
            {
                TpSensor = dto.TpSensor,
                TpMedida = dto.TpMedida,
                IdEstacao = dto.IdEstacao
            };

            await _repository.AddAsync(sensor);
            dto.Id = sensor.Id;

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SensorListagemDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var sensor = await _repository.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            sensor.TpSensor = dto.TpSensor;
            sensor.TpMedida = dto.TpMedida;
            sensor.IdEstacao = dto.IdEstacao;

            await _repository.UpdateAsync(sensor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sensor = await _repository.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            await _repository.DeleteAsync(sensor);
            return NoContent();
        }
    }
}
