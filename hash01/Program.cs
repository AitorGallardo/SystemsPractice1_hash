using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace hash01
{
    // Path.GetFullpath("TextFile1.txt");
    class Program
    {
        String stringifiedTxtFileHash = "";
        string oldTxtFile = "TextFile1.txt";
        string newShaFile = "TextFile1.SHA";
        string newFilePath;
        bool enableCheckIntegrity = false;
        string userInput = "0";


        static void Main(string[] args)
        {
            Program theProgram = new Program();
            theProgram.start();



        }
        public void start()
        {
            try
            {
                menu();
                while (userInput != "3")
                {
                  userInput = Console.ReadLine();
                    switch (userInput)
                    {

                        case "1":
                            createHash();
                            break;
                        case "2":
                            checkIntegrity();
                            break;
                        case "3":
                            Console.WriteLine("Programa finalitzat");
                            break;
                    }
                     
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("{0} File ------->NOT<--------------- found", e);
            }

             Console.ReadKey();
        }

        string getDataFromFilePath(String fileName)
        {

            string route = Path.GetFullPath(fileName);
            string TextFileToHash = File.ReadAllText(route);

            return TextFileToHash;
        }

        byte[] hashFileData(string fileToHash)
        {

            byte[] bytesIn = Encoding.UTF8.GetBytes(getDataFromFilePath(fileToHash));
            SHA512Managed SHA512 = new SHA512Managed();
            byte[] hashResult = SHA512.ComputeHash(bytesIn);

            SHA512.Dispose();

            return hashResult;
        }

        void compareHashedBytes(string hashFromTxtFile, string hashFromShaFile)
        {
            bool equalHashes = false;
            if (hashFromShaFile.Length == hashFromTxtFile.Length)
            {
                int i = 0;
                while ((i < hashFromShaFile.Length) && (hashFromShaFile[i] == hashFromTxtFile[i]))
                {
                    i += 1;
                }
                if (i == hashFromShaFile.Length)
                {
                    equalHashes = true;
                }
            }

            if (equalHashes)
                Console.WriteLine("\nThe two hash values are the same");
            else
                Console.WriteLine("\nThe two hash values are not the same");
        }
        void menu()
        {
            Console.WriteLine("Premeu '1' per calcular el hash de l'archiu 'TextFile1.txt' i introduir-lo en un nou archiu anomenat 'TextFile1.SHA'");
            Console.WriteLine("Premeu '2' per comprobar l'integritat de les dades comparant els 2 archius");
            Console.WriteLine("Premeu '3' per finalitzar el programa");
        }


        void createHash()
        {
            stringifiedTxtFileHash = BitConverter.ToString(hashFileData(oldTxtFile)).Replace("-", string.Empty);
            File.WriteAllText(newShaFile, stringifiedTxtFileHash);
            Console.WriteLine("Hash del text {0}", getDataFromFilePath(oldTxtFile));
            Console.WriteLine(stringifiedTxtFileHash);
            newFilePath = Path.GetFullPath(newShaFile);
            Console.WriteLine("La ruta del nou archiu es:  {0}", newFilePath);
            enableCheckIntegrity = true;

        }
        void checkIntegrity()
        {
            if (enableCheckIntegrity == true)
            {
                Console.WriteLine("\nChecking file integrity....  ");
                Console.WriteLine("\nOriginal file hash:   {0}", stringifiedTxtFileHash);
                Console.WriteLine("\nOriginated file hash: {0}", getDataFromFilePath(newShaFile));
                compareHashedBytes(stringifiedTxtFileHash, getDataFromFilePath(newShaFile));
            }
            else
            {
                Console.WriteLine("\nPrimer has de crear un archiu per comprobar l'integritat. Premeu '1'per crear un archiu amb el hash resultant del primer");
            }
        }

    }

}
