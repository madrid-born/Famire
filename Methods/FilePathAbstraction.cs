namespace Music_Player.Models ;

using TagLib;
using System.IO;

    public class FilePathAbstraction : TagLib.File.IFileAbstraction
    {
        private string filePath;
        private Stream stream;

        public FilePathAbstraction(string path)
        {
            filePath = path;
            stream = System.IO.File.OpenRead(filePath);
        }

        public string Name => filePath;

        public Stream ReadStream => stream;

        public Stream WriteStream => null;

        public void CloseStream(Stream stream)
        {
            stream.Close();
        }
    }
