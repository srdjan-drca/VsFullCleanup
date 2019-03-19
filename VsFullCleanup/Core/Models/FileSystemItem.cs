namespace VsFullCleanup.Core.Models
{
   public class FileSystemItem
   {
      public FileSystemItem(string name, string path, bool isUsed)
      {
         Name = name;
         Path = path;
         IsUsed = isUsed;
      }

      public string Name { get; set; }

      public string Path { get; set; }

      public bool IsUsed { get; set; }

      public DirectoryItem Parent { get; set; }
   }
}