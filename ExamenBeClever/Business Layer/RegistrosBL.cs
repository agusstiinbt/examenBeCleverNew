using ExamenBeClever.Entities_DTO;
using ExamenBeClever.Models;
using ExamenBeClever.Servicios;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace ExamenBeClever
{
    public class RegistrosBL
    {
        private readonly IRegistro _iRegistro;
        private readonly IEmpleado _iempleado;
        private readonly BeCleverContext _beCleverContext;

        //Constructor
        public RegistrosBL(IRegistro IRegistro, IEmpleado Iempleado, BeCleverContext context)
        {
            _iRegistro = IRegistro;
            _iempleado = Iempleado;
            _beCleverContext = context;
        }

        //Métodos privados
        /// <summary>
        /// Filtra una lista existente por nombre y apellido
        /// </summary>
        /// <param name="registrosAFiltrar"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <returns>Una lista filtrada por nombre y/o apellido. En caso de no haber otorgado ningun parámetro de tipo name, se devuelve la lista original</returns>
        private List<RegistroDTO> nameFilter(IEnumerable<RegistroDTO> registrosAFiltrar, string nombre, string apellido)
        {
            var lista = new List<RegistroDTO>();
            if (nombre != null)
            {
                var registrosXNombre = _iempleado.MostrarTodos().Where(x => x.Name.Trim().ToLower() == nombre.Trim().ToLower());
                foreach (Empleado empleado in registrosXNombre)
                {
                    foreach (var registro in registrosAFiltrar)
                    {
                        if (registro.IdEmpleado == empleado.IdEmpleado)
                        {
                            lista.Add(registro);
                        }
                    }
                }
            }
            if (apellido != null)
            {
                var registrosXApellido = _iempleado.MostrarTodos().Where(x => x.Name.Trim().ToLower() == apellido.Trim().ToLower());
                foreach (Empleado empleado in registrosXApellido)
                {
                    foreach (var registro in registrosAFiltrar)
                    {
                        if (registro.IdEmpleado == empleado.IdEmpleado)
                        {
                            lista.Add(registro);
                        }
                    }
                }
            }

            //Si no se envió ningun nombre o apellido devolvemos la lista como estaba
            if (lista.Count > 0)
            {
                return lista;
            }
             return (List<RegistroDTO>)registrosAFiltrar;
        }
        private float MonthDifference(DateTime lValue, DateTime rValue)
        {
            return (lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year);
        }

        //Métodos públicos
        public List<RegistroDTO> MostrarRegistros()
        {
            return _iRegistro.mostrarTodos();
        }
        public List<RegistroDTO> Search(DateTime? fechaDesde, DateTime? fechaHasta, string nombre, string apellido, string tipoRegistro, int IdBusinessLocation)
        {
           var registros = _iRegistro.mostrarTodos().Where
                (
                    x => x.RegisterType.ToLower().Trim() == tipoRegistro.ToLower().Trim()
                    &&
                    x.IdBusinessLocation == IdBusinessLocation
                );
            if (fechaHasta != null) { registros = registros.Where(x => x.Date <= fechaHasta); }
            if (fechaDesde != null) { registros = registros.Where(x => x.Date >= fechaDesde); }
            
            if (nombre!=null||apellido!=null)
            {
                var listaNueva = nameFilter(registros, nombre, apellido);
                return listaNueva;
            }
            List<RegistroDTO> lista = new List<RegistroDTO>();
            foreach (var registroDTO in registros)
            {
                lista.Add(registroDTO);
            }
            return lista;
        }
        public List<RegistroDTO> Register(RegistroDTO registro)
        {
            var lista = _iRegistro.GuardarRegistro(registro);
            return lista;
        }
        public List<PromedioDTO> Promedio(DateTime dateFrom, DateTime dateTo)
        {
            List<PromedioDTO> lista = new List<PromedioDTO>();
            var totalMeses = MonthDifference(dateTo, dateFrom);
           
            var listaLocales = _beCleverContext.BusinessLocations.ToList();

            //Cargamos los hombres y mujeres
            var listaHombres = _iempleado.MostrarTodos().Where(x => x.Genero == 'm');
            var listaMujeres = _iempleado.MostrarTodos().Where(x => x.Genero == 'f');

            //Listas de Ingresos
            var ingresosHombres = new List<Registro>();
            var ingresosMujeres = new List<Registro>();

            //Listas de Egresos
            var egresosHombres = new List<Registro>();
            var egresosMujeres = new List<Registro>();

            foreach (var local in listaLocales)
            {
                //Cargamos los registros que sean de tipo ingreso, entre fechas dadas, y según cada local.
                var Ingresos = _beCleverContext.Registros.Where
                    (
                        x => x.RegisterType.Trim().ToLower() == "ingreso"
                        &&
                        (x.Date >= dateFrom && x.Date <= dateTo)
                        &&
                        x.IdBusinessLocation == local.IdBusinessLocation
                    ).ToList();
                //Total de ingresos de hombres
                foreach (var registro in Ingresos)
                {
                    if (listaHombres.Contains(registro.Empleado))
                    {
                        ingresosHombres.Add(registro);
                    }
                }

                //Total de ingresos de mujeres
                foreach (var registro in Ingresos)
                {
                    if (listaMujeres.Contains(registro.Empleado))
                    {
                        ingresosMujeres.Add(registro);
                    }
                }

                //Ahora calculamos el promedio de los EGRESOS
                var Egresos = _beCleverContext.Registros.Where
                    (
                        x => x.RegisterType.Trim().ToLower() == "egreso"
                        &&
                        (x.Date >= dateFrom && x.Date <= dateTo)
                        &&
                        x.IdBusinessLocation == local.IdBusinessLocation
                    ).ToList();

                foreach (var registro in Egresos)
                {
                    if (listaHombres.Contains(registro.Empleado))
                    {
                        egresosHombres.Add(registro);
                    }
                }
                foreach (var registro in Egresos)
                {
                    if (listaMujeres.Contains(registro.Empleado))
                    {
                        egresosMujeres.Add(registro);
                    }
                }

                PromedioDTO dto = new PromedioDTO();
                dto.Sucursal = local.Country + "," + local.City + "," + local.Address;
                dto.IngresoHombres = ingresosHombres.Count() / totalMeses;
                dto.IngresoMujeres = ingresosMujeres.Count() / totalMeses;
                dto.EgresoHombres = egresosHombres.Count() / totalMeses;
                dto.EgresoMujeres = egresosMujeres.Count() / totalMeses;
                dto.RangoFechas = dateFrom.ToShortDateString() + " - " + dateTo.ToShortDateString();
                lista.Add(dto);
            }
            return lista;
        }
    }
} 