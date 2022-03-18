using ChequeWriterLib;
using NSubstitute;
using Xunit;

namespace ChequeWriterLibTests;

public class ConsoleChequeWriterTests
{
    [Theory]
    [InlineData(100, "one hundred DOLLARS")]
    public void WriteDollarsAndCentsInEnglish_ShouldWriteCorrectText(decimal money, string expectedText)
    {
        var console = Substitute.For<IConsoleWriter>();
        var cheque_writer = new ConsoleChequeWriter(console);
        
        cheque_writer.WriteDollarsAndCentsInEnglish(money);
        
        console.Received().Write(expectedText);
    }
}