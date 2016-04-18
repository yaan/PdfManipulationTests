using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfManipulationTests.PasswordProtectPdf
{
    public class PasswordProtection
    {
        #region Public methods

        public static void RunPasswordProtectTest()
        {
            Console.WriteLine("Enter a password to use for the test (password strength not important so it can be anything");
            Console.WriteLine();

            var password = Console.ReadLine();
            Console.WriteLine();

            if (!IsPasswordValid(password))
            {
                Console.WriteLine("You must enter SOMETHING for the password...");
                Console.WriteLine();

                password = Console.ReadLine();
                Console.WriteLine();

                if (!IsPasswordValid(password))
                {
                    Console.WriteLine("Ok, one more try... this time I'll quit if you don't enter a password!");
                    Console.WriteLine();

                    password = Console.ReadLine();
                    Console.WriteLine();

                    if (!IsPasswordValid(password))
                    {
                        Console.WriteLine("forget it!");
                        return;
                    }
                }
            }

            Console.WriteLine("Now enter the location of a PDF file to encrypt (don't worry, this will create a copy and not overwrite the original)");
            Console.WriteLine();

            var filePath = Console.ReadLine();
            Console.WriteLine();

            if (!IsFilePathValid(filePath))
            {
                Console.WriteLine("The file path entered is not valid or the file doesn't exist, please try again...");
                Console.WriteLine();

                filePath = Console.ReadLine();
                Console.WriteLine();

                if (!IsFilePathValid(filePath))
                {
                    Console.WriteLine("The file path is STILL not valid, one more go or I'm taking my ball home...");
                    Console.WriteLine();

                    filePath = Console.ReadLine();
                    Console.WriteLine();

                    if (!IsFilePathValid(filePath))
                    {
                        Console.WriteLine("forget it, I'm off!");
                        return;
                    }
                }
            }

            Console.WriteLine(string.Format("You chose: {0}; {1}", password, filePath));
            Console.ReadKey();

            Console.WriteLine("Creating a password protected copy...");
            Console.WriteLine();

            var protectedFileName = filePath.Replace(".pdf", "-protected.pdf");

            byte[] userPwd = Encoding.ASCII.GetBytes(password);
            byte[] ownerPwd = Encoding.ASCII.GetBytes(password);

            PdfReader reader = new PdfReader(filePath);

            PdfStamper stamper = new PdfStamper(reader, new FileStream(protectedFileName, FileMode.Create));

            stamper.SetEncryption(userPwd, ownerPwd, PdfWriter.AllowPrinting, PdfWriter.ENCRYPTION_AES_128);
            stamper.Close();

            Console.WriteLine(string.Format("All done, you should now have a protected file called {0}", protectedFileName));
            Console.WriteLine();

            Console.WriteLine("Press 1 to open the folder, or press any other key to exit");

            var pressedKey = Console.ReadKey();

            switch (pressedKey.KeyChar.ToString())
            {
                case ("1"):
                    OpenDirectory(protectedFileName);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Private methods

        private static bool IsPasswordValid(string password)
        {
            return (string.IsNullOrEmpty(password)) ? false : true;
        }

        private static bool IsFilePathValid(string filePath)
        {
            return (string.IsNullOrEmpty(filePath) || !File.Exists(filePath) || !filePath.EndsWith(".pdf")) ? false : true;
        }

        private static void OpenDirectory(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                ProcessStartInfo process = new ProcessStartInfo("explorer", "/select," + filePath);
                process.UseShellExecute = true;

                Process.Start(process);
            }
        }

        #endregion
    }
}
