using System;
using System.Text;
using ChequeWriterLib;
using Xunit;

namespace ChequeWriterLibTests;

public class TextFunctionTests
{
    [Theory]
    [InlineData(1, "one", false)]
    [InlineData(100, "one hundred", true)]
    [InlineData(1000, "one thousand", true)]
    [InlineData(10000, "ten thousand", true)]
    [InlineData(100000, "one hundred thousand", true)]
    [InlineData(1000000, "one million", true)]
    [InlineData(10000000, "ten million", true)]
    [InlineData(100000000, "one hundred million", true)]
    [InlineData(1000000000, "one billion", true)]
    
    [InlineData(1000000001, "one billion and one", true)]
    [InlineData(1000010001, "one billion, ten thousand and one", true)]
    
    [InlineData(1001010101, "one billion, one million, ten thousand, one hundred and one", true)]
    
    [InlineData(90, "ninety", true)]
    [InlineData(80, "eighty", true)]
    [InlineData(70, "seventy", true)]
    [InlineData(60, "sixty", true)]
    [InlineData(50, "fifty", true)]
    [InlineData(40, "forty", true)]
    [InlineData(30, "thirty", true)]
    [InlineData(20, "twenty", true)]
    
    [InlineData(21, "twenty one", true)]
    
    [InlineData(19, "nineteen", true)]
    [InlineData(18, "eighteen", true)]
    [InlineData(17, "seventeen", true)]
    [InlineData(16, "sixteen", true)]
    [InlineData(15, "fifteen", true)]
    [InlineData(14, "fourteen", true)]
    [InlineData(13, "thirteen", true)]
    [InlineData(12, "twelve", true)]
    [InlineData(11, "eleven", true)]

    [InlineData(9, "nine", true)]
    [InlineData(8, "eight", true)]
    [InlineData(7, "seven", true)]
    [InlineData(6, "six", true)]
    [InlineData(5, "five", true)]
    [InlineData(4, "four", true)]
    [InlineData(3, "three", true)]
    [InlineData(2, "two", true)]
    
    [InlineData(0, "zero", true)]
    public void SpellIntInEnglish_ShouldBeCorrect(int number, string result, bool plural)
    {
        var got_result = TextFunctions.SpellIntInEnglish(
            new StringBuilder(), number, out var got_plural
        ).ToString();
        
        Assert.Equal(result, got_result);
        Assert.Equal(plural, got_plural);
    }

    [Theory]
    [InlineData(100.0, "one hundred DOLLARS")]
    [InlineData(1.0, "one DOLLAR")]
    [InlineData(0.0, "zero DOLLARS")]
    [InlineData(0.10, "ten CENTS")]
    [InlineData(0.01, "one CENT")]
    [InlineData(1.01, "one DOLLAR AND one CENT")]
    public void SpellDollarsAndCents_ShouldBeCorrect(decimal money, string spell)
    {
        var got_spell = TextFunctions.SpellDollarsAndCents(money);
        
        Assert.Equal(spell, got_spell);
    }

    [Fact]
    public void SpellDollarsAndCents_ShouldError_WhenMoneyMoreThan2Billions()
    {
        Assert.Throws<ArgumentOutOfRangeException>(()=>TextFunctions.SpellDollarsAndCents(2_000_000_001));
    }
    
    [Fact]
    public void SpellDollarsAndCents_ShouldError_WhenMoneyLessThanZero()
    {
        Assert.Throws<ArgumentOutOfRangeException>(()=>TextFunctions.SpellDollarsAndCents(-1));
    }
}