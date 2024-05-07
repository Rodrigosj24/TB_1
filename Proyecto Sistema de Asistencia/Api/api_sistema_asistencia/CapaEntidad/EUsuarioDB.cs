using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class EUsuarioDB
    {
        public int id_usuario { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string nombres_apellidos { get; set; }
        public string celular { get; set; }
        public string correo { get; set; }
        public string contrasenia { get; set; }
        public int id_rol {  get; set; }
        public string imagen { get; set;}
        public Boolean estado { get; set; }
        public String rol { get; set; }
        public String descripcion_estado { get; set; }
    }


}
