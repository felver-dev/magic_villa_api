using AutoMapper;
using Microsoft.EntityFrameworkCore;
using villaApi.Repository;
using villaApi.Repository.IRepository;
using villaApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddDbContext<VillaDBContext>(options => 
{
	var villa = builder.Configuration.GetConnectionString("villa");
	options.UseMySql(villa, ServerVersion.AutoDetect(villa));
});
builder.Services.AddAutoMapper(typeof(villaApi.Config.MappingConfig));
builder.Services.AddScoped<IVillaRepository, VillaRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();
