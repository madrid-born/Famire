using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.AppPages;
using Music_Player.Pages.PopUps;
using TagLib.Riff;

namespace Music_Player.Pages.Views ;

    public partial class SongsListView : ContentView
    {
        private readonly SongPlayer _player;
        private readonly string _source;
        private readonly List<Song> _listOfSongs;
        private Album _album;
        private Artist _artist;
        private Folder _folder;
        private Genre _genre;
        private PlayList _playLsit;

        public SongsListView(SongPlayer player, List<Song> listOfSongs, string source)
        {
            InitializeComponent();
            _player = player;
            _source = source;
            _listOfSongs = listOfSongs;
            Songs.ItemsSource = _listOfSongs;
        }

        public SongsListView(SongPlayer player, Album album)
        {
            InitializeComponent();
            _player = player;
            _album = album;
            _listOfSongs = _album.Songs;
            Songs.ItemsSource = _listOfSongs;
        }

        public SongsListView(SongPlayer player, Artist artist)
        {
            InitializeComponent();
            _player = player;
            _artist = artist;
            _listOfSongs = _artist.Songs;
            Songs.ItemsSource = _listOfSongs;
        }

        public SongsListView(SongPlayer player, Folder folder)
        {
            InitializeComponent();
            _player = player;
            _folder = folder;
            _listOfSongs = _folder.Songs;
            Songs.ItemsSource = _listOfSongs;
        }

        public SongsListView(SongPlayer player, Genre genre)
        {
            InitializeComponent();
            _player = player;
            _genre = genre;
            _listOfSongs = _genre.Songs;
            Songs.ItemsSource = _listOfSongs;
        }

        public SongsListView(SongPlayer player, PlayList playList)
        {
            InitializeComponent();
            _player = player;
            _playLsit = playList;
            _listOfSongs = _playLsit.Songs;
            Songs.ItemsSource = _listOfSongs;
        }
        
        public SongsListView(SongPlayer player, List<Song> listOfSongs)
        {
            InitializeComponent();
            _player = player;
            _listOfSongs = listOfSongs;
            Songs.ItemsSource = _listOfSongs;
        }
        
        private void OptionMenu_OnClicked(object sender, EventArgs e)
        {
            var songTapped = (sender as Button)?.CommandParameter;
            if (songTapped is Song song)
            {
                if (_album != null)
                {
                    MopupService.Instance.PushAsync(new OptionMenu(_player, song, _album));
                }
                else if (_artist != null)
                {
                    MopupService.Instance.PushAsync(new OptionMenu(_player, song, _artist));
                }
                else if (_folder != null)
                {
                    MopupService.Instance.PushAsync(new OptionMenu(_player, song, _folder));
                }
                else if (_genre != null)
                {
                    MopupService.Instance.PushAsync(new OptionMenu(_player, song, _genre));
                }
                else if (_playLsit != null)
                {
                    MopupService.Instance.PushAsync(new OptionMenu(_player, song, _playLsit));
                }
                else
                {
                    MopupService.Instance.PushAsync(new OptionMenu(_player, song, "List"));
                }
            }
        }

        private void SongTapped(object sender, TappedEventArgs e)
        {
            var songTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (songTapped is Song song)
            {
                _player.OriginalList = _listOfSongs;
                _player.CalculateOrganizedList(song);
                _player.Play();
            }
        }
    }