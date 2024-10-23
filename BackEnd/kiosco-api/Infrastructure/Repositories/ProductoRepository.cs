using Infrastructure.Data;
using kiosco_api.Models;
using Microsoft.EntityFrameworkCore;


public class ProductoRepository : IProductoRepository
{
    private readonly AppDbContext _context;

    public ProductoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Producto>> GetAll()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task<Producto> GetById(int id)
    {
        return await _context.Productos.FindAsync(id);
    }

    public async Task Add(Producto producto)
    {
        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Producto producto)
    {
        _context.Entry(producto).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();
    }
}
