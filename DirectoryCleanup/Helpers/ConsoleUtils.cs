using DirectoryCleanup.InputParser;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DirectoryCleanup.Helpers
{
    public static class ConsoleUtils
    {
        public static bool AskQuestion(string directory, ItemsToDelete itemsToDelete)
        {
            List<string> rootBinDirectoryPaths = itemsToDelete.BinDirectoryItemList?.Where(x => x.Path.EndsWith(@"\bin\")).Select(x => x.Path).ToList();
            List<string> rootObjDirectoryPaths = itemsToDelete.ObjDirectoryItemList?.Where(x => x.Path.EndsWith(@"\obj\")).Select(x => x.Path).ToList();
            List<string> rootVsDirectoryPaths = itemsToDelete.VsDirectoryItemList?.Where(x => x.Path.EndsWith(@"\.vs")).Select(x => x.Path).ToList();
            List<string> vsUserFilePaths = itemsToDelete.VsUserFileList?.Select(x => x.Path).ToList();
            List<string> packagesDirectoryPaths = itemsToDelete.PackageDirectoryItemList?.Where(x => x.Path.EndsWith(@"\packages\")).Select(x => x.Path).ToList();

            Console.WriteLine("Doing clean-up of visual studio projects from directory:");
            Console.WriteLine($"{directory}\n");

            DisplayDirectoriesForDelete("Removing bin directories:", directory, rootBinDirectoryPaths);
            DisplayDirectoriesForDelete("Removing obj directories:", directory, rootObjDirectoryPaths);
            DisplayDirectoriesForDelete("Removing VS directories:", directory, rootVsDirectoryPaths);
            DisplayDirectoriesForDelete("Removing visual studio user files:", directory, vsUserFilePaths);
            DisplayDirectoriesForDelete("Removing packages directories:", directory, packagesDirectoryPaths);

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