using Android.Media;
using Mopups.Pages;
using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;

namespace Music_Player.Pages.PopUps ;

    public partial class OptionMenu : PopupPage
    {
        private readonly SongPlayer _player;        
        private readonly Song _songTapped;
        private readonly string _source;
        private readonly Album _albumTapped;
        private readonly Artist _artistTapped;
        private readonly Folder _folderTapped;
        private readonly Genre _genreTapped;
        private readonly PlayList _playListTapped;
        
        public OptionMenu(SongPlayer player, Album album)
        {
            InitializeComponent();
            _player = player;
            _albumTapped = album;
            
            var buttons = new List<Button>{ 
                AddAlbumToNextOn()
            };
            
            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }        
        }
        
        public OptionMenu(SongPlayer player, Artist artist)
        {
            InitializeComponent();
            _player = player;
            _artistTapped = artist;
            var buttons = new List<Button>{ 
                AddArtistToNextOn()
            };
            
            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }        
        }
        
        public OptionMenu(SongPlayer player, Folder folder)
        {
            InitializeComponent();
            _player = player;
            _folderTapped = folder;
            
            var buttons = new List<Button>{ 
                AddFolderToNextOn()
            };
            
            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }
        }

        public OptionMenu(SongPlayer player, Genre genre)
        {
            InitializeComponent();
            _player = player;
            _genreTapped = genre;
            
            var buttons = new List<Button>{ 
                AddGenreToNextOn()
            };
            
            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }        
        }
        
        public OptionMenu(SongPlayer player, PlayList playList)
        {
            InitializeComponent();
            _player = player;
            _playListTapped = playList;
            
            var buttons = new List<Button>{ 
                AddPlayListToNextOn()
            };
            
            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }        
        }
        
        public OptionMenu(SongPlayer player, Song song, Album album)
        {
            
            InitializeComponent();
            _player = player;
            _songTapped = song;
            _albumTapped = album;
            
            var buttons = new List<Button>{ 
                AddToQueueButton(),
                AddToNextOn(),
                AddToPlayList(),
                //Delete(),
                //Share(),
                //Details()
            };

            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }
        }
        
        public OptionMenu(SongPlayer player, Song song, Artist artist)
        {
            
            InitializeComponent();
            _player = player;
            _songTapped = song;
            _artistTapped = artist;
            
            var buttons = new List<Button>{ 
                AddToQueueButton(),
                AddToNextOn(),
                AddToPlayList(),
                //Delete(),
                //Share(),
                //Details()
            };

            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }
        }
        
        public OptionMenu(SongPlayer player, Song song, Folder folder)
        {
            
            InitializeComponent();
            _player = player;
            _songTapped = song;
            _folderTapped = folder;
            
            var buttons = new List<Button>{ 
                AddToQueueButton(),
                AddToNextOn(),
                AddToPlayList(),
                //Delete(),
                //Share(),
                //Details()
            };

            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }
        }
        
        public OptionMenu(SongPlayer player, Song song, Genre genre)
        {
            
            InitializeComponent();
            _player = player;
            _songTapped = song;
            _genreTapped = genre;
            
            var buttons = new List<Button>{ 
                AddToQueueButton(),
                AddToNextOn(),
                AddToPlayList(),
                RemoveFromPlayList(),
                //Delete(),
                //Share(),
                //Details()
            };

            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }
        }
        
        public OptionMenu(SongPlayer player, Song song, PlayList playList)
        {
            
            InitializeComponent();
            _player = player;
            _songTapped = song;
            _playListTapped = playList;
            
            var buttons = new List<Button>{ 
                AddToQueueButton(),
                AddToNextOn(),
                AddToPlayList(),
                RemoveFromPlayList(),
                //Delete(),
                //Share(),
                //Details()
            };

            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }
        }
        
        public OptionMenu(SongPlayer player, Song song, string source)
        {
            
            InitializeComponent();
            _player = player;
            _songTapped = song;
            _source = source;
            
            var buttons = new List<Button>{ 
                AddToQueueButton(),
                AddToNextOn(),
                AddToPlayList(),
                RemoveFromList(),
                //Delete(),
                //Share(),
                //Details()
            };
            
            switch (source)
            {
                case "List":
                    buttons.RemoveAt(3);
                    break;
                case "PreviousOn":
                    break;
                case "CurrentSong":
                    buttons.RemoveAt(0);
                    buttons.RemoveAt(0);
                    buttons.RemoveAt(1);
                    break;
                case "Queue":
                    buttons.RemoveAt(0);
                    break;
                case "NextOn":
                    buttons.RemoveAt(1);
                    buttons.Insert(1, MoveToTopNextOn());
                    break;
            }
            
            foreach (var button in buttons)
            {
                MainLayout.Children.Add(button);
            }
        }

        public void LoadPage()
        {
            
        }

        private void Disappear()
        {
            MopupService.Instance.PopAsync();
        }

        private Button NewButton(string name)
        {
            return new Button
            {
                Text = name
            };
        }

        private Button AddToQueueButton()
        {
            Button button = NewButton("Add to Queue");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                _player.AddToQueue(_songTapped);
                _player.ReloadSongsPlayView();
            };
            
            return button;
        }

        private Button AddToNextOn()
        {
            Button button = NewButton("Add to NextOn");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                _player.AddToNextOn(_songTapped);
                _player.ReloadSongsPlayView();
                
            };
            return button;
        }
        
        private Button MoveToTopNextOn()
        {
            Button button = NewButton("Move it to top");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                _player.MoveTopNextOn(_songTapped);
                _player.ReloadSongsPlayView();
                
            };
            return button;
        }
        
        private Button AddToPlayList()
        {
            Button button = NewButton("Add to PlayList");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                MopupService.Instance.PushAsync(new PlayListPicker(_songTapped));
                
            };
            return button;
        }
        
        // @@@
        private Button Share()
        {
            Button button = NewButton("Share");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
            };
            return button;
        }

        // @@@
        private Button Details()
        {
            Button button = NewButton("Details");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
            };
            return button;
        }
        
        private Button RemoveFromList()
        {
            Button button = NewButton("Remove from list");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                switch (_source)
                {
                    case "PreviousOn":
                        _player.RemoveFromPrevious(_songTapped);
                        break;
                    case "Queue":
                        _player.RemoveFromQueue(_songTapped);
                        break;
                    case "NextOn":
                        _player.RemoveFromNextOn(_songTapped);
                        break;
                }
                _player.ReloadSongsPlayView();
            };
            return button;
        }
        
        // @@@
        private Button Delete()
        {
            Button button = NewButton("Delete");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
            };
            return button;
        }
        
        private Button RemoveFromPlayList()
        {
            Button button = NewButton("Remove from playList");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                try
                {
                    // Read the existing playlist
                    var existingPlaylist = File.ReadAllLines(_playListTapped.Direction).ToList();
                    var songPath = _songTapped.Direction.Replace("/storage/emulated/0", "");
                    // Remove the song form the playlist
                    existingPlaylist.RemoveAt(existingPlaylist.FindIndex(line => line.Contains(songPath)));

                    // Write all the rest of the playlist songs to iy
                    File.WriteAllLines(_playListTapped.Direction, existingPlaylist);
                
                    // Let the Android Scan the new PlayList
                    MediaScannerConnection.ScanFile(
                        Android.App.Application.Context,
                        new string[] { _playListTapped.Direction }, 
                        null,
                        null
                        );
                
                    DisplayAlert("message", "Song removed from the playlist.", "ok");

                }
                catch (Exception ex)
                {
                    DisplayAlert("message", $"An error occurred: {ex.Message}", "ok");
                }
                
            };
            return button;
        }

        private Button AddAlbumToNextOn()
        {
            Button button = NewButton("Add Album to NextOn");
            
            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                foreach (var song in _albumTapped.Songs)
                {
                    _player.AddToNextOn(song);
                }
                _player.ReloadSongsPlayView();
            };
            return button;
        }
        
        private Button AddArtistToNextOn()
        {
            Button button = NewButton("Add Artist to NextOn");
            
            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                foreach (var song in _artistTapped.Songs)
                {
                    _player.AddToNextOn(song);
                }
                _player.ReloadSongsPlayView();
            };
            return button;
        }
        
        private Button AddFolderToNextOn()
        {
            Button button = NewButton("Add Folder to NextOn");
            
            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                foreach (var song in _folderTapped.Songs)
                {
                    _player.AddToNextOn(song);
                }
                _player.ReloadSongsPlayView();
            };
            return button;
        }
        
        private Button AddGenreToNextOn()
        {
            Button button = NewButton("Add Genre to NextOn");
            
            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                foreach (var song in _genreTapped.Songs)
                {
                    _player.AddToNextOn(song);
                }
                _player.ReloadSongsPlayView();
            };
            return button;
        }
        
        private Button AddPlayListToNextOn()
        {
            Button button = NewButton("Add PlayList to NextOn");
            
            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
                foreach (var song in _playListTapped.Songs)
                {
                    _player.AddToNextOn(song);
                }
                _player.ReloadSongsPlayView();
            };
            return button;
        }
        
        // @@@
        private Button ChooseArtWork()
        {
            Button button = NewButton("Choose Artwork");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
            };
            return button;
        }
        
        // @@@
        private Button RenamePlayList()
        {
            Button button = NewButton("Rename");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
            };
            return button;
        }
        
        // @@@
        private Button DeletePlayList()
        {
            Button button = NewButton("Delete");

            button.Clicked += (object sender, EventArgs e) =>
            {
                Disappear();
            };
            return button;
        }
        
    }