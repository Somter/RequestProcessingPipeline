using System;

public class RangeElevenToNineteenMiddleware
{
    private readonly RequestDelegate n;
    public RangeElevenToNineteenMiddleware(RequestDelegate next)
    {
        n = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? value = context.Request.Query["number"];
        try
        {
            int number = Math.Abs(Convert.ToInt32(value));
            if (number < 11 || number > 19)
                await n(context);
            else
            {
                string[] group =
                {
                        "eleven", "twelve", "thirteen", "fourteen", "fifteen",
                        "sixteen", "seventeen", "eighteen", "nineteen"
                };

                await context.Response.WriteAsync("Your number is " + group[number - 11]);
            }
        }
        catch
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}