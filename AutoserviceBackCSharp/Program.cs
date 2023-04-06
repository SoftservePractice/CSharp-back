using AutoserviceBackCSharp.Controllers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc().AddJsonOptions(o => {
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
});
builder.Services.AddDbContext<AutoserviceBackCSharp.Models.PracticedbContext>();
builder.Services.AddSingleton(_ => new AutoserviceBackCSharp.Singletone.DbConnection(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(
        options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()
    );


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
