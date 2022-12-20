using DB;
using Microsoft.EntityFrameworkCore;
using PruebaAPI.Helpers;
using PruebaAPI.Services.OwnerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOwnerService, OwnerService>();

builder.Services.AddAutoMapper(typeof(AutoMappingProfiles).Assembly);
builder.Services.AddDbContext<PetClinicContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DotNetAPIConnection"));
});

var app = builder.Build();

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
