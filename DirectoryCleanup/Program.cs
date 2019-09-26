using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using DirectoryCleanup.Core;
using DirectoryCleanup.Core.Models;
using DirectoryCleanup.Core.Result;
using DirectoryCleanup.Extensions;
using DirectoryCleanup.Helpers;
using DirectoryCleanup.InputParser;
using DirectoryCleanup.Logger;

namespace DirectoryCleanup
{
   class Program
   {
      static void Main(string[] args)
      {
         var logger = new SimpleLogger(@"DirectoryCleanupLog.txt");
         var itemsProvider = new FileSystemItemsProvider(logger);
         var inputParameters = new InputParameters();

         ReturnResult result = inputParameters.ParseArguments(args);

         if (!result.IsSuccess)
         {
            Console.WriteLine(result.Message);
            return;
         }

         try
         {
            List<FileSystemItem> fileSystemItems = itemsProvider.GetItems(inputParameters.RootPath).MakeFlat().AddBackslashOnDirectoryPaths();
            ItemsToDelete itemsToDelete = new ItemsToDelete(fileSystemItems, inputParameters);

            bool isDeleting = ConsoleUtils.AskQuestion(inputParameters.RootPath, itemsToDelete);

            if (isDeleting)
            {
               DeleteItems(itemsProvider, itemsToDelete, logger);
               Console.WriteLine("Cleanup is finished!");
            }

            if (!string.IsNullOrEmpty(inputParameters.ArchivePath))
            {
               Console.WriteLine($"Zipping content from {inputParameters.RootPath} to {inputParameters.ArchivePath}!");
               ZipFile.CreateFromDirectory(inputParameters.RootPath, inputParameters.ArchivePath);
            }

            Console.ReadKey();
         }
         catch (Exception exception)
         {
            Console.WriteLine($"Something went wrong: {exception.Message}");
         }
      }

      private static void DeleteItems(FileSystemItemsProvider itemsProvider, ItemsToDelete itemsToDelete, ILogger logger)
      {
         //VisualStudio
         LogResult(itemsProvider.DeleteItems(itemsToDelete.ItemsInDependenciesDirectory), logger);
         LogResult(itemsProvider.DeleteItems(itemsToDelete.ItemsInPackageDirectory), logger);
         LogResult(itemsProvider.DeleteItems(itemsToDelete.ItemsInObjDirectory), logger);
         LogResult(itemsProvider.DeleteItems(itemsToDelete.ItemsInBinDirectory), logger);
         LogResult(itemsProvider.DeleteItems(itemsToDelete.ItemsInVsDirectory), logger);
         if (!string.IsNullOrEmpty(itemsToDelete.VsUserFile?.Path))
         {
            File.Delete(itemsToDelete.VsUserFile?.Path);
         }

         //CI
         LogResult(itemsProvider.DeleteItems(itemsToDelete.ItemsInArtifactsDirectory), logger);
         LogResult(itemsProvider.DeleteItems(itemsToDelete.ItemsInDeploymentDirectory), logger);
      }

      private static void LogResult(ReturnResult result, ILogger logger)
      {
         if (!result.IsSuccess)
         {
            logger.Info(result.Message);
         }
      }
   }
}
