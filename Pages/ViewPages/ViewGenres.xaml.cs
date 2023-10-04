using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.AppPages;
using Music_Player.Pages.PopUps;
using Music_Player.Pages.SingleContentPages;

namespace Music_Player.Pages.ViewPages ;

    public partial class ViewGenres : ContentPage
    {
        private readonly SongPlayer _player;

        public ViewGenres(SongPlayer player, List<Genre> genres)
        {
            InitializeComponent();
            _player = player;
            Genres.ItemsSource = genres;
        }

        private async void GenreTapped(object sender, TappedEventArgs e)
        {
            var albumTapped = (sender as Grid)?.GestureRecognizers
                .OfType<TapGestureRecognizer>()
                .FirstOrDefault()
                ?.CommandParameter;
            if (albumTapped is Genre album)
            {
                await Navigation.PushAsync(new GenrePage(_player, album));
            }
        }

        private void OptionMenu_OnClicked(object sender, EventArgs e)
        {
            var genreTapped = (sender as Button)?.CommandParameter;
            if (genreTapped is Genre genre)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, genre));
            }
        }
        
        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }