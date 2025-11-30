using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ubereats.Data;
using Ubereats.Helpers;
using Ubereats.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ubereats API Project", Version = "v1" });

        // Define a security scheme for JWT
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT"
        });

        //Make sure all API methods are authenticated
        // c.AddSecurityRequirement(new OpenApiSecurityRequirement
        // {
        //     {
        //         new OpenApiSecurityScheme
        //         {
        //             Reference = new OpenApiReference
        //             {
        //                 Type = ReferenceType.SecurityScheme,
        //                 Id = "Bearer"
        //             }
        //         },
        //         new string[] { }
        //     }
        // });
        // 🔐 Only apply security requirement when [Authorize] is used
        c.OperationFilter<AuthorizeCheckOperationFilter>();
    });
builder.Services.AddDbContext<UberEatsContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
// register the services for DI 
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRestaurantRepository, RestaurantRepository>();

// add CORS policy to allow only communication with Flutter app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowUberEats", builder =>
    {
        builder.WithOrigins("http://192.168.0.175:40160", "http://10.0.2.2:40160").AllowAnyHeader().AllowAnyMethod();
    });
});
// use lowercase URLs 
builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ubereats API V1");
    });
    //app.MapOpenApi();
}
app.UseStaticFiles(); // use wwwwroot
app.MapControllers();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("AllowUberEats");
app.Run();
