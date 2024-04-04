using Microsoft.EntityFrameworkCore;
using ProductFacilities.Application.Interface;
using ProductFacilities.Application.Mapping;
using ProductFacilities.Application.Repository;
using ProductFacilities.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowOrigin", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(isOriginAllowed: _ => true)
        .AllowCredentials();
    });

});

var app = builder.Build();
app.UseRouting();
app.UseCors("AllowOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
