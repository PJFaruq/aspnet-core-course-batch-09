var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

var products = new List<Product>
{
    new Product { Id = 1, Name = "Laptop", Price = 1000 },
    new Product { Id = 2, Name = "Phone", Price = 500 },
    new Product { Id = 3, Name = "Book", Price = 1500 },

};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

//Get All products
app.MapGet("/products", () =>
{
    return products;
});

//Get By Id
app.MapGet("/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    return product;
});

//Post
app.MapPost("/products", (Product product) =>
{
    product.Id = products.Max(p => p.Id) + 1;
    products.Add(product);
    return product;

});

//PUT
app.MapPut("/products/{id}", (int id,Product updatedProduct) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null) return null;

    product.Name = updatedProduct.Name;
    product.Price = updatedProduct.Price;
    return product;
});

//PUT
app.MapPatch("/products/{id}", (int id, Product updatedProduct) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null) return null;

    if (!string.IsNullOrEmpty(updatedProduct.Name))
    {
        product.Name = updatedProduct.Name;
    }

    if (updatedProduct.Price > 0)
    {
        product.Price = updatedProduct.Price;
    }
    
    return product;
});

app.MapDelete("/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(p => p.Id == id);
    if (product is null) return null;

    products.Remove(product);

    return products;
});




app.Run();


public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}
