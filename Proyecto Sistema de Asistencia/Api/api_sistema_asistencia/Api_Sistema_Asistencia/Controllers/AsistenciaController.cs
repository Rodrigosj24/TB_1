using Api_Sistema_Asistencia.Validaciones;
using AutoMapper;
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
    public class AsistenciaController : ControllerBase
    {
        private readonly NAsistencia _AsistenciaNegocio;

        private bool flag_debug = true;
        private readonly IMapper _mapper;

        public AsistenciaController(NAsistencia AsistenciaNegocio, IMapper mapper)
        {
            _AsistenciaNegocio = AsistenciaNegocio;
            _mapper = mapper;
        }

        //Registrar el inicio de la asistencia en la BD
        [HttpPost]
        [Route("Registrar_Inicio_Asistencia")]
        public async Task<IActionResult> Registrar_Inicio_Asistencia([FromBody] EInicioAsistenciaRequest p_InicioAistenciaRequest)
        {

            try
            {
                if (p_InicioAistenciaRequest == null)
                {
                    return BadRequest(new ApiResponseError(success: false, statusCode: StatusCodes.Status400BadRequest, message: "La entidad se encuentra nula y/o distinta", errorsMessage: null));

                }

                var p_validator = new InicioAsistenciaRequest_Validator();

                ValidationResult resultad = await p_validator.ValidateAsync(p_InicioAistenciaRequest);

                if (!resultad.IsValid)
                {
                    var errors = resultad.Errors.Select(err => new { err.PropertyName, err.ErrorMessage }).ToArray();

                    return BadRequest(new { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors });
                }

                //Paseamos la clase DB Con el Request (DTO)
                var pAsistencia = new EInicioAsistenciaRequest()
                {
                    Id_Usuario = p_InicioAistenciaRequest.Id_Usuario,
                    Fecha_Inicio = p_InicioAistenciaRequest.Fecha_Inicio,

                };
                var _Codigo_Asistencia = await _AsistenciaNegocio.Registrar_Inicio_Asistencia_Async(pAsistencia.Id_Usuario, pAsistencia.Fecha_Inicio);

                if (_Codigo_Asistencia == 0)
                {
                    return NotFound(new ApiResponseError(success: false, statusCode: StatusCodes.Status404NotFound, message: "El inicio de la Asistencia No Se Registro", errorsMessage: ""));
                }
                return Ok(new ApiResponseData(success: true, statusCode: StatusCodes.Status200OK, messages: "Inicio de Asistencia Registrado Correctamente", data: null));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(success: false, statusCode: StatusCodes.Status500InternalServerError, message: "Ha ocurrido un error inesperado. Comuniquese con un administrador", errorsMessage: flag_debug ? ex.Message : ""));
            }

        }

        //Registrar el Fin de la asistencia en la BD
        [HttpPost]
        [Route("Registrar_Fin_Asistencia")]
        public async Task<IActionResult> Registrar_Fin_Asistencia([FromBody] EFinAsistenciaRequest p_FinAistenciaRequest)
        {

            try
            {
                if (p_FinAistenciaRequest == null)
                {
                    return BadRequest(new ApiResponseError(success: false, statusCode: StatusCodes.Status400BadRequest, message: "La entidad se encuentra nula y/o distinta", errorsMessage: null));

                }

                var p_validator = new FinAsistenciaRequest_Validator();

                ValidationResult resultad = await p_validator.ValidateAsync(p_FinAistenciaRequest);

                if (!resultad.IsValid)
                {
                    var errors = resultad.Errors.Select(err => new { err.PropertyName, err.ErrorMessage }).ToArray();

                    return BadRequest(new { Success = false, StatusCode = StatusCodes.Status400BadRequest, Errors = errors });
                }

                //Paseamos la clase DB Con el Request (DTO)
                var pAsistencia = new EFinAsistenciaRequest()
                {
                    Id_Usuario = p_FinAistenciaRequest.Id_Usuario,
                    Fecha_Fin = p_FinAistenciaRequest.Fecha_Fin,

                };
                var _Codigo_Asistencia = await _AsistenciaNegocio.Registrar_Fin_Asistencia_Async(pAsistencia.Id_Usuario, pAsistencia.Fecha_Fin);

                if (_Codigo_Asistencia == 0)
                {
                    return NotFound(new ApiResponseError(success: false, statusCode: StatusCodes.Status404NotFound, message: "El Final de la Asistencia No Se Registro", errorsMessage: ""));
                }
                return Ok(new ApiResponseData(success: true, statusCode: StatusCodes.Status200OK, messages: "Fin de Asistencia Registrado Correctamente", data: null));

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponseError(success: false, statusCode: StatusCodes.Status500InternalServerError, message: "Ha ocurrido un error inesperado. Comuniquese con un administrador", errorsMessage: flag_debug ? ex.Message : ""));
            }

        }


    }
}
