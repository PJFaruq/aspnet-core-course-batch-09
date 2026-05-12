namespace MiddlewareDemo.Middlewares
{
    public class CustomMiddleware2 : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware 2- Before calling next \n");
            await next(context);
            await context.Response.WriteAsync("Custom Middleware 2- After calling next\n");
        }
    }
}
