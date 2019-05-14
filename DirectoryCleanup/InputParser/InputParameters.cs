using System.IO;
using System.Linq;
using System.Reflection;

namespace DirectoryCleanup.InputParser
{
   public class InputParameters
   {
      public bool IsFullDelete { get; set; }

      public string RootPath { get; set; }

      public string ArchivePath { get; set; }

      public InputParameters(string[] args)
      {
         ParseArguments(args);
      }

      private void ParseArguments(string[] args)
      {
         RootPath = GetCurrentDirectory();

         if (args.Length == 0)
         {
            return;
         }

         foreach (string arg in args)
         {
            if (arg == "-fullDelete")
            {
               IsFullDelete = true;
            }
            else if (arg == "-path")
            {
               string filePath = args.SkipWhile(x => x.StartsWith("-path")).FirstOrDefault();

               if (filePath != null && !filePath.StartsWith("-"))
               {
                  RootPath = new DirectoryInfo(filePath).FullName;
               }
            }
            else if (arg == "-zip")
            {
               string archivePath = args.SkipWhile(x => x.StartsWith("-zip")).FirstOrDefault();

               if (archivePath != null && !archivePath.StartsWith("-"))
               {
                  ArchivePath = new FileInfo(archivePath).FullName;
               }
            }
         }
      }

      private string GetCurrentDirectory()
      {
         string assemblyLocation = Assembly.GetExecutingAssembly().Location;
         var directory = Path.GetDirectoryName(assemblyLocation);
         var directoryInfo = new DirectoryInfo(directory);

         return directoryInfo.FullName;
      }
   }
}
