using System;

public static class RangeElevenToNineteenExtensions
{
    public static IApplicationBuilder UseElevenToNineteen(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RangeElevenToNineteenMiddleware>();
    }
}