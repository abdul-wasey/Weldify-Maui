using Microsoft.EntityFrameworkCore;
using Weldify.Data;
using Weldify.Data.Entities;

namespace Weldify.Services;

public class CustomerService : ICustomerService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public CustomerService(
        IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Customer> CreateAsync(
        string name,
        string phone,
        string? address)
    {
        var customer = new Customer
        {
            Name = name.Trim(),
            Phone = phone.Trim(),
            Address = string.IsNullOrWhiteSpace(address)
                ? null
                : address.Trim()
        };

        await using var db =
            await _contextFactory.CreateDbContextAsync();

        db.Customers.Add(customer);

        await db.SaveChangesAsync();

        return customer;
    }

    public async Task<IReadOnlyList<Customer>> GetAllAsync()
    {
        await using var db =
            await _contextFactory.CreateDbContextAsync();

        return await db.Customers
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        await using var db =
            await _contextFactory.CreateDbContextAsync();

        return await db.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync(Customer customer)
    {
        await using var db =
            await _contextFactory.CreateDbContextAsync();

        db.Customers.Update(customer);

        await db.SaveChangesAsync();
    }
}