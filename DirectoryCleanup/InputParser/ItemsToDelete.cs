using DirectoryCleanup.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DirectoryCleanup.InputParser
{
    public class ItemsToDelete
    {
        public List<FileSystemItem> ItemsInBinDirectory { get; set; }
        public List<FileSystemItem> ItemsInObjDirectory { get; set; }
        public List<FileSystemItem> ItemsInVsDirectory { get; set; }
        public FileSystemItem VsUserFile { get; set; }
        public List<FileSystemItem> ItemsInPackageDirectory { get; set; }

        public ItemsToDelete(List<FileSystemItem> fileSystemItems, InputParameters inputParameters)
        {
            ItemsInBinDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\bin\")).ToList();
            ItemsInObjDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\obj\")).ToList();
            ItemsInVsDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\.vs")).ToList();
            VsUserFile = fileSystemItems.FirstOrDefault(x => x.Path.Contains(@".user"));
            ItemsInPackageDirectory = inputParameters.IsDeletingPackages
                ? fileSystemItems.Where(x => x.Path.Contains(@"\packages\")).ToList()
                : new List<FileSystemItem>();
        }
    }
}