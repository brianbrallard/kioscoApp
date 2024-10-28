using Infrastructure.Data;
using kiosco_api.Models;
using Microsoft.EntityFrameworkCore;


public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<Product> GetById(int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
        throw new KeyNotFoundException($"Product with ID {id} not found.");
    }
    return product;
}

    public async Task Add(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Product product)
    {
        _context.Entry(product).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
{
    var product = await _context.Products.FindAsync(id);
    if (product == null)
    {
        throw new KeyNotFoundException($"Product with ID {id} not found.");
    }
    _context.Products.Remove(product);
    await _context.SaveChangesAsync();
}
}
