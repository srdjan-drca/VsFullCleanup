using System.Linq;
using System.Collections.Generic;
using DirectoryCleanup.Core.Models;

namespace DirectoryCleanup.InputParser
{
   public class ItemsToDelete
   {
      //VisualStudio
      public List<FileSystemItem> ItemsInDependenciesDirectory { get; set; }

      public List<FileSystemItem> ItemsInPackageDirectory { get; set; }

      public List<FileSystemItem> ItemsInObjDirectory { get; set; }

      public List<FileSystemItem> ItemsInBinDirectory { get; set; }

      public List<FileSystemItem> ItemsInVsDirectory { get; set; }

      public FileSystemItem VsUserFile { get; set; }

      //CI
      public List<FileSystemItem> ItemsInArtifactsDirectory { get; set; }

      public List<FileSystemItem> ItemsInDeploymentDirectory { get; set; }

      public ItemsToDelete(List<FileSystemItem> fileSystemItems, InputParameters inputParameters)
      {
         ItemsInObjDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\obj\")).ToList();
         ItemsInBinDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\bin\")).ToList();
         ItemsInVsDirectory = fileSystemItems.Where(x => x.Path.Contains(@".vs\")).ToList();
         VsUserFile = fileSystemItems.FirstOrDefault(x => x.Path.Contains(@".user"));

         if (inputParameters.IsFullDelete)
         {
            ItemsInDependenciesDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\dependencies\")).ToList();
            ItemsInPackageDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\packages\")).ToList();
            ItemsInArtifactsDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\artifacts\")).ToList();
            ItemsInDeploymentDirectory = fileSystemItems.Where(x => x.Path.Contains(@"\deployment\")).ToList();
         }
      }
   }
}
