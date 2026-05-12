namespace MiddlewareDemo.Middlewares
{
    public class CustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Custom Middleware 1- Before calling next \n");
            await next(context);
            await context.Response.WriteAsync("Custom Middleware 1- After calling next\n");
        }
    }
}
