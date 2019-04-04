using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VsFullCleanup.Core;
using VsFullCleanup.Core.Models;
using VsFullCleanup.Core.Result;
using VsFullCleanup.Extensions;
using VsFullCleanup.Helpers;
using VsFullCleanup.Logger;

namespace VsFullCleanup
{
   class Program
   {
      static void Main(string[] args)
      {
         var logger = new SimpleLogger(@"BinObjLog.txt");
         IFileSystemItemsProvider fileSystemItemsProvider = new FileSystemItemsProvider(logger);
         string directory = args.Length == 0 ? GetCurrentDirectory() : new DirectoryInfo(args[0]).FullName;

         Console.WriteLine($"Doing full clean-up of visual studio projects from directory:");
         Console.WriteLine($" - {directory}\n");

         bool isDeleting = ConsoleUtils.Confirm("Are you sure?");

         if (isDeleting)
         {
            try
            {
               List<FileSystemItem> fileSystemItems = fileSystemItemsProvider.GetItems(directory).MakeFlat();

               AddBackslashOnDirectoryPaths(fileSystemItems);

               List<FileSystemItem> binObjDirectories = fileSystemItems
                  .Where(x => x.Path.Contains(@"\bin\") || x.Path.Contains(@"\obj\")).ToList();
               List<FileSystemItem> vsUserFiles = fileSystemItems.Where(x => x.Path.Contains(@".user")).ToList();
               List<FileSystemItem> vsFiles = fileSystemItems.Where(x => x.Path.Contains(@".vs\")).ToList();

               ReturnResult resultBinObj = fileSystemItemsProvider.DeleteItems(binObjDirectories);
               ReturnResult resultUser = fileSystemItemsProvider.DeleteItems(vsUserFiles);
               ReturnResult resultVsFiles = fileSystemItemsProvider.DeleteItems(vsFiles);

               Console.WriteLine($"Removing bin / obj directories.");
               Console.WriteLine($" - {resultBinObj.Message}");

               Console.WriteLine($"Removing user files.");
               Console.WriteLine($" - {resultUser.Message}");

               Console.WriteLine($"Removing vs directory.");
               Console.WriteLine($" - {resultVsFiles.Message}");

               Console.WriteLine("All is clean!");
            }
            catch (Exception exception)
            {
               Console.WriteLine($"Something went wrong: {exception.Message}");
            }
         }

         Console.ReadKey();
      }

      private static string GetCurrentDirectory()
      {
         string assemblyLocation = Assembly.GetExecutingAssembly().Location;
         var directory = Path.GetDirectoryName(assemblyLocation);
         var directoryInfo = new DirectoryInfo(directory);

         return directoryInfo.FullName;
      }

      private static void AddBackslashOnDirectoryPaths(List<FileSystemItem> fileSystemItems)
      {
         foreach (FileSystemItem fileSystemItem in fileSystemItems)
         {
            if (!Path.HasExtension(fileSystemItem.Path))
            {
               fileSystemItem.Path += @"\";
            }
         }
      }
   }
}
