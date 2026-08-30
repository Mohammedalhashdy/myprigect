using MediatR;
using Mafqoodi.Application.Abstractions;
using Mafqoodi.Domain.Entities;

namespace Mafqoodi.Application.CQRS.Support;

public sealed record SendSupportMessageCommand(Guid UserId, Guid ChatId, string Body) : IRequest<Guid>;

public sealed class SendSupportMessageCommandHandler(ISupportRepository support)
    : IRequestHandler<SendSupportMessageCommand, Guid>
{
    public async Task<Guid> Handle(SendSupportMessageCommand command, CancellationToken ct)
    {
        var chat = await support.GetChatAsync(command.ChatId, ct)
            ?? throw new KeyNotFoundException("المحادثة غير موجودة.");
        if (chat.UserId != command.UserId)
            throw new UnauthorizedAccessException("لا تملك صلاحية الكتابة في هذه المحادثة.");

        var message = new SupportMessage { Id = Guid.NewGuid(), ChatId = chat.Id, SenderId = command.UserId, Body = command.Body.Trim() };
        await support.AddMessageAsync(message, ct);
        await support.SaveChangesAsync(ct);
        return message.Id;
    }
}
