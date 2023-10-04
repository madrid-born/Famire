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

    public partial class AlbumsView : ContentView
    {
        private readonly SongPlayer _player;

        public AlbumsView(SongPlayer player, List<Album> albums)
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
    }