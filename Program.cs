using APIPedidos.Data;
using APIPedidos.Endpoints.Categories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("Pedidos"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

#region Endpoints

app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);

#endregion

app.Run();

