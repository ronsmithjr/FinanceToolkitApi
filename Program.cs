using FinanceToolkitApi.EndPoints;
using FinanceToolkitApi.MiddleWare;
using FinanceToolkitApi.Services;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/FinanceToolkitApi_.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Log.Logger);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInterestCalcService, InterestCalcService>();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapInterestCalcEndpoints();


app.Run();

Log.CloseAndFlush();

