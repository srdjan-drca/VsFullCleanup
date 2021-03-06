﻿using System.Collections.Generic;
using DirectoryCleanup.Core.Models;
using DirectoryCleanup.Core.Result;

namespace DirectoryCleanup.Core
{
   public interface IFileSystemItemsProvider
   {
      List<FileSystemItem> GetItems(string path, DirectoryItem parent = null);

      ReturnResult DeleteItems(List<FileSystemItem> itemsToDelete);
   }
}