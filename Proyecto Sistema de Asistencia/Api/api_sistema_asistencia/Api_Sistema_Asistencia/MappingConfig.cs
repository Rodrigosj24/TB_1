using AutoMapper;
using CapaEntidad;
using CapaEntidad.Request;

namespace Api_Sistema_Asistencia
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // se crea los formenber para darle la logica al dto
            //CreateMap<EAgenteRequest, EAgenteDB>();

            //// se crea los formenber para darle la logica al dto
            //CreateMap<EClienteInbCRequest, EClienteInbDB>();

            ////se crea los formenber para darle la logica al dto
            //CreateMap<EGestionRequest, EGestionDB>().ForMember(dest => dest.fecha_gestion_inicio, opt => opt.MapFrom(src => src.hora_servidor_crm));

            //// se crea los formenber para darle la logica al dto
            //CreateMap<EGestionDetalleRequest, EGestion_DetalleDB>();

            // se crea los formenber para darle la logica al dto
            //CreateMap<EHistoriaGestionAllBD, EGestion_HistoricoDB>();

        }
    }
}
