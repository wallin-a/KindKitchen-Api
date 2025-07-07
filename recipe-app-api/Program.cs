using Microsoft.EntityFrameworkCore;
using recipe_app_api.Data;
using recipe_app_api.Data.Repository;
using recipe_app_api.Interfaces;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RecipeDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000", "https://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
    "Logs/recipe-api-log.xml",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {Message} {Exception} \n",
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 7)
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
