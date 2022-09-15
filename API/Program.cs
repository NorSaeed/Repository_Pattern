using API.Data;
using API.Entities;
using API.Middleware;
using API.Repositories;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureLogging(logging =>{
    logging.ClearProviders();
    logging.AddConsole();
});
var connectionStrings = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddScoped<IRepository<Book, int>, BookRepository>();
builder.Services.AddDbContext<LibContext>(opt => opt.UseSqlite(connectionStrings));

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_localhost_policy",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                   .AllowCredentials()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
var app = builder.Build();

// Our Own Exception middleWare
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseDeveloperExceptionPage(); instead of this line we will use our middleware 
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var context = scope.ServiceProvider.GetRequiredService<LibContext>();
    try
    {
        context.Database.Migrate();
        DbInitializer.Initialize(context);
    }
    catch(Exception ex)
    {
        logger.LogError(ex, "program migrating data");
    }
}

// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("_localhost_policy");
// app.UseAuthorization();

app.MapControllers();

app.Run();