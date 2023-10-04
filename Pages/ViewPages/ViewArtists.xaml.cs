using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.AppPages;
using Music_Player.Pages.PopUps;
using Music_Player.Pages.SingleContentPages;

namespace Music_Player.Pages.ViewPages ;

    public partial class ViewArtists : ContentPage
    {
        private readonly SongPlayer _player;
        public ViewArtists(SongPlayer player, List<Artist> artists)
        {
            InitializeComponent();
            _player = player;
            Artists.ItemsSource = artists;
        }
        
        private async void ArtistTapped(object sender, TappedEventArgs e)
        {
            var artistTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (artistTapped is Artist artist)
            {
                await Navigation.PushAsync(new ArtistPage(_player,artist));
            }
        }

        private void OptionMenu_OnClicked(object sender, EventArgs e)
        {
            var artistTapped = (sender as Button)?.CommandParameter;
            if (artistTapped is Artist artist)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, artist));
            }        
        }
        
        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }