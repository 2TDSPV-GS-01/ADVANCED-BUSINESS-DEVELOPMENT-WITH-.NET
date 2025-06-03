using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistroMedidaApiController : ControllerBase
    {
        private readonly IRepository<RegistroMedida> _repository;

        public RegistroMedidaApiController(IRepository<RegistroMedida> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistroMedidaDto>>> Get()
        {
            var registros = await _repository.GetAllAsync();

            var lista = registros.Select(r => new RegistroMedidaDto
            {
                Id = r.Id,
                DtRegistro = r.DtRegistro,
                Resultado = r.Resultado,
                IdSensor = r.IdSensor
            }).ToList();

            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistroMedidaDto>> GetById(string id)
        {
            var registro = await _repository.GetByIdAsync(id);
            if (registro == null)
                return NotFound();

            return Ok(new RegistroMedidaDto
            {
                Id = registro.Id,
                DtRegistro = registro.DtRegistro,
                Resultado = registro.Resultado,
                IdSensor = registro.IdSensor
            });
        }
    }
}
