using System.Diagnostics.Contracts;
using AutoMapper;
using System.Numerics;
using ExamenBeClever.Models;
using ExamenBeClever.Entities_DTO;

namespace ExamenBeClever.Mapeo
{
    public class EntityMapper:Profile
    {
        public EntityMapper()
        {
            CreateMap<Registro,RegistroDTO>().ReverseMap();
        }
    }
}
