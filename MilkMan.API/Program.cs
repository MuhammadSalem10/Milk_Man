using MilkMan.API;
using MilkMan.Infrastructure;
using MilkMan.Application;
using Serilog;
using MilkMan.API.Filters;
using MilkMan.Infrastructure.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelValidationFilter>();
});


builder.Services.AddControllers().AddNewtonsoftJson();



builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();


var jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Host.UseSerilog((context, configuration) =>
configuration.ReadFrom.Configuration(context.Configuration)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapHub<OrderHub>("/orderHub");
//app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
app.UseExceptionHandler();
app.UseCors("CorsPolicy");
//app.UseMiddleware<RoleInitializerMiddleware>();
app.UseStaticFiles();
app.Run();
