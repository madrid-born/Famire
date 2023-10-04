using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Media;
using Mopups.Pages;
using Mopups.Services;
using Music_Player.Models;

namespace Music_Player.Pages.PopUps ;

    public partial class NewPlayList : PopupPage
    {
        private Song _tappedSong;
        public NewPlayList(Song song)
        {
            InitializeComponent();
            _tappedSong = song;
        }
        
        private void Disappear()
        {
            MopupService.Instance.PopAsync();
        }

        private void Confirm_OnClicked(object sender, EventArgs e)
        {
            var name = NameEntry.Text;
            CreatePlaylist(name);
            Disappear();
        }

        private async void CreatePlaylist(string playlistName)
        {
            try
            {
                
                var playlistDirectory = Path.Combine("/storage/emulated/0", "PlayList");
                var playlistPath = Path.Combine(playlistDirectory, $"{playlistName}.m3u");

                // Check if the playlist directory exists, if not, create it
                if (!Directory.Exists(playlistDirectory))
                {
                    Directory.CreateDirectory(playlistDirectory);
                }

                // Create or overwrite the playlist file
                using (StreamWriter writer = new StreamWriter(playlistPath))
                {
                    // Write the header information (optional)
                    writer.WriteLine("#EXTM3U");

                    // Write each song path to the playlist
                    writer.WriteLine(_tappedSong.Direction);
                }

                // Let the Android Scan PlayList for new added song 
                MediaScannerConnection.ScanFile(
                    Android.App.Application.Context,
                    new string[] { playlistPath }, 
                    null,
                    null
                    );
                
                await DisplayAlert("Success", "Playlist created successfully.", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }
    }