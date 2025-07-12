using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipe_app_api.Data;
using recipe_app_api.Data.Repository;
using recipe_app_api.Exceptions;
using recipe_app_api.Interfaces;
using recipe_app_api.Services;
using Serilog;



var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"INFO Current environment: {builder.Environment.EnvironmentName}");

// Add services to the container.
builder.Services.AddDbContext<RecipeDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddSingleton<BlobService>();

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

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
    .WriteTo.File(
    "Logs/recipe-api-log.xml",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} {Message} {Exception} \n",
    rollingInterval: RollingInterval.Day,
    retainedFileCountLimit: 7)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
        var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        logger.LogWarning("ERROR Model validation failed: {Errors}", string.Join(", ", errors));

        return new BadRequestObjectResult(context.ModelState);
    };
});

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        context.Response.ContentType = "application/json";

        if (exception is NotFoundException)
        {
            logger.LogWarning(exception, "WARNING NotFoundException: {Message}", exception.Message);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(exception.Message);
        }
        else
        {
            logger.LogError(exception, "ERROR Unhandled exception occurred while processing request.");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("ERROR An unexpected error occurred.");
        }
    });
});


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
