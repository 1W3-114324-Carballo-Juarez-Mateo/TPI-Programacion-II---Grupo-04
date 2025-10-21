using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using WebApi_TPI_AIRCNR_PII.Models;
using WebApi_TPI_AIRCNR_PII.Repositories.Interfaces;

namespace WebApi_TPI_AIRCNR_PII.Repositories.Implementations
{
    public class AlquilerRepository : IAlquilerRepository
    {
        private readonly AlquileresContext _context;
        private readonly DbSet<Alquiler> _alquileres;
        public AlquilerRepository(AlquileresContext context)
        {
            _context = context;
            _alquileres = _context.Set<Alquiler>();
        }
        public async Task<bool> CambioEstado(int id, string estado)
        {
            Alquiler? alquiler = await _alquileres
                .Include(a => a.id_estado_alquilerNavigation)
                .FirstOrDefaultAsync(a => a.id_alquiler == id);
            if (alquiler != null)
            {
                alquiler.id_estado_alquilerNavigation.estado = estado;
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Alquiler>?> GetAll(string docCliente, string? estado)
        {
            try
            {
                IQueryable<Alquiler> query = _alquileres
                    .AsNoTracking()
                    .Include(a => a.id_clienteNavigation)
                    .Include(a => a.id_estado_alquilerNavigation)
                    .Include(a => a.id_vehiculoNavigation)
                    .Where(a => a.id_clienteNavigation.documento == docCliente);

                if (!string.IsNullOrEmpty(estado))
                {
                    query = query.Where(a => a.id_estado_alquilerNavigation.estado == estado);
                }

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error inesperado: " + ex.Message);
                return null;
            }
        }

        public Task<Alquiler?> GetById(int id)
        {
            return _alquileres.AsNoTracking().FirstOrDefaultAsync(a => a.id_alquiler == id);
        }

        public async Task<bool> Post(Alquiler a)
        {
            await _alquileres.AddAsync(a);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Put(Alquiler a)
        {
            Alquiler? alquiler = await _alquileres
                .Include(a => a.id_clienteNavigation)
                .FirstOrDefaultAsync(al => al.id_alquiler == a.id_alquiler);

            if(alquiler != null)
            {
                alquiler.id_clienteNavigation = a.id_clienteNavigation;
                alquiler.id_vehiculo = a.id_vehiculo;
                alquiler.monto = a.monto;
                alquiler.fecha_inicio = a.fecha_inicio;
                alquiler.fecha_fin = a.fecha_fin; 
            }
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
