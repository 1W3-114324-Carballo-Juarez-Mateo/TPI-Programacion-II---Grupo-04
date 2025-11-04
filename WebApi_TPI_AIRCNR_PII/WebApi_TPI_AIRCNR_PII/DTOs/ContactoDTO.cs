using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.DTOs
{
    public class ContactoDTO
    {
        public int id_contacto { get; set; }

        public required string contacto { get; set; }

        public required virtual TipoContactoDTO id_tipo_contactoNavigation { get; set; }
    }

    public class ModifyContactoDTO
    {
        public int id_contacto { get; set; }

        public int id_tipo_contacto { get; set; }

        public required string contacto { get; set; }
    }
}
