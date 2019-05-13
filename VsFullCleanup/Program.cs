using System;
using System.IO;
using System.Collections.Generic;
using VsFullCleanup.Core;
using VsFullCleanup.Core.Models;
using VsFullCleanup.Core.Result;
using VsFullCleanup.Extensions;
using VsFullCleanup.Helpers;
using VsFullCleanup.InputParser;
using VsFullCleanup.Logger;

namespace VsFullCleanup
{
   class Program
   {
      static void Main(string[] args)
      {
         var logger = new SimpleLogger(@"BinObjLog.txt");
         var itemsProvider = new FileSystemItemsProvider(logger);
         var inputParameters = new InputParameters(args);

         try
         {
            List<FileSystemItem> fileSystemItems = itemsProvider.GetItems(inputParameters.RootPath).MakeFlat().AddBackslashOnDirectoryPaths();
            ItemsToDelete itemsToDelete = new ItemsToDelete(fileSystemItems, inputParameters);

            bool isDeleting = ConsoleUtils.AskQuestion(inputParameters.RootPath, itemsToDelete);

            if (isDeleting)
            {
               DeleteItems(itemsProvider, itemsToDelete, logger);

               Console.WriteLine("Process is finished!");
               Console.ReadKey();
            }
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
