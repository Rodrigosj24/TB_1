using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EAsistenciaDB
    {
        public int id_asistencia { get; set; }
        public int id_usuario { get; set; }
        public DateTime fecha_inicio { get; set; }
        public DateTime fecha_fin { get; set; }
        public int flag_Asistencia { get; set; }

    }
}
