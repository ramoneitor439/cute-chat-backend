namespace CuteChat.Infrastructure.Results;

public interface IBaseResult
{
    bool IsSuccess { get; }
    IEnumerable<ErrorResult> Errors { get; set; }
}
