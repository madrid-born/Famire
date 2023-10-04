using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.PopUps;

namespace Music_Player.Pages.Views ;

    public partial class SongsView : ContentView
    {
        private readonly SongPlayer _player;
        public SongsView(SongPlayer player, List<Song> songs)
        {
            InitializeComponent();
            _player = player;
            Songs.ItemsSource = songs;
        }

        private void OptionMenu_OnClicked(object sender, EventArgs e)
        {
            var songTapped = (sender as Button)?.CommandParameter;
            if (songTapped is Song song)
            {
                MopupService.Instance.PushAsync(new OptionMenu(_player, song, "List"));
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
                _player.CalculateOrganizedList(song);
                //_player.CalculateShuffleList(song);
                _player.Play();
            }
        }
    }