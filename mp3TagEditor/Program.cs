using System;
using TagLib;
using System.IO;

namespace mp3TagEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            /*File file = File.Create("C:/Users/Vidal Olmedo/Desktop/pruebasmp3/Alien Nation.mp3");

            //file.Tag.Title = "";

            file.Save();

            Console.WriteLine($"AUTOR: {file.Tag.Performers[0]}");
            Console.WriteLine($"TITLE: {file.Tag.Title}");

            string input = Console.ReadLine();

            Console.WriteLine(input);
            Console.WriteLine($"LENGTH: {input.Length}");*/



            //args[0] = source folder (as copied from the route in the file explorer)
            //args[1] = destination folder

            /*for(int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }*/

            string[] files = Directory.GetFiles(args[0]);
            int pathLength = args[0].Length + 1; //+1 for the ending slash

            string fileName;
            int extensionIndex;

            for(int i = 0; i < files.Length; i++)
            {
                fileName = files[i].Substring(pathLength);

                extensionIndex = fileName.LastIndexOf(".");

                fileName = fileName.Remove(extensionIndex);

                Console.WriteLine($"*NOMBRE* {fileName}");
            }


        }
    }
}
