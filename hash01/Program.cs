using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace hash01
{
    // Path.GetFullpath("TextFile1.txt");
    class Program
    {

        static void Main(string[] args)
        {
            string route;
            string TextFileToHash;
            string outputPath = "TextFile1.SHA";
            string outputRoute;

            try
            {

                route = Path.GetFullPath("TextFile1.txt");
                TextFileToHash = File.ReadAllText(route);
                byte[] bytesIn = Encoding.UTF8.GetBytes(TextFileToHash);
                SHA512Managed SHA512 = new SHA512Managed();
                byte[] hashResult = SHA512.ComputeHash(bytesIn);

                String textOut = BitConverter.ToString(hashResult).Replace("-", string.Empty);
                File.WriteAllText(outputPath, textOut);
                Console.WriteLine("Hash del text {0}", TextFileToHash);
                Console.WriteLine(textOut);
                outputRoute = Path.GetFullPath(outputPath);
                Console.WriteLine("La ruta del nou archiu es:  {0}", outputRoute);


                // Eliminem la classe instanciada
                SHA512.Dispose();
            }
            catch (FileNotFoundException e)
            {
                // FileNotFoundException
                Console.WriteLine("{0} File ------->NOT<--------------- found", e);
            }

            Console.ReadKey();




        }
    }
}
