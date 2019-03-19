using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using VsFullCleanup.Core;
using VsFullCleanup.Core.Models;
using VsFullCleanup.Core.Result;
using VsFullCleanup.Extensions;
using VsFullCleanup.Logger;

namespace VsFullCleanup
{
   class Program
   {
      static void Main(string[] args)
      {
         var logger = new SimpleLogger(@"BinObjLog.txt");
         string assemblyLocation = Assembly.GetExecutingAssembly().Location;
         var directory = Path.GetDirectoryName(assemblyLocation);
         var directoryInfo = new DirectoryInfo(directory);
         IFileSystemItemsProvider fileSystemItemsProvider = new FileSystemItemsProvider(logger);

         Console.WriteLine($"Doing full clean-up of visual studio projects from directory:");
         Console.WriteLine($" - {directoryInfo.FullName}");

         try
         {
            List<FileSystemItem> fileSystemItems = fileSystemItemsProvider.GetItems(directoryInfo.FullName).MakeFlat();

            AddBackslashOnDirectoryPaths(fileSystemItems);

            List<FileSystemItem> binObjDirectories =
               fileSystemItems.Where(x => x.Path.Contains(@"\bin\") || x.Path.Contains(@"\obj\")).ToList();
            List<FileSystemItem> vsUserFiles = fileSystemItems.Where(x => x.Path.Contains(@".user")).ToList();

            ReturnResult resultBinObj = fileSystemItemsProvider.DeleteItems(binObjDirectories);
            ReturnResult resultUser = fileSystemItemsProvider.DeleteItems(vsUserFiles);

            Console.WriteLine($"Removing bin / obj directories.");
            Console.WriteLine($" - {resultBinObj.Message}");

            Console.WriteLine($"Removing user files.");
            Console.WriteLine($" - {resultUser.Message}");

            Console.WriteLine("All is clean!");
         }
         catch (Exception exception)
         {
            Console.WriteLine($"Something went wrong: {exception.Message}");
         }
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
