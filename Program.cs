using FinanceToolkitApi.EndPoints;
using FinanceToolkitApi.MiddleWare;
using FinanceToolkitApi.Services;
using Serilog;
using Polly;
using Polly.CircuitBreaker;


Log.Logger = new LoggerConfiguration()
    .WriteTo.File("Logs/FinanceToolkitApi_.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(Log.Logger);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInterestCalcService, InterestCalcService>();


builder.Services.AddHttpClient("HighThroughputApi:", client =>
{
    client.BaseAddress = new Uri("https://downstream.example.com");
    client.Timeout = Timeout.InfiniteTimeSpan;
}).AddPolicyHandler(ResiliencePolicies.CreateCompositePolicy());

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


