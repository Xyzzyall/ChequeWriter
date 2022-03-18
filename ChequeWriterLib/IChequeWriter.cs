namespace ChequeWriterLib;

public interface IChequeWriter
{
    void WriteDollarsAndCentsInEnglish(decimal dollarsAndCents);
}