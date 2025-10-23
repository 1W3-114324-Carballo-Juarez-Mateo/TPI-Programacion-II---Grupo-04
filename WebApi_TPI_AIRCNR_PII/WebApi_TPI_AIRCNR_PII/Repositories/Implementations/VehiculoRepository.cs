using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class VehiculoRepository : IVehiculoRepository
    {

        private readonly AlquileresContext _context;
        private readonly DbSet<Vehiculo> _vehiculos;

        public VehiculoRepository(AlquileresContext context)
        {
            _context = context;
            _vehiculos = _context.Set<Vehiculo>();
        }

        public async Task<bool> CambioEstado(int id, string nvoEstado)
        {
            try
            {
                Vehiculo? v = await _vehiculos
                    .Include(v => v.id_estado_vehiculoNavigation)
                    .FirstOrDefaultAsync(v => v.id_vehiculo == id);

                if (v != null)
                {
                    v.id_estado_vehiculoNavigation.estado_vehiculo = nvoEstado;
                }
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return false;
            }
        }

        public async Task<List<Vehiculo>?> GetAll()
        {
            try
            {
                return await _vehiculos.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }

        public async Task<Vehiculo?> GetById(int id)
        {
            try
            {
                return await _vehiculos.AsNoTracking().FirstOrDefaultAsync(v => v.id_vehiculo == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }

        public async Task<Vehiculo?> GetByPatent(string patente)
        {
            try
            {
                return await _vehiculos.AsNoTracking().FirstOrDefaultAsync(v => v.patente == patente);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }

        public async Task<bool> Post(Vehiculo v)
        {
            try
            {
                v.id_estado_vehiculo = 5;
                await _vehiculos.AddAsync(v);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> Put(Vehiculo v)
        {
            try
            {
                Vehiculo? vehiculo = await _vehiculos.FindAsync(v.id_vehiculo);
                    
                if (vehiculo != null)
                {
                    vehiculo.patente = v.patente;
                    vehiculo.id_tipo_vehiculo = v.id_tipo_vehiculo;
                    vehiculo.id_marca = v.id_marca;
                    vehiculo.modelo = v.modelo;
                    vehiculo.id_sucursal = v.id_sucursal;
                    vehiculo.valor_tasado = v.valor_tasado;
                }
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return false;
            }
        }

        public async Task<bool> SoftDelete(int id)
        {
            try
            {
                Vehiculo? vehiculo = await _vehiculos.FindAsync(id);
                if (vehiculo != null)
                {
                    vehiculo.id_estado_vehiculo = 35; 
                }
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return false;
            }
        }
    }
}
