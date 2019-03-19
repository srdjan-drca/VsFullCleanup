using System.Collections.Generic;
using VsFullCleanup.Core.Models;
using VsFullCleanup.Core.Result;

namespace VsFullCleanup.Core
{
   public interface IFileSystemItemsProvider
   {
      List<FileSystemItem> GetItems(string path, DirectoryItem parent = null);

      ReturnResult DeleteItems(List<FileSystemItem> itemsToDelete);
   }
}