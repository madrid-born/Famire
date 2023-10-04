using Mopups.Services;
using Music_Player.Models;
using Music_Player.Methods;
using Music_Player.Pages.AppPages;
using Music_Player.Pages.PopUps;
using TagLib.Riff;
using Path = Microsoft.Maui.Controls.Shapes.Path;

namespace Music_Player.Pages.ViewPages ;

    public partial class ViewFolders : ContentPage
    {
        private readonly List<Song> _allSongs;
        private readonly SongPlayer _player;
        private string _currentPath;
        private Folder _folder;


        public ViewFolders(SongPlayer player, List<Song> allSongs)
        {
            InitializeComponent();
            _player = player;
            _allSongs = allSongs;
            _currentPath = "/storage/emulated/0";
            LoadPage();
        }

        public void LoadPage()
        {
            _folder = new Folder(_currentPath, _allSongs);
            FoldersLayout.ItemsSource = null;
            FoldersLayout.ItemsSource = _folder.FoldersInside;
            FilesLayout.ItemsSource = null;
            FilesLayout.ItemsSource = _folder.Songs;
        }
        
        private void FolderTapped(object sender, TappedEventArgs e)
        {
            var folderTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (folderTapped is Folder folder)
            {
                _currentPath = _currentPath + "/" + folder.Name;
                LoadPage();
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
                _player.OriginalList = _folder.Songs;
                _player.CalculateOrganizedList(song);
                _player.Play();
            }
        }

        private void PreviousFolder_OnClicked(object sender, EventArgs e)
        {
            int lastSlashIndex = _currentPath.LastIndexOf('/');
            if (!_currentPath.Equals("/storage/emulated/0"))
            {
                _currentPath = _currentPath.Substring(0, lastSlashIndex);
            }
            LoadPage();
        }
        
        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }

        private void Folder_OnClicked(object sender, EventArgs e)
        {
            MopupService.Instance.PushAsync(new OptionMenu(_player, _folder));
        }
        
        private void Song_OnClicked(object sender, EventArgs e)
        {
            var songTapped = (sender as Button)?.CommandParameter;
            if (songTapped is Song song)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, song, "Folder"));
            }
        }
    }