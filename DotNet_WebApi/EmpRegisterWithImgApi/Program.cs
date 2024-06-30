using EmpRegisterWithImgApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext <AppDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors(options =>
    options.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath,"Images")),
    RequestPath = "/Images"
});

app.UseHttpsRedirection();



app.UseAuthorization();

app.MapControllers();

app.Run();
