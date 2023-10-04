using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.AppPages;
using Music_Player.Pages.PopUps;

namespace Music_Player.Pages.Views ;

    public partial class SongsPlayView : ContentView
    {
        private readonly SongPlayer _player;
        private readonly ContentView _contentView;

        public SongsPlayView(SongPlayer player)
        {
            InitializeComponent();
            _player = player;
            LoadPage();
            _contentView = this;
            _player.SongsPlayViewEvent += OnSongsPlayView;
        }

        
      

        public void LoadPage()
        {
            PreviousLayout.ItemsSource = null;
            PreviousLayout.ItemsSource = _player.PreviousOn;
            CurrentLayout.ItemsSource = null;
            CurrentLayout.ItemsSource = new List<Song>{_player.CurrentSong};
            QueueLayout.ItemsSource = null;
            QueueLayout.ItemsSource = _player.Queue;
            NextLayout.ItemsSource = null;
            NextLayout.ItemsSource = _player.NextOn;
        }

        private void PreviousOnSong_OnClicked(object sender, EventArgs e)
        {
            var songTapped = (sender as Button)?.CommandParameter;
            if (songTapped is Song song)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, song, "PreviousOn"));
            }        
        }

        private void CurrentSong_OnClicked(object sender, EventArgs e)
        {
            var songTapped = (sender as Button)?.CommandParameter;
            if (songTapped is Song song)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, song, "CurrentSong"));
            }        
        }

        private void QueueSong_OnClicked(object sender, EventArgs e)
        {
            var songTapped = (sender as Button)?.CommandParameter;
            if (songTapped is Song song)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, song, "Queue"));
            }        
        }

        private void NextOnSong_OnClicked(object sender, EventArgs e)
        {
            var songTapped = (sender as Button)?.CommandParameter;
            if (songTapped is Song song)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, song, "NextOn"));
            }        
        }

        private void OnSongsPlayView(object sender, EventArgs e)
        {
            LoadPage();
        }

        private void PreviousSongTapped(object sender, TappedEventArgs e)
        {
            var songTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (songTapped is Song song)
            {
                _player.PlayFromPreviousOn(song);
            }
        }

        private void CurrentSongTapped(object sender, TappedEventArgs e)
        {
            _player.Play();
        }

        private void QueueSongTapped(object sender, TappedEventArgs e)
        {
            var songTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (songTapped is Song song)
            {
                _player.PlayFromQueue(song);
            }
        }

        private void NextOnSongTapped(object sender, TappedEventArgs e)
        {
            var songTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (songTapped is Song song)
            {
                _player.PlayFromNextOn(song);
                
            }
        }
    }