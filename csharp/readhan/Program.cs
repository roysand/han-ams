using System;

namespace readhan
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadHan reader = new ReadHan();

            reader.Read();
            Console.WriteLine("Program ends");
        }
    }
}