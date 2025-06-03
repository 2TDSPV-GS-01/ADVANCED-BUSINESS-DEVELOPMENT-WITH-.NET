using FonteViva.Models;
using FonteViva.Repository;
using Microsoft.AspNetCore.Mvc;

namespace FonteViva.Controllers
{
    public class RegistroMedidaController : Controller
    {
        private readonly IRepository<RegistroMedida> _repository;

        public RegistroMedidaController(IRepository<RegistroMedida> repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var registros = await _repository.GetAllAsync();
            return View(registros);
        }

        public async Task<IActionResult> Details(string id)
        {
            var registro = await _repository.GetByIdAsync(id);
            if (registro == null)
                return NotFound();

            return View(registro);
        }
    }
}
