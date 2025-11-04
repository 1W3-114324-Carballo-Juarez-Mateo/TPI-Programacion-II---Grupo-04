using System.Security.Cryptography;
using System.Text;
using WebApi_TPI_AIRCNR_PII.DTOs;
using WebApi_TPI_AIRCNR_PII.Helper;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;
using WebApi_TPI_AIRCNR_PII.Services.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Services.Implementations
{
    public class EmpleadoService : IEmpleadoService
    {

        private readonly IEmpleadoRepository _repo;
        private readonly IUsuarioRepository _usuarioRepo;

        public EmpleadoService(IEmpleadoRepository repo, IUsuarioRepository usuarioRepo)
        {
            _repo = repo;
            _usuarioRepo = usuarioRepo;
        }

        public EmpleadoDTO MapToDto(Empleado e)
        {
            return new EmpleadoDTO
            {
                nombre = e.nombre,
                legajo = e.legajo,
                documento = e.documento,
                id_empleado = e.id_empleado,
                id_sucursalNavigation = new SucursalDTO
                {
                    id_sucursal = e.id_sucursalNavigation.id_sucursal,
                    descripcion = e.id_sucursalNavigation.descripcion
                },
                id_tipo_documentoNavigation = new TipoDocDTO
                {
                    id_tipo_documento = e.id_tipo_documentoNavigation.id_tipo_documento,
                    tipo = e.id_tipo_documentoNavigation.tipo
                },
                id_usuarioNavigation = new UsuarioDTO
                {
                    id_usuario = e.id_usuarioNavigation.id_usuario,
                    usuario = e.id_usuarioNavigation.usuario,
                    contraseña = "."
                }
            };
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }

        public Empleado MapToBD(EmpleadoDTO e)
        {
            return new Empleado
            {
                nombre = e.nombre,
                legajo = e.legajo,
                documento = e.documento,
                id_empleado = e.id_empleado,
                id_sucursalNavigation = new Sucursal
                {
                    id_sucursal = e.id_sucursalNavigation.id_sucursal,
                    descripcion = e.id_sucursalNavigation.descripcion
                },
                id_tipo_documentoNavigation = new Tipos_Documento
                {
                    id_tipo_documento = e.id_tipo_documentoNavigation.id_tipo_documento,
                    tipo = e.id_tipo_documentoNavigation.tipo
                },
                id_usuarioNavigation = new Usuarios_Empleado
                {
                    id_usuario = e.id_usuarioNavigation.id_usuario,
                    usuario = e.id_usuarioNavigation.usuario,
                    contraseña = HashPassword(e.id_usuarioNavigation.contraseña)
                }
            };
        }

        public async Task<EmpleadoDTO?> GetByUser(int idUser)
        {
            try
            {
                Empleado? e = await _repo.GetByUser(idUser);
                if (e != null)
                {
                    return MapToDto(e);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<string> Validaciones(EmpleadoDTO e)
        {
            if (GetByLegajo(e.legajo) != null)
            {
                return "Ya existe un empleado con ese Legajo";
            }
            if (await _usuarioRepo.GetByUsername(e.id_usuarioNavigation.usuario) != null)
            {
                return "Nombre de usuario ya existente";
            }
            return string.Empty;
        }

        public async Task<ResponseApi> Post(EmpleadoDTO e)
        {
            try
            {
                string validaciones = await Validaciones(e);
                if (string.IsNullOrEmpty(validaciones))
                {
                    if (await _repo.Post(MapToBD(e)))
                    {
                        return new ResponseApi(200, "Registro realizado con exito", e);
                    }
                    else { return new ResponseApi(400, "No se pudo realizar el registro"); }
                }
                else { return new ResponseApi(400, validaciones); }
            }
            catch (Exception)
            {
                return new ResponseApi(500, "Error interno del servidor");
            }
        }

        public async Task<EmpleadoDTO?> GetByLegajo(int legajo)
        {
            Empleado? e = await _repo.GetByLegajo(legajo);
            return e != null ? MapToDto(e) : null;
        }
    }
}
