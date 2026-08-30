using MediatR;
using Mafqoodi.Application.Abstractions;

namespace Mafqoodi.Application.CQRS.Support;

public sealed record GetMySupportChatsQuery(Guid UserId) : IRequest<IReadOnlyList<SupportChatSummary>>;
public sealed record GetSupportMessagesQuery(Guid UserId, Guid ChatId) : IRequest<IReadOnlyList<SupportMessageDto>>;

public sealed record SupportChatSummary(Guid Id, DateTime CreatedAt, int MessageCount);
public sealed record SupportMessageDto(Guid Id, Guid SenderId, string Body, bool IsRead, DateTime CreatedAt);

public sealed class GetMySupportChatsHandler(ISupportRepository repository)
    : IRequestHandler<GetMySupportChatsQuery, IReadOnlyList<SupportChatSummary>>
{
    public async Task<IReadOnlyList<SupportChatSummary>> Handle(GetMySupportChatsQuery request, CancellationToken ct)
        => (await repository.GetChatsAsync(ct)).Where(x => x.UserId == request.UserId)
            .Select(x => new SupportChatSummary(x.Id, x.CreatedAt, x.Messages.Count)).ToList();
}

public sealed class GetSupportMessagesHandler(ISupportRepository repository)
    : IRequestHandler<GetSupportMessagesQuery, IReadOnlyList<SupportMessageDto>>
{
    public async Task<IReadOnlyList<SupportMessageDto>> Handle(GetSupportMessagesQuery request, CancellationToken ct)
    {
        var chat = await repository.GetChatAsync(request.ChatId, ct)
            ?? throw new KeyNotFoundException("المحادثة غير موجودة.");
        if (chat.UserId != request.UserId) throw new UnauthorizedAccessException("لا تملك صلاحية قراءة هذه المحادثة.");
        return chat.Messages.OrderBy(x => x.CreatedAt)
            .Select(x => new SupportMessageDto(x.Id, x.SenderId, x.Body, x.IsRead, x.CreatedAt)).ToList();
    }
}
