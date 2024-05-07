using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class NUsuario
    {
        private readonly DUsuario _UsuarioData;

        public NUsuario(DUsuario UsuarioData)
        {
            _UsuarioData = UsuarioData;
        }

        //Obtener la lista de roles
        public async Task<List<ERolesDB>> Obtener_Roles_Async()
        {
            try
            {
                return await _UsuarioData.Obtener_Roles_Async();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al buscar los roles en la BD", ex);
            }
        }

        //Obtener Login de los usuario
        public async Task<EUsuarioDB> Obtener_Login_Usuario_Async(string pCorreo, string pContrasenia)
        {
            try
            {
                return await _UsuarioData.Obtener_Login_Usuario_Async(pCorreo, pContrasenia);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al Obtener el Login de los Usuarios", ex);
            }

        }

        //Obtener los usuarios acivos de la BD
        public async Task<EUsuarioDB> Obtener_Usuarios_Async(string pCorreo)
        {
            try
            {
                return await _UsuarioData.Obtener_Usuarios_Async(pCorreo);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al Obtener los Usuarios de la BD", ex);
            }

        }
        //Obtener los usuarios acivos e inactivos de la BD
        public async Task<EUsuarioDB> Obtener_Usuarios_General_Async(string pCorreo)
        {
            try
            {
                return await _UsuarioData.Obtener_Usuarios_General_Async(pCorreo);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al Obtener los Usuarios de la BD", ex);
            }

        }

        //Obtener la lista de Usuarios
        public async Task<List<EUsuarioDB>> Listar_Datos_Usuarios_Async()
        {
            try
            {
                return await _UsuarioData.Listar_Datos_Usuarios_Async();
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al Listar los Usuarios en la BD", ex);
            }
        }

        //Eliminar los usuarios acivos de la BD
        public async Task<int> Eliminar_Usuarios_Async(string pCorreo)
        {
            try
            {
                return await _UsuarioData.Eliminar_Usuarios_Async(pCorreo);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al Eliminar los Usuarios de la BD", ex);
            }

        }

        //Registrar los nuevos usuarios en la BD
        public async Task<int> Registrar_Usuarios_Async(string p_nombres, string p_apellidos, string p_celular, string p_correo, string p_contrasenia, int p_id_rol, string p_imagen)
        {
            try
            {
                return await _UsuarioData.Registrar_Usuarios_Async(p_nombres, p_apellidos, p_celular, p_correo, p_contrasenia, p_id_rol, p_imagen);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al Registrar los Usuarios de la BD", ex);
            }

        }

    }

}
