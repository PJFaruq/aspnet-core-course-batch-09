
using ECommerceApp.BusinessLayer.Modules.Carts;
using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Categories;
using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.BusinessLayer.Modules.Orders;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Products;
using ECommerceApp.BusinessLayer.Modules.Products.Interface;
using ECommerceApp.DataAccessLayer.Data;
using ECommerceApp.DataAccessLayer.Modules.Carts;
using ECommerceApp.DataAccessLayer.Modules.Carts.Inerfaces;
using ECommerceApp.DataAccessLayer.Modules.Categories;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Orders;
using ECommerceApp.DataAccessLayer.Modules.Orders.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Products;
using ECommerceApp.DataAccessLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Carts;
using ECommerceApp.PresentationLayer.Modules.Carts.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories;
using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products;
using ECommerceApp.PresentationLayer.Modules.Products.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ECommerceDbContext>(options =>
options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddAutoMapper(cfg => { }, typeof(CategoryMappingProfile).Assembly);

//Session related services
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICategoryViewModelProvider, CategoryViewModelProvider>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductViewModelProvider, ProductViewModelProvider>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddScoped<ICartRepository, SessionCartRepository>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartViewModelProvider, CartViewModelProvider>();

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ICheckoutViewModelProvider, CheckoutViewModelProvider>();

var app = builder.Build();

app.UseStatusCodePagesWithReExecute("/Error/StatusCode", "?statusCode={0}");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error/ServerError");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
