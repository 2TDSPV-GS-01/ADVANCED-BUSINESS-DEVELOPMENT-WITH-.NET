using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoApiController : ControllerBase
    {
        private readonly IRepository<Endereco> _repository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnderecoDto>>> Get()
        {
            var enderecos = await _repository.GetAllAsync();

            var listaDto = enderecos.Select(e => new EnderecoDto
            {
                Id = e.Id,
                Pais = e.Pais,
                Estado = e.Estado,
                Cidade = e.Cidade,
                Rua = e.Rua,
                CEP = e.CEP
            });

            return Ok(listaDto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnderecoDto>> GetById(int id)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null) return NotFound();

            return Ok(new EnderecoDto
            {
                Id = endereco.Id,
                Pais = endereco.Pais,
                Estado = endereco.Estado,
                Cidade = endereco.Cidade,
                Rua = endereco.Rua,
                CEP = endereco.CEP
            });
        }

        [HttpPost]
        public async Task<ActionResult<EnderecoDto>> Post([FromBody] EnderecoDto dto)
        {
            var endereco = new Endereco
            {
                Pais = dto.Pais,
                Estado = dto.Estado,
                Cidade = dto.Cidade,
                Rua = dto.Rua,
                CEP = dto.CEP
            };

            await _repository.AddAsync(endereco);

            // Retorna com o novo ID gerado
            dto.Id = endereco.Id;

            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EnderecoDto dto)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null)
                return NotFound();

            endereco.Pais = dto.Pais;
            endereco.Estado = dto.Estado;
            endereco.Cidade = dto.Cidade;
            endereco.Rua = dto.Rua;
            endereco.CEP = dto.CEP;

            await _repository.UpdateAsync(endereco);

            return NoContent(); // ou Ok(dto) se quiser retornar
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var endereco = await _repository.GetByIdAsync(id);
            if (endereco == null)
                return NotFound();

            await _repository.DeleteAsync(endereco);
            return NoContent();
        }

    }
}
