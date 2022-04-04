using DirectoryCleanup.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace DirectoryCleanup.InputParser
{
    public class ItemsToDelete
    {
        public List<FileSystemItem> BinDirectoryItemList { get; set; }
        public List<FileSystemItem> ObjDirectoryItemList { get; set; }
        public List<FileSystemItem> VsDirectoryItemList { get; set; }
        public List<FileSystemItem> VsUserFileList { get; set; }
        public List<FileSystemItem> PackageDirectoryItemList { get; set; }

        public ItemsToDelete(List<FileSystemItem> fileSystemItems, InputParameters inputParameters)
        {
            BinDirectoryItemList = fileSystemItems.Where(x => x.Path.Contains(@"\bin\") && !x.Path.Contains(@"node_modules")).ToList();
            ObjDirectoryItemList = fileSystemItems.Where(x => x.Path.Contains(@"\obj\")).ToList();
            VsDirectoryItemList = fileSystemItems.Where(x => x.Path.Contains(@"\.vs")).ToList();
            VsUserFileList = fileSystemItems.Where(x => x.Path.Contains(@".user")).ToList();
            PackageDirectoryItemList = inputParameters.IsDeletingPackages
                ? fileSystemItems.Where(x => x.Path.Contains(@"\packages\") && !x.Path.Contains(@"node_modules")).ToList()
                : new List<FileSystemItem>();
        }
    }
}