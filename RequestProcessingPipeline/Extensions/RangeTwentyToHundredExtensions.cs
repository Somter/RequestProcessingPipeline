using System;

public static class RangeTwentyToHundredExtensions
{
    public static IApplicationBuilder UseTwentyToHundred(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RangeTwentyToHundredMiddleware>();
    }
}