namespace CuteChat.Domain.Entities;

public abstract class BaseEntity<T>
{
    public T Id { get; set; } = default!;
}

public abstract class BaseEntity : BaseEntity<int>;
