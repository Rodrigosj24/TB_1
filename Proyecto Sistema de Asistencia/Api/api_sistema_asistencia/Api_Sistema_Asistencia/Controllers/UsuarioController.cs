using Api_Sistema_Asistencia.Validaciones;
using AutoMapper;
using CapaEntidad;
using CapaEntidad.Request;
using CapaEntidad.Response;
using CapaNegocio;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api_Sistema_Asistencia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly NUsuario _UsuarioNegocio;

        private bool flag_debug = true;
        private readonly IMapper _mapper;

        public UsuarioController(NUsuario UsuarioNegocio, IMapper mapper)
        {
            _UsuarioNegocio = UsuarioNegocio;
            _mapper = mapper;
        }

        //Buscar los Roles de la BD
        [HttpGet]
        [Route("Obtener_Roles")]
        public async Task<IActionResult> Obtener_Roles()
        {

            try
            {
                var _lst_Roles = await _UsuarioNegocio.Obtener_Roles_Async();

                if (_lst_Roles == null)
                {
                    return NotFound(new ApiResponseError(success: false, statusCode: StatusCodes.Status404NotFound, message: "Roles No Encontradas", errorsMessage: null));
                }
                return Ok(new ApiResponseData(success: true, statusCode: StatusCodes.Status200OK, messages: "Roles Encontradas", data: _lst_Roles));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(success: false, statusCode: StatusCodes.Status500InternalServerError, message: "Ha ocurrido un error inesperado. Comuniquese con un administrador", errorsMessage: flag_debug ? ex.Message : null));
            }

        }

        //Logueo del usuario en la BD
        [HttpPost]
        [Route("Obtener_Login_Usuario")]
        public async Task<IActionResult> Obtener_Login_Usuario([FromBody] ELoginRequest p_LoginRequest)
        {

            try
            {
                if (p_LoginRequest == null)
                {
                    return BadRequest(new ApiResponseError(success: false, statusCode: StatusCodes.Status400BadRequest, message: "La entidad se encuentra nula y/o distinta", errorsMessage: null));

                }

                var p_validator = new LoginRequest_Validator();

                ValidationResult resultad = await p_validator.ValidateAsync(p_LoginRequest);

                if (!resultad.IsValid)
                {
                    var errors = resultad.Errors.Select(err => new { err.PropertyName, err.ErrorMessage }).ToArray();

                    return BadRequest(new { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors });
                }

                var _EUsuario = await _UsuarioNegocio.Obtener_Login_Usuario_Async(p_LoginRequest.Correo, p_LoginRequest.Contrasenia);

                if (_EUsuario == null)
                {
                    return NotFound(new ApiResponseError(success: false, statusCode: StatusCodes.Status404NotFound, message: "Usuario No Encontrado o No tiene permiso", errorsMessage: null));
                }
                return Ok(new ApiResponseData(success: true, statusCode: StatusCodes.Status200OK, messages: "Usuario Encontrado", data: _EUsuario));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(success: false, statusCode: StatusCodes.Status500InternalServerError, message: "Ha ocurrido un error inesperado. Comuniquese con un administrador", errorsMessage: flag_debug ? ex.Message : ""));
            }

        }

        //Buscar usuario en la BD
        [HttpGet]
        [Route("Obtener_Usuario")]
        public async Task<IActionResult> Obtener_Usuario([FromQuery] EUsuarioRequest p_UsuarioRequest)
        {

            try
            {
                if (p_UsuarioRequest == null)
                {
                    return BadRequest(new ApiResponseError(success: false, statusCode: StatusCodes.Status400BadRequest, message: "La entidad se encuentra nula y/o distinta", errorsMessage: null));

                }

                var p_validator = new UsuarioRequest_Validator();

                ValidationResult resultad = await p_validator.ValidateAsync(p_UsuarioRequest);

                if (!resultad.IsValid)
                {
                    var errors = resultad.Errors.Select(err => new { err.PropertyName, err.ErrorMessage }).ToArray();

                    return BadRequest(new { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors });
                }

                var _EUsuario = await _UsuarioNegocio.Obtener_Usuarios_Async(p_UsuarioRequest.Correo);

                if (_EUsuario == null)
                {
                    return NotFound(new ApiResponseError(success: false, statusCode: StatusCodes.Status404NotFound, message: "Usuario No Encontrado o No tiene permiso", errorsMessage: null));
                }
                return Ok(new ApiResponseData(success: true, statusCode: StatusCodes.Status200OK, messages: "Usuario Encontrado", data: _EUsuario));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(success: false, statusCode: StatusCodes.Status500InternalServerError, message: "Ha ocurrido un error inesperado. Comuniquese con un administrador", errorsMessage: flag_debug ? ex.Message : ""));
            }

        }

        //Buscar usuario en la BD
        [HttpGet]
        [Route("Listar_Datos_Usuarios")]
        public async Task<IActionResult> Listar_Datos_Usuarios()
        {
            try
            {
                var _lst_Usuarios = await _UsuarioNegocio.Listar_Datos_Usuarios_Async();

                if (_lst_Usuarios == null)
                {
                    return NotFound(new ApiResponseError(success: false, statusCode: StatusCodes.Status404NotFound, message: "Usuarios No Encontradas", errorsMessage: null));
                }

                return Ok(new ApiResponseData(success: true, statusCode: StatusCodes.Status200OK, messages: "Usuarios Encontradas", data: _lst_Usuarios));


            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(success: false, statusCode: StatusCodes.Status500InternalServerError, message: "Ha ocurrido un error inesperado. Comuniquese con un administrador", errorsMessage: flag_debug ? ex.Message : null));
            }

        }

        //Desactivar los usuarios de la BD
        [HttpGet]
        [Route("Eliminar_Usuario")]
        public async Task<IActionResult> Eliminar_Usuario([FromQuery] EUsuarioRequest p_UsuarioRequest)
        {

            try
            {
                if (p_UsuarioRequest == null)
                {
                    return BadRequest(new ApiResponseError(success: false, statusCode: StatusCodes.Status400BadRequest, message: "La entidad se encuentra nula y/o distinta", errorsMessage: null));

                }

                var p_validator = new UsuarioRequest_Validator();

                ValidationResult resultad = await p_validator.ValidateAsync(p_UsuarioRequest);

                if (!resultad.IsValid)
                {
                    var errors = resultad.Errors.Select(err => new { err.PropertyName, err.ErrorMessage }).ToArray();

                    return BadRequest(new { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors });
                }

                // Lógica para validar si el usuario ya existe en la base de datos
                var _ExisteUsuario = await _UsuarioNegocio.Obtener_Usuarios_Async(p_UsuarioRequest.Correo);


                if (_ExisteUsuario == null)
                {
                    return Conflict(new ApiResponseError(success: false, statusCode: StatusCodes.Status409Conflict, message: "Usuario No Existe Registrado en la Base o Ya Se Encuentra Deshabilitado", errorsMessage: ""));
                }

                var _Codigo_Usuario = await _UsuarioNegocio.Eliminar_Usuarios_Async(p_UsuarioRequest.Correo);

                if (_Codigo_Usuario == 0)
                {
                    return NotFound(new ApiResponseError(success: false, statusCode: StatusCodes.Status404NotFound, message: "Usuario No Se Elimino", errorsMessage: ""));
                }
                return Ok(new ApiResponseData(success: true, statusCode: StatusCodes.Status200OK, messages: "Usuario Eliminado", data: null));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(success: false, statusCode: StatusCodes.Status500InternalServerError, message: "Ha ocurrido un error inesperado. Comuniquese con un administrador", errorsMessage: flag_debug ? ex.Message : ""));
            }

        }

        //Registrar nuevos usuarios en la BD
        [HttpPost]
        [Route("Registrar_Usuario")]
        public async Task<IActionResult> Registrar_Usuario([FromBody] EUsuarioCRequest p_UsuarioCRequest)
        {

            try
            {
                if (p_UsuarioCRequest == null)
                {
                    return BadRequest(new ApiResponseError(success: false, statusCode: StatusCodes.Status400BadRequest, message: "La entidad se encuentra nula y/o distinta", errorsMessage: null));

                }

                var p_validator = new UsuarioCRequest_Validator();

                ValidationResult resultad = await p_validator.ValidateAsync(p_UsuarioCRequest);

                if (!resultad.IsValid)
                {
                    var errors = resultad.Errors.Select(err => new { err.PropertyName, err.ErrorMessage }).ToArray();

                    return BadRequest(new { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors });
                }

                // Lógica para validar si el usuario ya existe en la base de datos
                var _ExisteUsuario = await _UsuarioNegocio.Obtener_Usuarios_General_Async(p_UsuarioCRequest.correo);


                if (_ExisteUsuario != null)
                {
                    return Conflict(new ApiResponseError(success: false, statusCode: StatusCodes.Status409Conflict, message: "Usuario Ya Existe Registrado en la Base, Validar que no esté Deshabilitado", errorsMessage: ""));
                }

                //Paseamos la clase DB Con el Request (DTO)
                var pUsuario = new EUsuarioCRequest()
                {
                    nombres = p_UsuarioCRequest.nombres,
                    apellidos = p_UsuarioCRequest.apellidos,
                    celular = p_UsuarioCRequest.celular,
                    correo = p_UsuarioCRequest.correo,
                    contrasenia = p_UsuarioCRequest.contrasenia,
                    id_rol = p_UsuarioCRequest.id_rol,
                    imagen = p_UsuarioCRequest.imagen,
                    
                };
                var _Codigo_Usuario = await _UsuarioNegocio.Registrar_Usuarios_Async(pUsuario.nombres,pUsuario.apellidos,pUsuario.celular,pUsuario.correo,pUsuario.contrasenia,pUsuario.id_rol,pUsuario.imagen);

                if (_Codigo_Usuario == 0)
                {
                    return NotFound(new ApiResponseError(success: false, statusCode: StatusCodes.Status404NotFound, message: "Usuario No Se Registró", errorsMessage: ""));
                }
                return Ok(new ApiResponseData(success: true, statusCode: StatusCodes.Status200OK, messages: "Usuario Registrado Correctamente", data: null));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(success: false, statusCode: StatusCodes.Status500InternalServerError, message: "Ha ocurrido un error inesperado. Comuniquese con un administrador", errorsMessage: flag_debug ? ex.Message : ""));
            }

        }

    }
}
