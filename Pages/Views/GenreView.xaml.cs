using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.PopUps;
using Music_Player.Pages.SingleContentPages;

namespace Music_Player.Pages.Views ;

    public partial class GenreView : ContentView
    {
        private readonly SongPlayer _player;

        public GenreView(SongPlayer player, List<Genre> genres)
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
    }