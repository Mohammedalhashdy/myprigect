using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Application.Abstractions;

public interface IOrganizationRepository
{
    Task<IReadOnlyList<Organization>> GetActiveAsync(CancellationToken ct);
}
