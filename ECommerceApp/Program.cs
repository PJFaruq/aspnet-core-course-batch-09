
using ECommerceApp.BusinessLayer.Modules.Categories;
using ECommerceApp.BusinessLayer.Modules.Categories.Interface;
using ECommerceApp.BusinessLayer.Modules.Products;
using ECommerceApp.BusinessLayer.Modules.Products.Interface;
using ECommerceApp.DataAccessLayer.Data;
using ECommerceApp.DataAccessLayer.Modules.Categories;
using ECommerceApp.DataAccessLayer.Modules.Categories.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Products;
using ECommerceApp.DataAccessLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Categories;
using ECommerceApp.PresentationLayer.Modules.Categories.Interfaces;
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

builder.Services.AddScoped<ICategoryViewModelProvider, CategoryViewModelProvider>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

builder.Services.AddScoped<IProductViewModelProvider, ProductViewModelProvider>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
