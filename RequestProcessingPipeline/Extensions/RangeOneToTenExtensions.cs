using System;

public static class RangeOneToTenExtensions
{
    public static IApplicationBuilder UseOneToTen(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RangeOneToTenMiddleware>();
    }
}