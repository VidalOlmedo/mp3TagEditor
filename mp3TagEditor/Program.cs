using System;
//using TagLib; https://github.com/mono/taglib-sharp
using System.IO;

namespace mp3TagEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            //RemoveAlbumArtist();
            EditFiles(args);
        }

        private static void RemoveAlbumArtist()
        {
            string path = "C:/Users/Vidal Olmedo/Music/Chidas nuevas (listas)";

            string[] files = Directory.GetFiles(path);

            TagLib.File file;

            int count = 0;

            for(int i = 0; i < files.Length; i++)
            {
                file = TagLib.File.Create(files[i]);
                //Console.WriteLine($"{file.Tag.AlbumArtists[0]}");

                if(file.Tag.AlbumArtists.Length > 0)
                {
                    file.Tag.AlbumArtists = new string[0];
                }

                if(file.Tag.Title != "")
                {
                    file.Tag.Title = "";
                }

                if(file.Tag.AlbumArtists.Length > 1)
                {
                    count++;
                }

                file.Save();
            }

            Console.WriteLine($"COUNT: {count}");
            Console.ReadLine(); //Pause
        }

        private static void EditFiles(string[] paths)
        {
            //paths[0] = source folder (as copied from the route in the file explorer)
            //paths[1] = destination folder

            if (paths.Length == 0)
            {
                paths = new string[2];
                /*paths[0] = "C:/Users/Vidal Olmedo/Music/Music"; //source
                paths[1] = "C:/Users/Vidal Olmedo/Music/MUSIC LISTOS";  //destination*/

                paths[0] = "C:/Users/Vidal Olmedo/Music/Chidas nuevas (descargadas)"; //source
                paths[1] = "C:/Users/Vidal Olmedo/Music/Chidas nuevas (listas)";  //destination
            }

            string[] files = Directory.GetFiles(paths[0]);
            int pathLength = paths[0].Length + 1; //+1 for the ending slash

            string fileName, newName, performer, newPerformer, extension;
            int extensionIndex;

            TagLib.File file;

            for (int i = 0; i < files.Length; i++)
            {
                Console.Clear();

                fileName = files[i].Substring(pathLength);
                extensionIndex = fileName.LastIndexOf(".");
                extension = fileName.Substring(extensionIndex + 1);

                if (extension != "mp3")
                {
                    continue;
                }

                fileName = fileName.Remove(extensionIndex);

                file = TagLib.File.Create(files[i]);


                if (file.Tag.Performers.Length == 0)
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

                if (newName == ".")
                {
                    continue;
                }
                else if (newName == "")
                {
                    newName = fileName;
                }

                Console.Write("Artista: ");
                GetPerformer();

                if (newPerformer == ".")
                {
                    continue;
                }

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
