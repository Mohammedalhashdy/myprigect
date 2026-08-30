using Microsoft.EntityFrameworkCore;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Domain.Entities;
using Mafqoodi.Infrastructure.Persistence;

namespace Mafqoodi.Infrastructure.Repositories;

public sealed class OrganizationRepository(ApplicationDbContext db) : IOrganizationRepository
{
    public async Task<IReadOnlyList<Organization>> GetActiveAsync(CancellationToken ct)
        => await db.Organizations.AsNoTracking().Where(x => x.IsActive).OrderBy(x => x.Name).ToListAsync(ct); // قراءة بدون تتبع
}
