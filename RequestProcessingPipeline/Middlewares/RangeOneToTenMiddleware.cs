using System;

public class RangeOneToTenMiddleware
{
    private readonly RequestDelegate n;
    public RangeOneToTenMiddleware(RequestDelegate next)
    {
        n = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? value = context.Request.Query["number"];
        try
        {
            int number = Math.Abs(Convert.ToInt32(value));

            if (number == 0)
                await context.Response.WriteAsync("Incorrect parameter");
            else if (number == 10)
                await context.Response.WriteAsync("Your number is ten");
            else if (number <= 9)
            {
                string[] names = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                await context.Response.WriteAsync("Your number is " + names[number - 1]);
            }
            else
            {
                if (number > 20 && (number % 10) != 0)
                {
                    string[] shortNames = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
                    context.Session.SetString("lastDigitName", shortNames[(number % 10) - 1]);
                }
                await n(context);
            }
        }
        catch
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}