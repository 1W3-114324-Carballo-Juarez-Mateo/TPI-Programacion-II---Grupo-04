namespace WebApi_TPI_AIRCNR_PII.DTOs
{
    public class UsuarioDTO
    {
        public int id_usuario { get; set; }

        public required string usuario { get; set; }

        public required string contraseña { get; set; }
    }
}
