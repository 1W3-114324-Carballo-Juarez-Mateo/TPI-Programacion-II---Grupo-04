using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.DTOs
{
    public class EmpleadoDTO
    {
        public int id_empleado { get; set; }

        public int legajo { get; set; }

        public required string nombre { get; set; }

        public required string documento { get; set; }

        public required virtual SucursalDTO id_sucursalNavigation { get; set; }

        public required virtual TipoDocDTO id_tipo_documentoNavigation { get; set; }

        public required virtual UsuarioDTO id_usuarioNavigation { get; set; }
    }
}
