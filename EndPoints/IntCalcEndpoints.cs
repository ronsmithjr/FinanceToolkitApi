namespace FinanceToolkitApi.EndPoints
{
    public static partial class IntCalcEndpoints
    {
        public static void MapInterestCalcEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/IntCalc/Simple", SimpleInterest);
            app.MapPost("/IntCalc/SimpleAccruedInterest", SimpleAccruedInterest);
            app.MapPost("/IntCalc/CompoundAccruedInterest", CompoundAccruedInterest);

            app.MapPost("/IntCalc/Compound", Compound);
            app.MapPost("/IntCalc/CompundWithContributions", CompoundInterestWithContributions);


            app.MapPost("/IntCalc/AmortizedPayment", AmortizedPayment);
            app.MapPost("/IntCalc/AmortizedSchedule", AmortizedSchedule);
           
            app.MapPost("/IntCalc/Continuous", Continuous);
            app.MapPost("/IntCalc/Daily", Daily);
            app.MapPost("/IntCalc/EffectiveRate", EffectiveRate);
            app.MapPost("/IntCalc/InflationAdjusted", InflationAdjusted);
            app.MapPost("/IntCalc/TaxAdjusted", TaxAdjusted);
        }
       

    }
}
