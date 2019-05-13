using System;
using System.Linq;
using System.Collections.Generic;

namespace VsFullCleanup.Helpers
{
   public class ConsoleUtils
   {
      public static bool AskQuestion(string directory, ItemsToDelete itemsToDelete)
      {
         //VisualStudio
         string dependenciesDirectoryPath = itemsToDelete.ItemsInDependenciesDirectory.FirstOrDefault(x => x.Path.EndsWith(@"\dependencies\"))?.Path;
         string packagesDirectoryPath = itemsToDelete.ItemsInPackageDirectory.FirstOrDefault(x => x.Path.EndsWith(@"\packages\"))?.Path;
         List<string> rootObjDirectoryPaths = itemsToDelete.ItemsInObjDirectory.Where(x => x.Path.EndsWith(@"\obj\")).Select(x => x.Path).ToList();
         List<string> rootBinDirectoryPaths = itemsToDelete.ItemsInBinDirectory.Where(x => x.Path.EndsWith(@"\bin\")).Select(x => x.Path).ToList();
         List<string> rootVsDirectoryPaths = itemsToDelete.ItemsInVsDirectory.Where(x => x.Path.EndsWith(@".vs\")).Select(x => x.Path).ToList();
         string visualStudioUserFile = itemsToDelete.VsUserFile?.Path;
         //CI
         string artifactsDirectoryPath = itemsToDelete.ItemsInArtifactsDirectory.FirstOrDefault(x => x.Path.EndsWith(@"\artifacts\"))?.Path;
         string deploymentDirectoryPath = itemsToDelete.ItemsInDeploymentDirectory.FirstOrDefault(x => x.Path.EndsWith(@"\deployment\"))?.Path;

         Console.WriteLine("Doing clean-up of visual studio projects from directory:");
         Console.WriteLine($"{directory}\n");

         //VisualStudio
         DisplayDirectoryForDelete("Removing dependency directory:", directory, dependenciesDirectoryPath);
         DisplayDirectoryForDelete("Removing packages directory:", directory, packagesDirectoryPath);
         DisplayDirectoriesForDelete("Removing obj directories:", directory, rootObjDirectoryPaths);
         DisplayDirectoriesForDelete("Removing bin directories:", directory, rootBinDirectoryPaths);
         DisplayDirectoriesForDelete("Removing VS directories:", directory, rootVsDirectoryPaths);
         DisplayDirectoryForDelete("Removing visual studio user file:", directory, visualStudioUserFile);
         //CI process
         DisplayDirectoryForDelete("Removing artifacts directory:", directory, artifactsDirectoryPath);
         DisplayDirectoryForDelete("Removing deployment directory:", directory, deploymentDirectoryPath);

         bool isDeleting = Confirm("Are you sure?");

         return isDeleting;
      }

      private static void DisplayDirectoriesForDelete(string title, string rootPath, List<string> rootDirectoryPaths)
      {
         if (rootDirectoryPaths.Count > 0)
         {
            Console.WriteLine(title);

            foreach (string rootDirectoryPath in rootDirectoryPaths)
            {
               string subPath = rootDirectoryPath.Replace(rootPath, "");

               Console.WriteLine($"   + {subPath}");
            }

            Console.WriteLine();
         }
      }

      private static void DisplayDirectoryForDelete(string title, string rootPath, string rootDirectoryPath)
      {
         if (!string.IsNullOrEmpty(rootDirectoryPath))
         {
            string subPath = rootDirectoryPath.Replace(rootPath, "");

            Console.WriteLine(title);
            Console.WriteLine($"   + {subPath}");
            Console.WriteLine();
         }
      }

      private static bool Confirm(string title)
      {
         ConsoleKey response;

         do
         {
            Console.Write($"{ title } [y/n] ");

            response = Console.ReadKey(false).Key;

            if (response != ConsoleKey.Enter)
            {
               Console.WriteLine();
            }
         } while (response != ConsoleKey.Y && response != ConsoleKey.N);

         return (response == ConsoleKey.Y);
      }
   }
}
