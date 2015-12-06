using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace MetroDictLib
{
    /// <summary>
    ///     Scans removable storage, creates a <see cref="StarDict" /> instances for every
    ///     dictionary found.
    /// </summary>
    public class DictFactory
    {
        private static readonly CreationCollisionOption _option = CreationCollisionOption.OpenIfExists;

        /// <summary>
        ///     Main method.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StarDict>> Create()
        {
            var result = new List<StarDict>();
            var devices = await KnownFolders.RemovableDevices.GetFoldersAsync();
            if (!devices.Any())
            {
                throw new Exception("This application requires removable devices to operate.");
            }
            foreach (var device in devices)
            {
                var subDir = "MetroDict";
                var subFolder = await device.CreateFolderAsync(subDir, _option);
                var files = await subFolder.GetFilesAsync(CommonFileQuery.OrderByName);
                StorageFile dzFile = null;
                foreach (var file in files)
                {
                    // here, we require two types of file at this moment: *.idx and *.dz
                    if (file.FileType == ".dz")
                    {
                        dzFile = file;
                        continue;
                    }
                    if (file.FileType == ".idx")
                    {
                        if (dzFile != null)
                        {
                            result.Add(new StarDict(dzFile, file));
                        }
                    }
                }
            }
            return result;
        }
    }
}