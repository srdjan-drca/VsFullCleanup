using System;
using System.Collections.Generic;
using VsCleanup.Core;
using VsCleanup.Core.Models;
using VsCleanup.Core.Result;
using VsCleanup.Extensions;
using VsCleanup.Helpers;
using VsCleanup.InputParser;
using VsCleanup.Logger;

namespace VsCleanup
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var logger = new SimpleLogger(@"VsCleanupLog.txt");
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