using System.IO;
using System.Linq;
using System.Reflection;
using DirectoryCleanup.Core.Result;

namespace DirectoryCleanup.InputParser
{
   public class InputParameters
   {
      public bool IsFullDelete { get; set; }

      public string RootPath { get; set; }

      public string ArchivePath { get; set; }

      public ReturnResult ParseArguments(string[] args)
      {
         RootPath = GetCurrentDirectory();

         if (args.Length == 0)
         {
            return new SuccessResult();
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
            else
            {
               string message = GetToolHelpMessage();

               return new FailResult(message);
            }
         }

         return new SuccessResult();
      }

      private string GetToolHelpMessage()
      {
         return "Unknown parameter.\n" +
                "DirectoryCleanup -path <path_to_files> -fullDelete -zip\n" +
                " - path: this parameter can be ommited. In that case current directory is taken as path.\n" +
                " -fullDelete: when this option is included, tool delete folders: package, dependency and artifacts\n" +
                " -zip: when this option is included tool create zip file out of folder/files on targeted path.\n";
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
