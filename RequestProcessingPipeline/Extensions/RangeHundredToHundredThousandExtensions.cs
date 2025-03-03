using System;

public static class RangeHundredToHundredThousandExtensions
{
    public static IApplicationBuilder UseHundredToHundredThousand(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RangeHundredToHundredThousandMiddleware>();
    }
}