using DirectoryCleanup.Core;
using DirectoryCleanup.Core.Models;
using DirectoryCleanup.Core.Result;
using DirectoryCleanup.Extensions;
using DirectoryCleanup.Helpers;
using DirectoryCleanup.InputParser;
using DirectoryCleanup.Logger;
using System;
using System.Collections.Generic;

namespace DirectoryCleanup
{
    internal static class Program
    {
        private static void Main(string[] args)
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

                Console.ReadKey();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Something went wrong: {exception.Message}");
            }
        }

        private static void DeleteItems(FileSystemItemsProvider itemsProvider, ItemsToDelete itemsToDelete, ILogger logger)
        {
            LogError(itemsProvider.DeleteItems(itemsToDelete.BinDirectoryItemList), logger);
            LogError(itemsProvider.DeleteItems(itemsToDelete.ObjDirectoryItemList), logger);
            LogError(itemsProvider.DeleteItems(itemsToDelete.VsDirectoryItemList), logger);
            LogError(itemsProvider.DeleteItems(itemsToDelete.VsUserFileList), logger);
            LogError(itemsProvider.DeleteItems(itemsToDelete.PackageDirectoryItemList), logger);
        }

        private static void LogError(ReturnResult result, ILogger logger)
        {
            if (!result.IsSuccess)
            {
                logger.Info(result.Message);
            }
        }
    }
}