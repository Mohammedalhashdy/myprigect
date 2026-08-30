using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Application.DTOs;

namespace Mafqoodi.Application.CQRS.Organizations;

public sealed record GetOrganizationsQuery : IRequest<IReadOnlyList<OrganizationResponse>>;

public sealed class GetOrganizationsHandler(IOrganizationRepository organizations) : IRequestHandler<GetOrganizationsQuery, IReadOnlyList<OrganizationResponse>>
{
    public async Task<IReadOnlyList<OrganizationResponse>> Handle(GetOrganizationsQuery request, CancellationToken ct)
        => (await organizations.GetActiveAsync(ct)).Select(x => new OrganizationResponse(x.Id, x.Name, x.Description, x.Phone, x.Address, x.LogoUrl, x.IsActive)).ToList();
}
