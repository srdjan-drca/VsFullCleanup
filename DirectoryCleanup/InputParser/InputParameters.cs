using DirectoryCleanup.Core.Result;
using System;
using System.IO;
using System.Reflection;

namespace DirectoryCleanup.InputParser
{
    public class InputParameters
    {
        public string RootPath { get; set; }
        public bool IsDeletingPackages { get; set; }

        public ReturnResult ParseArguments(string[] parameterList)
        {
            int helpOptIndex = Array.IndexOf(parameterList, "-help");
            int pathOptIndex = Array.IndexOf(parameterList, "-path");
            int packagesOptIndex = Array.IndexOf(parameterList, "-packages");

            if (helpOptIndex > 0)
            {
                return new FailResult(CreateHelpMessage(string.Empty));
            }

            RootPath = GetRootPath(pathOptIndex, parameterList);
            IsDeletingPackages = packagesOptIndex > 0;

            if(string.IsNullOrEmpty(RootPath))
            {
                return new FailResult(CreateHelpMessage("Missing path value"));
            }

            return new SuccessResult();
        }

        private string CreateHelpMessage(string reason)
        {
            return $"{reason}.{Environment.NewLine}" +
                $"DirectoryCleanup -path <path_to_files> -packages -help{Environment.NewLine}" +
                $" -path: Root path of VS project. If not specified current directory is taken.{Environment.NewLine}" +
                $" -packages: Tool deletes packages directory for specified path.{Environment.NewLine}" +
                $" -help: This message.{Environment.NewLine}";
        }

        private string GetRootPath(int pathOptIndex, string[] parameterList)
        {
            if (pathOptIndex > 0)
            {
                if (pathOptIndex + 1 <= parameterList.Length)
                {
                    return new DirectoryInfo(parameterList[pathOptIndex + 1]).FullName;
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return GetCurrentDirectory();
            }
        }

        private string GetCurrentDirectory()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var directory = Path.GetDirectoryName(assemblyLocation);
            var directoryInfo = new DirectoryInfo(directory);

            return directoryInfo.FullName;
        }
    }
}