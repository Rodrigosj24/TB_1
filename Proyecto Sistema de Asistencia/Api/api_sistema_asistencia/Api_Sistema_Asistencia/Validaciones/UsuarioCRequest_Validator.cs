using CapaEntidad.Request;
using FluentValidation;

namespace Api_Sistema_Asistencia.Validaciones
{
    public class UsuarioCRequest_Validator:AbstractValidator<EUsuarioCRequest>
    {
        public UsuarioCRequest_Validator() {

            ////---Validacion - Nombres --
            RuleFor(x => x.nombres)
                  .NotNull().WithMessage("No Se Permite Valores Nulos.")
                  .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
                  .MaximumLength(50).WithMessage("Longitud Maxima es 50.");

            ////---Validacion - Apellidos --
            RuleFor(x => x.apellidos)
                  .NotNull().WithMessage("No Se Permite Valores Nulos.")
                  .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
                  .MaximumLength(50).WithMessage("Longitud Maxima es 50.");

            ////---Validacion - Celular --
            RuleFor(x => x.celular)
                  .NotNull().WithMessage("No Se Permite Valores Nulos.")
                  .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
                  .MaximumLength(12).WithMessage("Longitud Maxima es 12.");

            ////---Validacion - Correo --
            RuleFor(x => x.correo)
                  .NotNull().WithMessage("No Se Permite Valores Nulos.")
                  .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
                  .MaximumLength(100).WithMessage("Longitud Maxima es 100.");

            ////---Validacion - Contraseña --
            RuleFor(x => x.contrasenia)
                  .NotNull().WithMessage("No Se Permite Valores Nulos.")
                  .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
                  .MaximumLength(100).WithMessage("Longitud Maxima es 100.");

            ////---Validacion - Id_Rol --
            RuleFor(x => x.id_rol)
               .NotNull().WithMessage("No Se Permite Valores Nulos.")
               .NotEmpty().WithMessage("No Se Permite Valores Vacios.")
               .Must(x => int.TryParse(x.ToString(), out _)).WithMessage("El campo solo permite números.");
        }

    }
}
