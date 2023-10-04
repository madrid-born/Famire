using Music_Player.Methods;
using Music_Player.Models;
using TagLib.Riff;
using Xamarin.Essentials;
using System.Collections.Generic;
using Music_Player.Pages.Views;
using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;

namespace Music_Player.Pages.ViewPages;

    public partial class ViewSongs : ContentPage
    {
        private readonly SongPlayer _player;
        public ViewSongs(SongPlayer player, List<Song> allSongs)
        {
            InitializeComponent();
            _player = player;
            SongContent.Content = new SongsListView(player, allSongs,"List");
        }

        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }
