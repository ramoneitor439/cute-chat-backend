using CuteChat.Domain.Entities;
using CuteChat.Infrastructure.Results;
using CuteChat.Models.Contact;
using CuteChat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CuteChat.Services.Contact;

public class ContactManager(ApplicationContext context) : BaseEntityService<Domain.Entities.Contact>(context)
{
    public async Task<BaseResult<IEnumerable<ContactResponse>>> GetAllAsync(int userId)
    {
        var contacts = await _repository
            .AsNoTracking()
            .Include(x => x.ContactUser)
            .Where(x => x.UserId == userId)
            .ToListAsync();

        if (contacts.Count == 0)
            return Result.Success(Enumerable.Empty<ContactResponse>());

        var userContactIds = contacts.Select(x => x.ContactUserId);

        var latestReceivedMesages = await context.Set<Message>()
            .AsNoTracking()
            .Where(x => x.ReceiverId == userId)
            .GroupBy(x => x.SenderId)
            .Select(x => new { SenderId = x.Key, LastMessage = x.OrderByDescending(x => x.CreatedAt).FirstOrDefault() })
            .ToListAsync();

        var latestSentMessages = await context.Set<Message>()
            .AsNoTracking()
            .Where(x => x.SenderId == userId)
            .GroupBy(x => x.ReceiverId)
            .Select(x => new { ReceiverId = x.Key, LastMessage = x.OrderByDescending(x => x.CreatedAt).FirstOrDefault() })
            .ToListAsync();

        var latestMessagesByContact = latestReceivedMesages
            .Join(latestSentMessages,
                  received => received.SenderId,
                  sent => sent.ReceiverId,
                  (received, sent) => new
                  {
                      ContactId = received.LastMessage!.CreatedAt > sent.LastMessage!.CreatedAt ? received.SenderId : sent.ReceiverId,
                      Message = received.LastMessage!.CreatedAt > sent.LastMessage!.CreatedAt ? received.LastMessage : sent.LastMessage
                  });

        var result = contacts
            .GroupJoin(latestMessagesByContact,
                  contact => contact.ContactUserId,
                  message => message.ContactId,
                  (contact, messages) => new ContactResponse
                  {
                      ContactUserId = contact.ContactUserId,
                      LastConnection = DateTime.UtcNow,
                      LastMessage = messages.FirstOrDefault()?.Message.Content,
                      Name = contact.ContactUser?.FullName ?? string.Empty
                  });

        return Result.Success(result);
    }

    public async Task<BaseResult<int>> CreateAsync(int userId, CreateContactRequest request)
    {
        var contactUser = context.Set<Domain.Entities.AppUser>()
            .AsNoTracking()
            .FirstOrDefault(x => x.Email == request.Email);

        if (contactUser is null)
            return Result.BadRequest<int>("Email", "No contact with thah email was found");

        if (contactUser.Id == userId)
            return Result.Error<int>("Same user", "This contact references to the same user", 409);

        var contact = new Domain.Entities.Contact(userId, contactUser.Id);
        context.Set<Domain.Entities.Contact>().Add(contact);

        await context.SaveChangesAsync();
        return Result.Success(contact.Id);
    }
}
