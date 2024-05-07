using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad.Request
{
    public class EUsuarioCRequest
    {
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string contrasenia { get; set; }
        public int id_rol { get; set; }
        public string imagen { get; set; }

    }
}
