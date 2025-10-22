using WebApi_TPI_AIRCNR_PII.Models;

namespace WebApi_TPI_AIRCNR_PII.DTOs
{
    public class VehiculoDTO
    {
        public int id_vehiculo { get; set; }

        public required string patente { get; set; }

        public required string modelo { get; set; }

        public decimal valor_tasado { get; set; }

        public int id_sucursal { get; set; }

        public required virtual Estados_Vehiculo id_estado_vehiculoNavigation { get; set; }

        public required virtual Marca id_marcaNavigation { get; set; }

        public required virtual Sucursal id_sucursalNavigation { get; set; }

        public required virtual Tipos_Vehiculo id_tipo_vehiculoNavigation { get; set; }
    }

    public class ModifyVehiculoDTO()
    {
        public int id_vehiculo { get; set; }

        public required string patente { get; set; }

        public int id_tipo_vehiculo { get; set; }

        public int id_marca { get; set; }

        public required string modelo { get; set; }

        public decimal valor_tasado { get; set; }

        public int id_estado_vehiculo { get; set; }

        public int id_sucursal { get; set; }
    }
}
