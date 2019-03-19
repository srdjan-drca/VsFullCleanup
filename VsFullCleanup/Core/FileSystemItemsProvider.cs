using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VsFullCleanup.Core.Models;
using VsFullCleanup.Core.Result;
using VsFullCleanup.Logger;

namespace VsFullCleanup.Core
{
   public class FileSystemItemsProvider : IFileSystemItemsProvider
   {
      private readonly ILogger _logger;

      public FileSystemItemsProvider(ILogger logger)
      {
         _logger = logger;
      }

      public List<FileSystemItem> GetItems(string path, DirectoryItem parent = null)
      {
         var fileSystemItems = new List<FileSystemItem>();
         var directoryInfo = new DirectoryInfo(path);

         foreach (var directory in directoryInfo.GetDirectories())
         {
            var directoryItem = new DirectoryItem(directory.Name, directory.FullName);
            directoryItem.Parent = parent;
            directoryItem.FileSystemItems = GetItems(directory.FullName, directoryItem);

            fileSystemItems.Add(directoryItem);
         }

         foreach (FileInfo file in directoryInfo.GetFiles())
         {
            string lowercaseExtension = file.Extension.ToLower();
            string fileName = Path.ChangeExtension(file.Name, null) + lowercaseExtension;
            string fileFullName = Path.ChangeExtension(file.FullName, null) + lowercaseExtension;

            var fileItem = new FileItem(fileName, fileFullName);

            fileItem.Parent = parent;

            fileSystemItems.Add(fileItem);
         }

         return fileSystemItems;
      }

      public ReturnResult DeleteItems(List<FileSystemItem> itemsToDelete)
      {
         int numberOfDeletedFiles = 0;
         int numberOfDeletedDirectories = 0;
         bool isSuccess = true;

         try
         {
            List<FileItem> filesToDelete = itemsToDelete.OfType<FileItem>().ToList();
            foreach (FileItem fileItem in filesToDelete)
            {
               File.Delete(fileItem.Path);
               numberOfDeletedFiles++;
            }

            List<DirectoryItem> directoriesToDelete = itemsToDelete.OfType<DirectoryItem>().OrderByDescending(x => x.Path.Length).ToList();
            foreach (DirectoryItem directoryItem in directoriesToDelete)
            {
               Directory.Delete(directoryItem.Path);
               numberOfDeletedDirectories++;
            }
         }
         catch (Exception exception)
         {
            _logger.Info($@"DeleteItems error: {exception.Message}");

            isSuccess = false;
         }

         string message = $"Files / directories deleted: {numberOfDeletedFiles} / {numberOfDeletedDirectories}.";

         return new ReturnResult(isSuccess, message);
      }
   }
}