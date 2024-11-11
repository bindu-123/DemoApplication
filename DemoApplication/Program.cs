using DemoApplication.Business.Interface;
using DemoApplication.Business.Service;
using DemoApplication.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();
// Add services to the container.

builder.Host.UseSerilog((context, loggerConfig) => { 
    loggerConfig.ReadFrom.Configuration(context.Configuration);
});
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IHomeService, HomeService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Home/Error");
}
// register custom middleware
app.UseMiddleware<GlobalExceptionHandler>();
app.UseSerilogRequestLogging();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
