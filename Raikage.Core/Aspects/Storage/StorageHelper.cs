using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCLStorage;

namespace RaikageFramework.Aspects.Storage
{
    internal static class StorageHelper
    {
        public static IDictionary<string, IFile> _files;
        public static async Task<IFile> LoadFile(string fileName)
        {
            var rootFolder = FileSystem.Current.LocalStorage;
            var folder = await rootFolder.CreateFolderAsync("LightningShadow",
                CreationCollisionOption.OpenIfExists);
            return await folder.CreateFileAsync(fileName,
                CreationCollisionOption.OpenIfExists);
        }

        public static async Task<string> ReadFile(IFile file)
        {
            using (var stream = await file.OpenAsync(FileAccess.Read))
            {
                using (var sr = new StreamReader(stream))
                {
                    return sr.ReadToEnd();
                }
            }

        }
        public static async void Save(IFile file, string data)
        {
            using (var stream = await file.OpenAsync(FileAccess.ReadAndWrite))
            {
                using (var sr = new StreamWriter(stream))
                {
                    sr.Write(data);
                }
            }

        }
    }
}
