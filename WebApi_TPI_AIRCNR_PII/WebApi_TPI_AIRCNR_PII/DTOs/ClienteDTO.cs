using System.Diagnostics.CodeAnalysis;
using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.DTOs
{
    public class ClienteDTO
    {
        public int id_cliente { get; set; }

        public required string nombre { get; set; }

        public required string documento { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();

        public required virtual TipoDocDTO id_tipo_documentoNavigation { get; set; }
    }

    public class ModifyClienteDTO
    {
        public int id_cliente { get; set; }

        public required string nombre { get; set; }

        public required string documento { get; set; }

        public int id_tipo_documento { get; set; }

        public virtual ICollection<Contacto> Contactos { get; set; } = new List<Contacto>();
    }
}
