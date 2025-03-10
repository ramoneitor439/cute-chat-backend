using CuteChat.Domain.Entities;

namespace CuteChat.Models;

public class CreatedEntityResponse<T> where T : struct
{
    public T? Id { get; set; }

    public CreatedEntityResponse(T id)
    {
        Id = id;
    }
}
