using Music_Player.Models;
using DevicePlatform = Xamarin.Essentials.DevicePlatform;
using FilePicker = Xamarin.Essentials.FilePicker;
using FilePickerFileType = Xamarin.Essentials.FilePickerFileType;

namespace Music_Player.Methods ;


    public class Functions 
    {
        
        public static async Task<List<string>> FindMusicFiles()
        {
            try
            {
                // Define the allowed MIME types for music files
                var options = new Xamarin.Essentials.PickOptions
                {
                    FileTypes = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                    {
                        { DevicePlatform.Android, new[] { "audio/*" } },
                        { DevicePlatform.iOS, new[] { "public.audio" } },
                    }),
                    PickerTitle = "Pick Music Files",
                };

                // Pick music files
                var result = await FilePicker.PickMultipleAsync(options);

                if (result != null)
                {
                    var musicFiles = new List<string>();
                    foreach (var file in result)
                    {
                        musicFiles.Add(file.FullPath); // Store the paths of selected music files
                    }

                    return musicFiles;
                }
                else
                {
                    // User canceled the file picker
                    return new List<string>();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error: {ex.Message}");
                return new List<string>();
            }
        }
        public static List<string> LoadFolders(string path)
        {
            List<string> directions = new List<string>();
            foreach (var direction in Directory.GetDirectories(path))
            {
                directions.Add(Rculs(direction));
            }
            return directions;
        }

        public static List<Song> LoadMusics(string path)
        {
            List<Song> Songs = new List<Song>();
            foreach (var musicDir in Directory.GetFiles(path, ".mp3"))
            {
                Songs.Add(new Song(musicDir));
            }
            return Songs;
        }
        public static List<Song> FindSongs()
        {
            List<Song> songs = new List<Song>();
            try
            {
                string internalStoragePath = "/storage/emulated/0";

                if (Directory.Exists(internalStoragePath))
                {
                    ParallelSearchForSongs(internalStoragePath, songs);
                }
                else
                {
                    // Handle case where path is not found
                }
            }
            catch (Exception ex)
            {
                // handle error
            }
            return songs;
        }

        private static void ParallelSearchForSongs(string directoryPath, List<Song> songs)
        {
            
            string[] songFiles = Directory.GetFiles(directoryPath, "*.mp3");
            foreach (string filePath in songFiles)
            {
                Song song = new Song(filePath);
                songs.Add(song);
            }

            string[] subDirectories = Directory.GetDirectories(directoryPath);
            Parallel.ForEach(subDirectories, subDir =>
            {
                string folderName = new DirectoryInfo(subDir).Name;
                if (folderName != "Android" && !folderName.StartsWith("."))
                {
                    ParallelSearchForSongs(subDir, songs);
                }
            });
        }
        
        public static List<PlayList> FindPlayLists()
        {
            List<PlayList> playLists = new List<PlayList>();
            try
            {
                string internalStoragePath = "/storage/emulated/0/Music";
                
                if (Directory.Exists(internalStoragePath))
                {
                    string[] playListDirections = Directory.GetFiles(internalStoragePath, "*.m3u");
                    foreach (string dir in playListDirections)
                    {
                        PlayList pl = new PlayList(dir);
                        playLists.Add(pl);
                    }
                }
                else
                {
                    //not found any playlist
                    
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
            return playLists;
        }
        
        public static string Rculs(string input)
        {
            int lastSlashIndex = input.LastIndexOf('/');
        
            if (lastSlashIndex >= 0)
            {
                return input.Substring(lastSlashIndex + 1);
            }
            else
            {
                return input;
            }
        }

        public static List<PlayList> ReadAllPlayLists()
        {
            return new AudioFileHelper(Android.App.Application.Context).GetAllPlayLists().OrderBy(playList => playList.Name).ToList();

        }
        
        public static List<Song> ReadAllSongs()
        {
            return new AudioFileHelper(Android.App.Application.Context).GetAllSongs().OrderBy(song => song.Title).ToList();
        }
        
        public static List<Album> OrganizeSongsIntoAlbums(List<Song> songs)
        {
            List<Album> albums = new List<Album>();

            foreach (var song in songs)
            {
                // Check if there's an album with the same name and artist
                Album existingAlbum = albums.FirstOrDefault(
                    album => album.Name == song.Album && album.Artist == song.AlbumArtist);

                if (existingAlbum != null)
                {
                    // Add the song to the existing album
                    existingAlbum.Songs.Add(song);
                }
                else
                {
                    // Create a new album and add it to the list
                    if (song.Album == null) continue;
                    Album newAlbum = new Album(song.Album, song.Artist);
                    newAlbum.Songs.Add(song);
                    albums.Add(newAlbum);
                }
            }

            return albums.OrderBy(album => album.Name).ToList();
        }
        
        public static List<Genre> OrganizeSongsIntoGenres(List<Song> songs)
        {
            List<Genre> genres = new List<Genre>();

            foreach (var song in songs)
            {
                // Check if there's an genre with the same name
                Genre existingGenre = genres.FirstOrDefault(
                    genre => genre.Name == song.Genre);

                if (existingGenre != null)
                {
                    // Add the song to the existing genre
                    existingGenre.Songs.Add(song);
                }
                else
                {
                    // Create a new genre and add it to the list
                    if (song.Genre == null) continue;
                    Genre newGenre = new Genre(song.Genre);
                    newGenre.Songs.Add(song);
                    genres.Add(newGenre);
                }
            }

            return genres.OrderBy(genre => genre.Name).ToList();;
        }
        
        public static List<Artist> OrganizeSongsIntoArtists(List<Song> songs)
        {
            List<Artist> artists = new List<Artist>();

            foreach (var song in songs)
            {
                // Check if there's an artist with the same name
                Artist existingArtist = artists.FirstOrDefault(
                    artist => artist.Name == song.Artist);

                if (existingArtist != null)
                {
                    // Add the song to the existing artist
                    existingArtist.Songs.Add(song);
                }
                else
                {
                    // Create a new artist and add it to the list
                    if (song.Artist == null) continue;
                    Artist newArtist = new Artist(song.Artist);
                    newArtist.Songs.Add(song);
                    artists.Add(newArtist);
                }
            }

            return artists.OrderBy(artist => artist.Name).ToList();;
        }

        public static List<PlayList> OrganizeSongsIntoPlayLists(List<Song> allSongs)
        {
            var result = new AudioFileHelper(Android.App.Application.Context).GetAllPlayLists().OrderBy(playList => playList.Name).ToList();
            foreach (var playList in result)
            {
                string[] lines = File.ReadAllLines(playList.Direction);

                foreach (string line in lines)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                    {
                        playList.Songs.Add(allSongs.Find(song => song.Direction.Equals(line)));
                    }
                }
                
            }
            return result;
        }
        
        public void AddSongToPlaylist(string songFilePath, string playlistFilePath)
        {
            try
            {
                // Read the existing playlist
                string[] existingPlaylist = File.ReadAllLines(playlistFilePath);

                // Check if the song is already in the playlist
                if (Array.Exists(existingPlaylist, line => line.Equals(songFilePath, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Song is already in the playlist.");
                    return;
                }

                // Add the song to the playlist
                File.AppendAllLines(playlistFilePath, new[] { songFilePath });
                Console.WriteLine("Song added to the playlist.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
    