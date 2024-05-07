using CapaEntidad.Request;
using FluentValidation;

namespace Api_Sistema_Asistencia.Validaciones
{
    public class InicioAsistenciaRequest_Validator:AbstractValidator<EInicioAsistenciaRequest>
    {
       public InicioAsistenciaRequest_Validator() {

            ////---Validacion - Id_Usuario --
            RuleFor(x => x.Id_Usuario)
               .NotNull().WithMessage("No Se Permite Valores Nulos.")
               .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
               .Must(x => int.TryParse(x.ToString(), out _)).WithMessage("El campo solo permite números.");

            ////---Validacion - Fecha Inicio --
            RuleFor(x => x.Fecha_Inicio)
               .NotNull().WithMessage("No Se Permite Valores Nulos.")
               .NotEmpty().WithMessage("No Se Permite Valores Vacios.");

        }

    }
}
