using System;
//using TagLib;
using System.IO;

namespace mp3TagEditor
{
    class Program
    {
        static void Main(string[] paths)
        {
            //args[0] = source folder (as copied from the route in the file explorer)
            //args[1] = destination folder

            if(paths.Length == 0)
            {
                paths = new string[2];
                paths[0] = "C:/Users/Vidal Olmedo/Music/Music"; //source
                paths[1] = "C:/Users/Vidal Olmedo/Music/MUSIC LISTOS";  //destination
            }

            string[] files = Directory.GetFiles(paths[0]);
            int pathLength = paths[0].Length + 1; //+1 for the ending slash

            string fileName, newName, performer, newPerformer, extension;
            int extensionIndex;

            TagLib.File file;

            for(int i = 0; i < files.Length; i++)
            {
                Console.Clear();

                fileName = files[i].Substring(pathLength);
                extensionIndex = fileName.LastIndexOf(".");
                extension = fileName.Substring(extensionIndex + 1);

                if(extension != "mp3")
                {
                    continue;
                }

                fileName = fileName.Remove(extensionIndex);

                file = TagLib.File.Create(files[i]);


                if(file.Tag.Performers.Length == 0)
                {
                    performer = "-";
                }
                else
                {
                    performer = file.Tag.Performers[0];
                }

                Console.WriteLine($"*NOMBRE* {fileName} *ARTISTA* {performer}");

                Console.Write("Nombre: ");
                newName = Console.ReadLine();

                if(newName == "")
                {
                    newName = fileName;
                }

                Console.Write("Artista: ");
                GetPerformer();

                string[] performersArray = { newPerformer };

                file.Tag.Performers = performersArray;

                file.Save();

                Directory.Move(files[i], paths[1] + "/" + newName + ".mp3");
            }

            Console.Clear();
            Console.WriteLine("No hay mas archivos");

            void GetPerformer()
            {                
                newPerformer = Console.ReadLine();

                if (newPerformer == "")
                {
                    if (performer == "-")
                    {
                        GetPerformer();
                    }
                    else
                    {
                        newPerformer = performer;
                    }                 
                }
            }
        }
    }
}
