using CapaEntidad.Request;
using FluentValidation;

namespace Api_Sistema_Asistencia.Validaciones
{
    public class UsuarioRequest_Validator:AbstractValidator<EUsuarioRequest>
    {
        public UsuarioRequest_Validator()
        {
            ////---Validacion - Correo --
            RuleFor(x => x.Correo)
                  .NotNull().WithMessage("No Se Permite Valores Nulos.")
                  .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
                  .MaximumLength(100).WithMessage("Longitud Maxima es 100.");
        }

    }
}
