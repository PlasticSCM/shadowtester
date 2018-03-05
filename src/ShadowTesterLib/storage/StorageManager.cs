using System.IO;

namespace ShadowTesterLib.Storage
{
    public class StorageManager
    {
        private DirectoryInfo directory;

        public void CreateCapturesDirectory(string path)
        {
            directory = Directory.CreateDirectory(path);
        }
    }
}