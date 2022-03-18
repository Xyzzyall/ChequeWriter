// See https://aka.ms/new-console-template for more information

using System.Text;
using Cheque_Writer;
using ChequeWriterLib;

var cheque_writer = new ConsoleChequeWriter(new ConsoleWriter());

while (true)
{
    Console.Write("Enter sum: ");
    if (!decimal.TryParse(Console.ReadLine(), out var money))
    {
        Console.WriteLine("Incorrect input. Try again!");
        continue;
    }

    if (Math.Round(money, 2) != money)
    {
        Console.WriteLine("Cent fractions were truncated.");
    }
    
    cheque_writer.WriteDollarsAndCentsInEnglish(money);
}