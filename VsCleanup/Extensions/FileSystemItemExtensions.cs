using System.Collections.Generic;
using System.IO;
using System.Linq;
using VsCleanup.Core.Models;

namespace VsCleanup.Extensions
{
    public static class FileSystemItemExtensions
    {
        public static List<FileSystemItem> MakeFlat(this List<FileSystemItem> allItems)
        {
            List<FileSystemItem> allItemsFlat = allItems.SelectManyRecursive(x =>
            {
                var directoryItem = x as DirectoryItem;
                if (directoryItem != null)
                {
                    return ((DirectoryItem)x).FileSystemItems;
                }
                return x as IEnumerable<FileSystemItem>;
            }).ToList();

            return allItemsFlat;
        }

        public static List<FileSystemItem> MakeFlat(this IEnumerable<FileSystemItem> allItems)
        {
            List<FileSystemItem> allItemsFlat = allItems.SelectManyRecursive(x =>
            {
                var directoryItem = x as DirectoryItem;
                if (directoryItem != null)
                {
                    return ((DirectoryItem)x).FileSystemItems;
                }
                return x as IEnumerable<FileSystemItem>;
            }).ToList();

            return allItemsFlat;
        }

        public static List<FileSystemItem> AddBackslashOnDirectoryPaths(this List<FileSystemItem> allItems)
        {
            foreach (FileSystemItem fileSystemItem in allItems)
            {
                if (!Path.HasExtension(fileSystemItem.Path))
                {
                    fileSystemItem.Path += @"\";
                }
            }

            return allItems.ToList();
        }
    }
}