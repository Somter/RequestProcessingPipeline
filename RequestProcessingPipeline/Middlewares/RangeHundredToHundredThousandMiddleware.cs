using System;

public class RangeHundredToHundredThousandMiddleware
{
    private readonly RequestDelegate n;
    public RangeHundredToHundredThousandMiddleware(RequestDelegate next)
    {
        n = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? value = context.Request.Query["number"];
        try
        {
            int number = Math.Abs(Convert.ToInt32(value));

            if (number <= 100)
                await n(context);
            else if (number > 100000)
                await context.Response.WriteAsync("Number greater than one hundred thousand");
            else
            {
                string words = ConvertNumberToWords(number);
                await context.Response.WriteAsync($"Your number is {words}");
            }
        }
        catch
        {
            await context.Response.WriteAsync("Incorrect parameter");
        }
    }

    private string ConvertNumberToWords(int number)
    {
        if (number == 100000)
            return "one hundred thousand";
        string[] units = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        string[] tens = { "", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
        string[] teens = { "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        List<string> resultParts = new List<string>();
        if (number >= 1000)
        {
            int thousandsPart = number / 1000;
            number %= 1000;
            if (thousandsPart == 10)
                resultParts.Add("ten thousand");
            else
                resultParts.Add(ConvertNumberToWords(thousandsPart) + " thousand");
        }

        if (number >= 100)
        {
            int hundredsPart = number / 100;
            number %= 100;
            resultParts.Add(units[hundredsPart] + " hundred");
        }

        if (number >= 11 && number <= 19)
            resultParts.Add(teens[number - 11]);
        else
        {
            if (number >= 10)
            {
                int t = number / 10;
                number %= 10;
                resultParts.Add(tens[t]);
            }

            if (number > 0 && number < 10)
                resultParts.Add(units[number]);
        }
        return string.Join(" ", resultParts).Trim();
    }
}