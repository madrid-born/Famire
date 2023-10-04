using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.AppPages;
using Music_Player.Pages.PopUps;
using Music_Player.Pages.SingleContentPages;

namespace Music_Player.Pages.ViewPages ;

    public partial class ViewAlbums : ContentPage
    {
        private readonly SongPlayer _player;

        public ViewAlbums(SongPlayer player, List<Album> albums)
        {
            InitializeComponent();
            _player = player;
            Albums.ItemsSource = albums;
        }

        private async void AlbumTapped(object sender, TappedEventArgs e)
        {
            var albumTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (albumTapped is Album album)
            {
                await Navigation.PushAsync(new AlbumPage(_player, album));
            }
        }

        private void OptionMenu_OnClicked(object sender, EventArgs e)
        {
            var albumTapped = (sender as Button)?.CommandParameter;
            if (albumTapped is Album album)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, album));
            }
        }

        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }