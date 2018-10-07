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
            // string route;
            // string TextFileToHash;
            string oldTxtFile = "TextFile1.txt";
            string newShaFile = "TextFile1.SHA";
            string newFilePath;

            string getDataFromFilePath(String fileName){

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

            void compareHashedBytes(byte[] hashFromTxtFile, byte[] hashFromShaFile)
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
                Console.ReadLine();
            }

            try
            {
                String stringifiedTxtFileHash = BitConverter.ToString(hashFileData(oldTxtFile)).Replace("-", string.Empty); //.Replace("-", string.Empty)
                File.WriteAllText(newShaFile, stringifiedTxtFileHash);
                Console.WriteLine("Hash del text {0}", getDataFromFilePath(oldTxtFile));
                Console.WriteLine(stringifiedTxtFileHash);
                newFilePath = Path.GetFullPath(newShaFile);
                Console.WriteLine("La ruta del nou archiu es:  {0}", newFilePath);
                
                Console.WriteLine("\nChecking file integrity....  ");
                byte[] originalHash = hashFileData(oldTxtFile);
                byte[] newOriginatedHash = Encoding.UTF8.GetBytes(getDataFromFilePath(newShaFile));
                Console.WriteLine("\nOriginal file hash:   {0}", BitConverter.ToString(originalHash).Replace("-", string.Empty));
                Console.WriteLine("\nOriginated file hash: {0}", BitConverter.ToString(newOriginatedHash).Replace("-", string.Empty));
                compareHashedBytes(originalHash, newOriginatedHash);


            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("{0} File ------->NOT<--------------- found", e);
            }

            Console.ReadKey();




        }
    }
}
