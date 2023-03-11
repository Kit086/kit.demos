using Microsoft.AspNetCore.Mvc;
using Serilog;
using SerilogTimings;

namespace TimingAndDiagnostic.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IDiagnosticContext _diagnosticContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IDiagnosticContext diagnosticContext)
    {
        _logger = logger;
        _diagnosticContext = diagnosticContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        _diagnosticContext.Set("Username", "kitlau");
        _diagnosticContext.Set("UserId", 10086);
        
        using (Operation.Time("Do some DBQuery"))
        {
            await DBQuery();
        }

        using (Operation.Time("Do some IOTask"))
        {
            await IOTask();
        }

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    private async Task DBQuery()
    {
        await Task.Delay(100);
    }

    private async Task IOTask()
    {
        await Task.Delay(1000);
    }
}
