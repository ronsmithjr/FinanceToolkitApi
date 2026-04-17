using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;



namespace FinanceToolkitApi.MiddleWare
{
    /// <summary>
    /// How this scales to ~20k RPS
    //Low retry count(0–2) :
    //At 20k RPS, every extra retry is expensive.Use retries only for true transients(timeouts, 5xx, 429).

    //Short timeouts(≈1–2s):  
    //You’d rather fail fast and try another instance than let calls hang.This keeps thread/connection pools healthy.
    //    Bulkhead per downstream:

    //Example: service A gets 200 concurrent, service B gets 100, etc.

    //This prevents one flaky dependency from consuming all your app’s resources.

    //Circuit breaker tuned for volume:

    //handledEventsAllowedBeforeBreaking: 50 avoids opening on tiny blips when you’re doing thousands of calls/sec.

    //durationOfBreak: 10s is enough to shed load but short enough to recover quickly.

    //Jitter:  
    //Prevents synchronized retry spikes when a dependency hiccups—critical at 20k RPS.
    /// </summary>
    public static class ResiliencePolicies
    {
        public static IAsyncPolicy<HttpResponseMessage> CreateCompositePolicy()
        {
            return Policy.WrapAsync(
                GetCircuitBreakerPolicy(),
                GetRetryPolicy(),
                GetBulkheadPolicy(),
                GetTimeoutPolicy()
                );
        }

        public static IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
        {
            return Policy.TimeoutAsync<HttpResponseMessage>(
                TimeSpan.FromSeconds(1.5),
                TimeoutStrategy.Optimistic
                );
        }
        public static IAsyncPolicy<HttpResponseMessage> GetBulkheadPolicy()
        {
            return Policy.BulkheadAsync<HttpResponseMessage>(
                maxParallelization: 200,
                maxQueuingActions: 400
                );
        }
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(
                retryCount: 2,
                sleepDurationProvider: attempt =>
                {
                    var baseDelayMs = Math.Pow(2, attempt) * 50;
                    var jitterMs = Random.Shared.Next(-25, 25);
                    return TimeSpan.FromMilliseconds(baseDelayMs + jitterMs);
                });
        }
        public static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(
                    handledEventsAllowedBeforeBreaking: 50,
                    durationOfBreak: TimeSpan.FromSeconds(10),
                    onBreak: (outcome, breakDelay) =>
                    {
                        if (outcome.Exception != null)
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
