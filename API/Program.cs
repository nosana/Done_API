

using API.Context;
using API.Repository.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/// yang di tambahkan
/// menambakahn sesuai repository
builder.Services.AddScoped<DivisionRepository>();
builder.Services.AddScoped<DepartmentRepository>();
builder.Services.AddScoped<AccountRepository>();


builder.Services.AddDbContext<MyContext>(option =>
                            option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

/*builder.Services.AddDbContext<MyContext>(option =>
                        option.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));*/


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
