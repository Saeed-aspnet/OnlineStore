using FluentValidation.AspNetCore;
using OnlineStore.Application;
using OnlineStore.Persistance;
using OnlineStore.WebApi.Middleware;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddServices();

builder.Services.AddControllers()
       .AddFluentValidation(options =>
       {
           options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
       });


builder.Services.AddMemoryCache();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(x =>
{
    x.AddPolicy("Any", b =>
    {
        b.AllowAnyOrigin();
        b.AllowAnyHeader();
        b.AllowAnyMethod();

    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnlineStore.WebApi v1"));
}

app.UseStaticFiles();

app.UseCors("Any");
app.UseRouting();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();
public partial class Program { }