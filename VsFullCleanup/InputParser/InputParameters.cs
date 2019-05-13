using System.IO;
using System.Linq;
using System.Reflection;

namespace VsFullCleanup.InputParser
{
   public class InputParameters
   {
      public string RootPath { get; set; }

      public bool IsFullDelete { get; set; }

      public bool IsZipping { get; set; }

      public InputParameters(string[] args)
      {
         ParseArguments(args);
      }

      private void ParseArguments(string[] args)
      {
         RootPath = GetCurrentDirectory();

         foreach (string arg in args)
         {
            if (arg == "-path")
            {
               string filePath = args.SkipWhile(x => x.StartsWith("-")).FirstOrDefault();

               if (filePath != null)
               {
                  RootPath = new DirectoryInfo(filePath).FullName;
               }
            }
            else if (arg == "-fullDelete")
            {
               IsFullDelete = true;
            }
            else if (arg == "-zip")
            {
               IsZipping = true;
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
