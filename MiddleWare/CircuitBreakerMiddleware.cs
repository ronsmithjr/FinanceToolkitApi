using Polly.CircuitBreaker;
using Polly;
using Polly.Extensions.Http;

namespace FinanceToolkitApi.MiddleWare
{
    public class CircuitBreakerMiddleware
    {
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                retryCount: 3,
                sleepDurationProvider: attempt =>
                TimeSpan.FromSeconds(Math.Pow(2, attempt)) +
                TimeSpan.FromMilliseconds(Random.Shared.Next(0, 100)),
                onRetry: (outcome, delay, attempt, context) =>
                {
                    if(outcome.Exception != null)
                    {
                        Serilog.Log.Warning(
                            "Retry {Attempt} after {Delay}ms due to {ExceptionMessage}",
                            attempt,
                            delay.TotalMilliseconds,
                            outcome.Exception.Message);
                    }
                    else
                    {
                        Serilog.Log.Warning(
                            "Retry {Attempt} after {Delay}ms due to {Result}",
                            attempt,
                            delay.TotalMilliseconds,
                            outcome.Result?.StatusCode);
                    }
                });
        }

        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 5,
                    durationOfBreak: TimeSpan.FromSeconds(30),
                    onBreak: (outcome, breakDelay) =>
                    {
                        if(outcome.Exception != null)
                        {
                            Serilog.Log.Warning(
                                "Circuit breaker opened for {BreakDelay} due to exception: {ExceptionMessage}",
                                breakDelay.TotalMilliseconds,
                                outcome.Exception.Message);
                        }
                        else
                        {
                            Serilog.Log.Warning(
                                "Circuit breaker opened for {BreakDelay}ms due to result: {Result}",
                                breakDelay.TotalMilliseconds,
                                outcome.Result?.StatusCode);
                        }
                    },
                    onReset: () =>
                    {
                        Serilog.Log.Information("Circuit breaker reset.  Normal operation has resumed.");
                    },
                    onHalfOpen: () =>
                    {
                        Serilog.Log.Information("Circuit breaker is half-open. Testing for recovery.");
                    });
        }
    }
}
