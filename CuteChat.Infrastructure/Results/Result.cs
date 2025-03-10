using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace CuteChat.Infrastructure.Results;
public static class Result
{
    public static BaseResult<T> Error<T>(string name, string description, int status)
        => new()
        {
            Data = default,
            Errors = [
                new ErrorResult {
                    Name = name,
                    Description = description
                }
            ],
            Status = (HttpStatusCode)status
        };
    public static BaseResult<T> Error<T>(string name, string description)
        => new()
        {
            Data = default,
            Errors = [ 
                new ErrorResult {
                    Name = name,
                    Description = description
                }
            ],
            Status = HttpStatusCode.InternalServerError
        };

    public static BaseResult<Empty> Error(string name, string description)
        => new()
        {
            Data = default,
            Errors = [
                new ErrorResult {
                    Name = name,
                    Description = description
                }
            ],
            Status = HttpStatusCode.InternalServerError
        };

    public static BaseResult<T> Error<T>(string name, params string[] descriptions)
        => new()
        {
            Data = default,
            Errors = descriptions.Select(description => new ErrorResult
            {
                Name = name,
                Description = description
            }),
            Status = HttpStatusCode.InternalServerError
        };

    public static BaseResult<Empty> Error(string name, params string[] descriptions)
        => new()
        {
            Data = default,
            Errors = descriptions.Select(description => new ErrorResult
            {
                Name = name,
                Description = description
            }),
            Status = HttpStatusCode.InternalServerError
        };

    public static BaseResult<T> Success<T>(T data)
        => new()
        {
            Data = data,
            Errors = [],
            Status = HttpStatusCode.OK
        };

    public static BaseResult<Empty> Success()
        => new();

    public static BaseResult<T> BadRequest<T>(string field, string description)
        => new()
        {
            Errors = [
                new(){ Name = field, Description = description }    
            ],
            Data = default,
            Status = HttpStatusCode.BadRequest
        };

    public static BaseResult<T> Unauthorized<T>()
        => new() { Status = HttpStatusCode.Unauthorized, Errors = [new() { Name = "Unauthorized", Description = "Unauthorized" }] };
}
