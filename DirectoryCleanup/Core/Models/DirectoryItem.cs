using System.Collections.Generic;

namespace DirectoryCleanup.Core.Models
{
   public class DirectoryItem : FileSystemItem
   {
      public List<FileSystemItem> FileSystemItems { get; set; }

      public DirectoryItem(string name, string path, List<FileSystemItem> items = null, bool isUsed = false) : base(name, path, isUsed)
      {
         FileSystemItems = new List<FileSystemItem>();

         if (items != null)
         {
            FileSystemItems.AddRange(items);
         }
      }
   }
}