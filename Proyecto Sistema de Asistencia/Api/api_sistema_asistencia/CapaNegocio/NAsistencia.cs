using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class NAsistencia
    {
        private readonly DAsistencia _AsistenciaData;

        public NAsistencia(DAsistencia AsistenciaData)
        {
            _AsistenciaData = AsistenciaData;
        }

        //Registrar el inicio de la asistencia en la BD
        public async Task<int> Registrar_Inicio_Asistencia_Async(int pId_Usuario, DateTime p_Fecha_Inicio)
        {
            try
            {
                return await _AsistenciaData.Registrar_Inicio_Asistencia_Async(pId_Usuario, p_Fecha_Inicio);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al Registrar el inicio de asistencia en la BD", ex);
            }

        }

        //Registrar el fin de la asistencia en la BD
        public async Task<int> Registrar_Fin_Asistencia_Async(int pId_Usuario, DateTime p_Fecha_Fin)
        {
            try
            {
                return await _AsistenciaData.Registrar_Fin_Asistencia_Async(pId_Usuario, p_Fecha_Fin);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al Registrar el Fin de asistencia en la BD", ex);
            }

        }


    }
}
