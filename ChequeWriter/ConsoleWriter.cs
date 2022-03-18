using ChequeWriterLib;

namespace Cheque_Writer;

internal class ConsoleWriter : IConsoleWriter
{
    public void Write(object? o)
    {
        Console.WriteLine($"CHEQUE: {o}.");
    }
}