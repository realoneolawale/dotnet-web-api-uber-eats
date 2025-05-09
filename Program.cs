using Microsoft.EntityFrameworkCore;
using Ubereats.Data;
using Ubereats.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UberEatsContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// register the services for DI 
builder.Services.AddScoped<IUserRepository, UserRepository>();

// add CORS policy to allow only communication with Flutter app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUberEats", builder =>
    {
        builder.WithOrigins("http://192.168.0.175:40160", "http://10.0.2.2:40160").AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.MapOpenApi();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowUberEats");
app.Run();
