using Weldify.Data.Entities;

namespace Weldify.Services;

public interface IJobService
{
    Task<Job> CreateAsync(Job job);
}