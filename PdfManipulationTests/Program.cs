using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfManipulationTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the PDF manipulation test project.");
            Console.WriteLine();
            Console.WriteLine("Select from one of the options below to run the specific test");

            Console.WriteLine();
            Console.WriteLine("[to exit, press ctrl + C]");

            Console.WriteLine();
            Console.WriteLine("1 - Add password protection to a PDF file");

            Console.WriteLine();

            var key = Console.ReadKey(true);

            Console.WriteLine();

            string[] validKeys = { "1" };

            if (!validKeys.Contains(key.KeyChar.ToString()))
            {
                Console.WriteLine("Sorry, you must select one of the options shown");
                Console.WriteLine();
            }

            switch (key.KeyChar.ToString())
            {
                case ("1"):
                    // run the password protect test
                    PasswordProtectPdf.PasswordProtection.RunPasswordProtectTest();
                    break;
            }
        }

              
    }
}
