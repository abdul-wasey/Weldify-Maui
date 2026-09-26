using Microsoft.EntityFrameworkCore;
using Weldify.Data;
using Weldify.Data.Entities;

namespace Weldify.Services;

public class JobService : IJobService
{
    private readonly IDbContextFactory<AppDbContext> _contextFactory;

    public JobService(IDbContextFactory<AppDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<Job> CreateAsync(Job job)
    {
        await using var db = await _contextFactory.CreateDbContextAsync();

        var customerExists = await db.Customers
            .AsNoTracking()
            .AnyAsync(x => x.Id == job.CustomerId);

        if (!customerExists)
            throw new InvalidOperationException("The selected customer no longer exists.");

        var jobNumberExists = await db.Jobs
            .AsNoTracking()
            .AnyAsync(x => x.JobNumber == job.JobNumber);

        if (jobNumberExists)
            throw new InvalidOperationException("A job with this job number already exists.");

        db.Jobs.Add(job);
        await db.SaveChangesAsync();

        return job;
    }
}
