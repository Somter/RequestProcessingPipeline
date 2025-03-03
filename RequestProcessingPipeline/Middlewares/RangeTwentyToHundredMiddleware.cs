using System;

public class RangeTwentyToHundredMiddleware
{
    private readonly RequestDelegate _next;
    public RangeTwentyToHundredMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? queryValue = context.Request.Query["number"];

        try
        {
            int number = Math.Abs(Convert.ToInt32(queryValue));

            if (number < 20)
                await _next(context);
            else if (number > 100)
                await context.Response.WriteAsync("Number greater than one hundred");
            else if (number == 100)
                await context.Response.WriteAsync("Your number is one hundred");
            else
            {
                string[] multiplesOfTen = { "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number % 10 == 0)
                {
                    int index = number / 10 - 2;
                    await context.Response.WriteAsync("Your number is " + multiplesOfTen[index]);
                }
                else
                {
                    await _next(context);
                    string? lastDigitName = context.Session.GetString("lastDigitName");
                    int index = number / 10 - 2;
                    string tensPart = multiplesOfTen[index];
                    await context.Response.WriteAsync("Your number is " + tensPart + " " + lastDigitName);
                }
            }
        }
        catch
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }
}