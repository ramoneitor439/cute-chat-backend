using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CuteChat.Infrastructure.Results;

public class Empty;
public class BaseResult<T> : IBaseResult
{
    public bool IsSuccess => !Errors.Any();
    public T? Data { get; set; }
    public IEnumerable<ErrorResult> Errors { get; set; } = [];
    public HttpStatusCode Status { get; set; } = HttpStatusCode.OK;

    public IActionResult ToActionResult()
    {
        object? value = IsSuccess ? Data : string.Join(",", Errors.Select(x => x.Description));
        var result = new ObjectResult(value)
        {
            StatusCode = (int)Status
        };
        return result;
    }
}
