using MiddlewareDemo.Middlewares;

var builder = WebApplication.CreateBuilder(args);

#region Commented Code
// Add services to the container.

//builder.Services.AddControllers();
//builder.Services.AddOpenApi();
#endregion

builder.Services.AddTransient<CustomMiddleware>();
builder.Services.AddTransient<CustomMiddleware2>();

var app = builder.Build();

app.UseMiddleware<CustomMiddleware>();
app.UseMiddleware<CustomMiddleware2>();

#region Branching using app.Map()
//app.Map("/admin", adminApp =>
//{
//    adminApp.Use(async (context, next) =>
//    {
//        await context.Response.WriteAsync("Middleware #4 - Before calling next \n");
//        await next();
//        await context.Response.WriteAsync("Middleware #4 - After calling next\n");
//    });

//    adminApp.Use(async (context, next) =>
//    {
//        await context.Response.WriteAsync("Middleware #5 - Before calling next \n");
//        await next();
//        await context.Response.WriteAsync("Middleware #5 - After calling next\n");
//    });

//    adminApp.Run(async context =>
//    {
//        await context.Response.WriteAsync("Admin Endpoint\n");
//    });
//});

#endregion 

#region Branching using app.MapWhen()

//app.MapWhen(context =>
//{
//    return context.Request.Query.ContainsKey("id");
//}, adminApp =>
//{
//    adminApp.Use(async (context, next) =>
//    {
//        await context.Response.WriteAsync("Middleware #4 - Before calling next \n");
//        await next();
//        await context.Response.WriteAsync("Middleware #4 - After calling next\n");
//    });

//    adminApp.Use(async (context, next) =>
//    {
//        await context.Response.WriteAsync("Middleware #5 - Before calling next \n");
//        await next();
//        await context.Response.WriteAsync("Middleware #5 - After calling next\n");
//    });

//    adminApp.Run(async context =>
//    {
//        await context.Response.WriteAsync("Admin Endpoint\n");
//    });
//});
#endregion

#region Middleware using app.Use()

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware #1 - Before calling next \n");
//    await next();
//    await context.Response.WriteAsync("Middleware #1 - After calling next\n");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware #2 - Before calling next \n");

//    // Short Circuting
//    //if (true)
//    //{
//    //    return;
//    //}

//    await next();
//    await context.Response.WriteAsync("Middleware #2 - After calling next\n");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware #3 - Before calling next \n");
//    await next();
//    await context.Response.WriteAsync("Middleware #3 - After calling next\n");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware #4 - Before calling next \n");
//    await next();
//    await context.Response.WriteAsync("Middleware #4 - After calling next\n");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Middleware #5 - Before calling next \n");
//    await next();
//    await context.Response.WriteAsync("Middleware #5 - After calling next\n");
//});

#endregion

#region Terminal middleware
//app.Run(async context =>
//{
//    await context.Response.WriteAsync("This is terminal middleware\n");
//});

#endregion


#region Commented code

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
#endregion

app.Run();
