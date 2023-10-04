using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Provider;
using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.PopUps;
using Music_Player.Pages.SingleContentPages;

namespace Music_Player.Pages.Views ;

    public partial class ArtistView : ContentView
    {
        private readonly SongPlayer _player;
        public ArtistView(SongPlayer player, List<Artist> artists)
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
    }