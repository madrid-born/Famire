using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.AppPages;
using Music_Player.Pages.PopUps;
using Music_Player.Pages.SingleContentPages;

namespace Music_Player.Pages.ViewPages ;
    public partial class ViewPlayLists : ContentPage
    {
        private readonly SongPlayer _player;

        public ViewPlayLists(SongPlayer player, List<PlayList> playLists)
        {
            InitializeComponent();
            _player = player;
            PlayLists.ItemsSource = playLists;
        }
        
        
        private void OptionMenu_OnClicked(object sender, EventArgs e)
        {
            var playlistTapped = (sender as Button)?.CommandParameter;
            if (playlistTapped is PlayList playlist)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, playlist));
            }
            
        }

        private async void PlayListTapped(object sender, TappedEventArgs e)
        {
            var playListTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (playListTapped is PlayList playList)
            {
                await Navigation.PushAsync(new PlayListPage(_player, playList));
            }
        }
        
        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }
    