using Music_Player.Methods;
using TagLib.Riff;

namespace Music_Player.Models ;

    public class Folder
    {
        public readonly string Name;
        public readonly string CurrentPath;
        public readonly List<Folder> FoldersInside = new List<Folder>();
        public readonly List<Song> Songs;

        public Folder(string currentPath, List<Song> allSongs)
        {
            CurrentPath = currentPath;
            Name = Functions.Rculs(currentPath);
            var folders = Directory.GetDirectories(currentPath).ToList().OrderBy(folder => folder).ToList();
            foreach (var path in folders)
            {
                if (!Functions.Rculs(path).StartsWith("."))
                {
                    FoldersInside.Add(new Folder(path, allSongs));
                }
            }
            var files = Directory.GetFiles(currentPath).ToList();
            Songs = allSongs.Where(song => files.Contains(song.Direction)).ToList().OrderBy(song => song.Title).ToList();
        }
    }