using AutoMapper;
using ExamenBeClever.Entities_DTO;
using ExamenBeClever.Mapeo;
using ExamenBeClever.Models;
using System.Collections.Generic;

namespace ExamenBeClever.Servicios
{
    public interface IRegistro
    {
        List<RegistroDTO> mostrarTodos();
        List<RegistroDTO> GuardarRegistro(RegistroDTO registroDTO);
    }
    public class RegistroService: IRegistro
    {
        private readonly BeCleverContext _context;
        private readonly IMapper _mapper;
        public List<RegistroDTO> registros;

        public RegistroService(BeCleverContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            registros=new List<RegistroDTO>();
        }
        private void cargarRegistros()
        {
            foreach (Registro registro in _context.Registros)
            {
                var registroAMostrar = _mapper.Map<RegistroDTO>(registro);
                registros.Add(registroAMostrar);
            }
        }
        public List<RegistroDTO> GuardarRegistro(RegistroDTO registroDTO)
        {
            if (registroDTO.RegisterType.ToLower().Trim()=="egreso"||registroDTO.RegisterType.ToLower().Trim()=="ingreso")
            {
                var registro = _mapper.Map<Registro>(registroDTO);
                registro.RegisterType = strings.CapitalizeFirstLetter(registroDTO.RegisterType);
                try
                {
                    _context.Add(registro);
                    _context.SaveChanges();
                    cargarRegistros();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return registros;
        }
        public List<RegistroDTO> mostrarTodos()
        {
            cargarRegistros();
            return registros;
        }
    }

    public interface IEmpleado
    {
        List<Empleado> MostrarTodos();
    }

    public class EmpleadoService : IEmpleado
    {
        private readonly BeCleverContext _context;
        public EmpleadoService(BeCleverContext context)
        {
          _context= context;
        }
        public List<Empleado> MostrarTodos()
        {
            return _context.Empleados.ToList();
        }
    }
}
