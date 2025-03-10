namespace CuteChat.WebApi.Middlewares;

public class SlowResponseMiddleware
{
    private readonly RequestDelegate _next;

    public SlowResponseMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var random = new Random();
        await Task.Delay(random.Next(500, 2500));

        await _next(context);
    }
}
