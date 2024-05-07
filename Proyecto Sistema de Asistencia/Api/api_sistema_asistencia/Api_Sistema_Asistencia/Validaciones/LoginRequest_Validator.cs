using CapaEntidad.Request;
using FluentValidation;

namespace Api_Sistema_Asistencia.Validaciones
{
    public class LoginRequest_Validator: AbstractValidator<ELoginRequest>
    {
        public LoginRequest_Validator() 
        {
            ////---Validacion - nombres --
            RuleFor(x => x.Correo)
              .NotNull().WithMessage("No Se Permite Valores Nulos.")
              .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
              .MaximumLength(100).WithMessage("Longitud Maxima es {GetMaxLength(x.Correo)}.");

            ////---Validacion - nombres --
            RuleFor(x => x.Contrasenia)
              .NotNull().WithMessage("No Se Permite Valores Nulos.")
              .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
              .MaximumLength(100).WithMessage("Longitud Maxima es {GetMaxLength(x.Contrasenia)}.");
        }

    }

}
