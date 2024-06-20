using ProductManagementApp.Domain.Repositories;
using ProductManagementApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        builder =>
        {
            builder.WithOrigins("http://localhost:5291") // The URL of your Blazor WebAssembly app
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddLogging();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowBlazorClient");

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManagementApp API V1"));

app.Run();

