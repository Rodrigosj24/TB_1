using CapaEntidad.Request;
using FluentValidation;

namespace Api_Sistema_Asistencia.Validaciones
{
    public class FinAsistenciaRequest_Validator:AbstractValidator<EFinAsistenciaRequest>
    {
        public FinAsistenciaRequest_Validator() {

            ////---Validacion - Id_Rol --
            RuleFor(x => x.Id_Usuario)
               .NotNull().WithMessage("No Se Permite Valores Nulos.")
               .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
               .Must(x => int.TryParse(x.ToString(), out _)).WithMessage("El campo solo permite números.");

            ////---Validacion - Fecha Fin --
            RuleFor(x => x.Fecha_Fin)
               .NotNull().WithMessage("No Se Permite Valores Nulos.")
               .NotEmpty().WithMessage("No Se Permite Valores Vacios.");

        }
    }
}
