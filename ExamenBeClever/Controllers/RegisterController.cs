using ExamenBeClever.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ExamenBeClever.Entities_DTO;
using ExamenBeClever.Servicios;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;

namespace ExamenBeClever.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly RegistrosBL _registroBL;

        public RegisterController(IRegistro IRegistro, IEmpleado Iempleado, BeCleverContext context)
        {
            _registroBL=new RegistrosBL(IRegistro, Iempleado,context);
        }

        //POSTs
        [HttpPost]
        public IActionResult guardarRegistro([FromBody] RegistroDTO registro)
        {
            var lista = _registroBL.Register(registro);
            return Ok(lista);
        }

        //GETs
        [HttpGet("MostrarRegistros")]
        public IActionResult MostrarRegistros()
        {
            return Ok(_registroBL.MostrarRegistros());
        }
        [HttpGet("Search")]
        public IActionResult Search(int IdBusinessLocation, string? tipoRegistro, DateTime? fechaDesde, DateTime? fechaHasta = null, string? nombre = null, string? apellido = null)
        {
            var registros = _registroBL.Search(fechaDesde,fechaHasta,nombre,apellido,tipoRegistro, IdBusinessLocation);
            return Ok(registros);
        }
        [HttpGet("Average")]
        public IActionResult average(DateTime dateFrom, DateTime dateTo)
        {
            var lista=_registroBL.Promedio(dateFrom,dateTo);
            return Ok(lista);
        }
    }
}
