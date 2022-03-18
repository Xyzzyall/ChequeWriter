namespace ChequeWriterLib;

public class ConsoleChequeWriter : IChequeWriter
{
    private readonly IConsoleWriter _consoleWriter;
    public ConsoleChequeWriter(IConsoleWriter consoleWriter)
    {
        _consoleWriter = consoleWriter;
    }
    
    public void WriteDollarsAndCentsInEnglish(decimal dollarsAndCents)
    {
        _consoleWriter.Write(TextFunctions.SpellDollarsAndCents(dollarsAndCents));
    }
}

public interface IConsoleWriter
{
    void Write(object? o);
}