using System.Collections.Generic;
using VsCleanup.Core.Models;
using VsCleanup.Core.Result;

namespace VsCleanup.Core
{
    public interface IFileSystemItemsProvider
    {
        List<FileSystemItem> GetItems(string path, DirectoryItem parent = null);

        ReturnResult DeleteItems(List<FileSystemItem> itemsToDelete);
    }
}