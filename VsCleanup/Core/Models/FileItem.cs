namespace VsCleanup.Core.Models
{
    public class FileItem : FileSystemItem
    {
        public FileItem(string name, string path, bool isUsed = false) : base(name, path, isUsed)
        {
        }
    }
}