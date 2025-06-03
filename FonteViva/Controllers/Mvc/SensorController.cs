using FonteViva.DTO;
using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers
{
    public class SensorController : Controller
    {
        private readonly IRepository<Sensor> _repository;

        public SensorController(IRepository<Sensor> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var sensores = await _repository.GetAllAsync();
            return View(sensores);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sensor = await _repository.GetByIdWithIncludesAsync(id,
                s => s.EstacaoTratamento,
                s => s.RegistroMedidas
            );

            if (sensor == null)
                return NotFound();

            var sensorDetalhado = new SensorDetalhadoDto
            {
                Id = sensor.Id,
                TpSensor = sensor.TpSensor,
                TpMedida = sensor.TpMedida,
                Estacao = new EstacaoListagemDto
                {
                    Id = sensor.EstacaoTratamento.Id,
                    Status = sensor.EstacaoTratamento.Status,
                    DataInstalacao = sensor.EstacaoTratamento.DataInstalacao
                },
                Registros = sensor.RegistroMedidas.Select(r => new RegistroMedidaDto
                {
                    Id = r.Id,
                    Resultado = r.Resultado,
                    DtRegistro = r.DtRegistro
                }).ToList()
            };

            return View(sensorDetalhado);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sensor sensor)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddAsync(sensor);
                return RedirectToAction(nameof(Index));
            }

            return View(sensor);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sensor = await _repository.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            return View(sensor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Sensor sensor)
        {
            if (id != sensor.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _repository.UpdateAsync(sensor);
                return RedirectToAction(nameof(Index));
            }

            return View(sensor);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sensor = await _repository.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            return View(sensor);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sensor = await _repository.GetByIdAsync(id);
            if (sensor == null)
                return NotFound();

            await _repository.DeleteAsync(sensor);
            return RedirectToAction(nameof(Index));
        }
    }
}
