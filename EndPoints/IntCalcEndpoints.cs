namespace FinanceToolkitApi.EndPoints
{
    public static partial class IntCalcEndpoints
    {
        public static void MapInterestCalcEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/IntCalc/Accrued", Accrued);


            app.MapPost("/IntCalc/Amortized", Amortized);
            app.MapPost("/IntCalc/Compound", Compound);
            app.MapPost("/IntCalc/Continuous", Continuous);
            app.MapPost("/IntCalc/Daily", Daily);
            app.MapPost("/IntCalc/EffectiveRate", EffectiveRate);
            app.MapPost("/IntCalc/InflationAdjusted", InflationAdjusted);
            app.MapPost("/IntCalc/Simple", Simple);
            app.MapPost("/IntCalc/TaxAdjusted", TaxAdjusted);
        }
       

    }
}
