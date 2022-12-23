using DB;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.DTO;
using PruebaAPI.Helpers;
using PruebaAPI.Middleware;
using PruebaAPI.Services.OwnerService;
using PruebaAPI.Services.PetService;
using PruebaAPI.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddValidatorsFromAssemblyContaining<OwnerValidator>();
builder.Services.AddScoped<IOwnerService, OwnerService>();
builder.Services.AddScoped<IPetService, PetService>();
//https://github.com/FluentValidation/FluentValidation/issues/1965
builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
builder.Services.AddDbContext<PetClinicContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DotNetAPIConnection"));
});

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PetClinicContext>();
    context.Database.Migrate();
}


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
