using Weldify.Data.Entities;

namespace Weldify.Services;

public interface ICustomerService
{
    Task<Customer> CreateAsync(
        string name,
        string phone,
        string? address);

    Task<IReadOnlyList<Customer>> GetAllAsync();

    Task<Customer?> GetByIdAsync(int id);

    Task UpdateAsync(Customer customer);
}