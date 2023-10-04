using Android.Content;
using Android.Provider;
using Android.Database;
using Music_Player.Models;
using TagLib.Riff;

namespace Music_Player.Methods
{
    public class AudioFileHelper
    {
        private readonly Context _context;

        public AudioFileHelper(Context context)
        {
            _context = context;
        }

        public List<Song> GetSongs(bool fromPlayList, PlayList playList)
        {
            List<Song> songs = new List<Song>();

            string[] projection = {
                MediaStore.Audio.Media.InterfaceConsts.Data, // Audio file path
                MediaStore.Audio.Media.InterfaceConsts.Album, // Album name
                MediaStore.Audio.Media.InterfaceConsts.Title, // Song title
                MediaStore.Audio.Media.InterfaceConsts.Artist, // Artist name
                MediaStore.Audio.Media.InterfaceConsts.AlbumArtist, // Album artist
                MediaStore.Audio.Media.InterfaceConsts.Track, // Track number
                MediaStore.Audio.Media.InterfaceConsts.Genre, // Genre
                MediaStore.Audio.Media.InterfaceConsts.Year, // Year
                MediaStore.Audio.Media.InterfaceConsts.Duration, // Duration
                MediaStore.Audio.Media.InterfaceConsts.Id // Song ID
            };
            
            ICursor cursor;
            if (!fromPlayList)
            {
                cursor = _context.ContentResolver.Query(MediaStore.Audio.Media.ExternalContentUri, projection, null, null, null);
            }
            else
            {
                
                string selectionPlaylist = MediaStore.Audio.Playlists.Members.PlaylistId + "=?";
                string[] selectionArgsPlaylist = { playList.Id.ToString() }; // Use the playlist ID as the selection argument

                 cursor = _context.ContentResolver.Query(
                    MediaStore.Audio.Playlists.Members.GetContentUri("external", playList.Id),
                    projection,
                    selectionPlaylist,
                    selectionArgsPlaylist,
                    null
                    );
            }

            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    int dataIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Data);
                    string audioFilePath = cursor.GetString(dataIndex);

                    int albumIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Album);
                    string albumName = cursor.GetString(albumIndex);

                    int titleIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Title);
                    string songTitle = cursor.GetString(titleIndex);

                    int artistIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Artist);
                    string songArtist = cursor.GetString(artistIndex);

                    int albumArtistIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.AlbumArtist);
                    string songAlbumArtist = cursor.GetString(albumArtistIndex);

                    int trackNumberIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Track);
                    int songTrackNumber = cursor.GetInt(trackNumberIndex);

                    int genreIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Genre);
                    string songGenre = cursor.GetString(genreIndex);

                    int yearIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Year);
                    int songYear = cursor.GetInt(yearIndex);
                    
                    int durationIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Duration);
                    int songDuration = cursor.GetInt(durationIndex);

                    // Add cover art retrieval logic here (not shown in this code)

                    songs.Add(new Song(audioFilePath, songTitle, songArtist, albumName, songAlbumArtist, songTrackNumber, songGenre, songYear, songDuration, null));
                }
                cursor.Close();
            }
            return songs;
        }
        
        public List<Song> GetAllAudioFiles()
        {
            List<Song> songs = new List<Song>();

            // Define the columns you want to retrieve
            string[] projection = { MediaStore.Audio.Media.InterfaceConsts.Data };
            // Create a cursor to query the MediaStore for audio files
            ICursor cursor = _context.ContentResolver.Query(MediaStore.Audio.Media.ExternalContentUri, projection, null,null, null);

            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    int dataIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Media.InterfaceConsts.Data);
                    string audioFilePath = cursor.GetString(dataIndex);
                    if (!audioFilePath.Contains("/storage/emulated/0/media/audio"))
                    {
                        songs.Add(new Song(audioFilePath));
                    }
                }
                cursor.Close();
            }
            
            return songs;
        }

        
        public List<PlayList> GetAllPlayLists()
        {
            List<PlayList> playlists = new List<PlayList>();

            // Define the columns you want to retrieve
            string[] projection = {
                MediaStore.Audio.Playlists.InterfaceConsts.Id,
                MediaStore.Audio.Playlists.InterfaceConsts.Data,
                MediaStore.Audio.Playlists.InterfaceConsts.Name,
            };

            // Create a cursor to query the MediaStore for playlists
            ICursor cursor = _context.ContentResolver.Query(MediaStore.Audio.Playlists.ExternalContentUri, projection, null, null, null);

            if (cursor != null)
            {
                while (cursor.MoveToNext())
                {
                    int idIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Playlists.InterfaceConsts.Id);
                    int directionIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Playlists.InterfaceConsts.Data);
                    int nameIndex = cursor.GetColumnIndexOrThrow(MediaStore.Audio.Playlists.InterfaceConsts.Name);

                    long playlistId = cursor.GetLong(idIndex);
                    string playlistDirection = cursor.GetString(directionIndex);
                    string playlistName = cursor.GetString(nameIndex);
                    var playlist = new PlayList(playlistId, playlistDirection, playlistName);
                    //playlist.Songs = GetSongs(true, playlist);

                    playlists.Add(playlist);
                }
                cursor.Close();
            }
            return playlists;
        }
        
        public List<Song> GetSongsInPlaylist(PlayList playlist)
        {
            return GetSongs(true, playlist);
        }
        
        public List<Song> GetAllSongs()
        {
            return GetSongs(false, null);
        }
    }
}
