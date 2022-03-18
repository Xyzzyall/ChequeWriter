using System.Diagnostics;
using System.Text;

namespace ChequeWriterLib;

public static class TextFunctions
{
    public static string SpellDollarsAndCents(decimal dollarsAndCents)
    {
        switch (dollarsAndCents)
        {
            case > 2_000_000_000:
                throw new ArgumentOutOfRangeException(nameof(dollarsAndCents), "cannot be more than two billion");
            case < 0:
                throw new ArgumentOutOfRangeException(nameof(dollarsAndCents), "cannot be less than zero");
        }
        
        var dollars = (int)Math.Truncate(dollarsAndCents);
        var cents = (int)Math.Truncate((dollarsAndCents - dollars) * 100);
        
        var dollars_builder = new StringBuilder();
        SpellIntInEnglish(dollars_builder, dollars, out var dollars_plural);
        dollars_builder.Append(dollars_plural ? " DOLLARS" : " DOLLAR");
        if (cents == 0) return dollars_builder.ToString();

        var cents_builder = new StringBuilder();
        SpellIntInEnglish(cents_builder, cents, out var cents_plural);
        cents_builder.Append(cents_plural ? " CENTS" : " CENT");
        if (dollars == 0) return cents_builder.ToString();

        return dollars_builder.Append(" AND ").Append(cents_builder).ToString();
    }
    
    public static StringBuilder SpellIntInEnglish(StringBuilder builder, int number, out bool plural)
    {
        const int oneBillion = 1000_000_000;
        const int oneMillion = 1000_000;
        const int oneThousand = 1000;
        const int oneHundred = 100;
        switch (number)
        {
            case >= oneBillion:
            {
                SpellIntInEnglish(builder, number / oneBillion, out _);
                builder.Append(" billion");
                plural = true;
                
                if (number % oneBillion == 0) return builder;
                builder.Append(number % oneBillion < oneHundred ? " and " : ", ");
                
                return SpellIntInEnglish(
                    builder,
                    number - number / oneBillion * oneBillion,
                    out _
                );
            }
            case >= oneMillion:
            {
                SpellIntInEnglish(builder, number / oneMillion, out _);
                builder.Append(" million");
                plural = true;
                
                if (number % oneMillion == 0) return builder;
                builder.Append(number % oneMillion < oneHundred ? " and " : ", ");
                
                return SpellIntInEnglish(
                    builder,
                    number - number / oneMillion * oneMillion,
                    out _
                );
            }
            case >= oneThousand:
            {
                SpellIntInEnglish(builder, number / oneThousand, out _);
                builder.Append(" thousand");
                plural = true;

                if (number % oneThousand == 0) return builder;
                builder.Append(number % oneThousand < oneHundred ? " and " : ", ");

                return SpellIntInEnglish(
                    builder,
                    number - number / oneThousand * oneThousand,
                    out _
                );
            }
            case >= oneHundred:
            {
                TensAndLess(builder, number / oneHundred, out _);
                builder.Append(" hundred");
                plural = true;
                
                if (number % oneHundred == 0) return builder;
                if (number % oneHundred > 0) builder.Append(" and ");
                
                TensAndLess(builder, number - number / oneHundred * oneHundred, out _);
                return builder;
            }
            default:
            {
                TensAndLess(builder, number, out plural);
                return builder;
            }
        }
    }

    private static void TensAndLess(StringBuilder builder, int number, out bool plural)
    {
        Debug.Assert(number < 100);
        
        plural = true;
        var less_than_ten = number;
        
        switch (number / 10 * 10)
        {
            case 90:
                less_than_ten %= 10;
                builder.Append("ninety");
                break;
            case 80: 
                less_than_ten %= 10;
                builder.Append("eighty");
                break;
            case 70:
                less_than_ten %= 10;
                builder.Append("seventy");
                break;
            case 60:
                less_than_ten %= 10;
                builder.Append("sixty");
                break;
            case 50: 
                less_than_ten %= 10;
                builder.Append("fifty");
                break;
            case 40: 
                less_than_ten %= 10;
                builder.Append("forty");
                break;
            case 30:
                less_than_ten %= 10;
                builder.Append("thirty");
                break;
            case 20:
                less_than_ten %= 10;
                builder.Append("twenty");
                break;
        }

        if (less_than_ten == 0 && number != 0) return;
        
        if (less_than_ten != number)
            builder.Append(' ');

        switch (less_than_ten)
        {
            case 19: builder.Append("nineteen"); return;
            case 18: builder.Append("eighteen"); return;
            case 17: builder.Append("seventeen"); return;
            case 16: builder.Append("sixteen"); return;
            case 15: builder.Append("fifteen"); return;
            case 14: builder.Append("fourteen"); return;
            case 13: builder.Append("thirteen"); return;
            case 12: builder.Append("twelve"); return;
            case 11: builder.Append("eleven"); return;
            case 10: builder.Append("ten"); return;
            case 9: builder.Append("nine"); return;
            case 8: builder.Append("eight"); return;
            case 7: builder.Append("seven"); return;
            case 6: builder.Append("six"); return;
            case 5: builder.Append("five"); return;
            case 4: builder.Append("four"); return;
            case 3: builder.Append("three"); return;
            case 2: builder.Append("two"); return;
            case 1: 
                plural = number != 1;
                builder.Append("one"); 
                return;
        }
        
        builder.Append("zero");
    }
}