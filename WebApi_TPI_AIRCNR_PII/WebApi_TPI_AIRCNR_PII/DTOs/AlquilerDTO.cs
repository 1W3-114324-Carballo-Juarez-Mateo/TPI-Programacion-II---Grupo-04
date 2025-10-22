using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.DTOs
{
    public class AlquilerDTO
    {
        public int id_alquiler { get; set; }

        public decimal monto { get; set; }

        public DateTime fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        public required virtual Cliente id_clienteNavigation { get; set; }

        public required virtual Estados_Alquiler id_estado_alquilerNavigation { get; set; }

        public required virtual Sucursal id_sucursalNavigation { get; set; }

        public required virtual Vehiculo id_vehiculoNavigation { get; set; }
    }

    public class ModifyAlquilerDTO()
    {
        public int id_alquiler { get; set; }

        public required virtual Cliente id_clienteNavigation { get; set; }

        public int id_estado_alquiler { get; set; }

        public decimal monto { get; set; }

        public DateTime fecha_inicio { get; set; }

        public DateTime? fecha_fin { get; set; }

        public int id_vehiculo { get; set; }

        public int id_sucursal { get; set; }
    }
}
