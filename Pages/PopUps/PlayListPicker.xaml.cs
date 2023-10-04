using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Media;
using Android.Content;
using Xamarin.Essentials;
using Mopups.Pages;
using Mopups.Services;
using Music_Player.Methods;
using Music_Player.Models;
using FilePicker = Xamarin.Essentials.FilePicker;
using FileResult = Xamarin.Essentials.FileResult;
using PickOptions = Xamarin.Essentials.PickOptions;

namespace Music_Player.Pages.PopUps ;

    public partial class PlayListPicker : PopupPage
    {
        private List<PlayList> _playLists = Functions.ReadAllPlayLists();
        private readonly Song _song;
        
        public PlayListPicker(Song song)
        {
            InitializeComponent();
            List.ItemsSource = _playLists;
            _song = song;
        }

        private void Disappear()
        {
            MopupService.Instance.PopAsync();
        }
        
        private void AddNew_OnClicked(object sender, EventArgs e)
        {
            Disappear();
            MopupService.Instance.PushAsync(new NewPlayList(_song));
            //Disappear();
        }

        private void PlayList_OnClicked(object sender, EventArgs e)
        {
            var tappedPlayList = (sender as Button)?.CommandParameter;
            if (tappedPlayList is PlayList playList)
            {
                AddSongToPlaylist(_song.Direction, playList.Direction);
            }
            Disappear();

        }

        private void AddSongToPlaylist(string songPath, string playlistPath)
        {

            const string storagePrefix = "/storage/emulated/0";
            songPath = songPath.Replace(storagePrefix, "..");
            
            try
            {
                // Read the existing playlist
                string[] existingPlaylist = File.ReadAllLines(playlistPath);

                // Check if the song is already in the playlist
                if (Array.Exists(existingPlaylist, line => line.Equals(songPath)))
                {
                    DisplayAlert("message", "Song is already in the playlist.", "ok");
                    return;
                }

                // Add the song to the playlist
                File.AppendAllLines(playlistPath, new[] { songPath });
                
                // Let the Android Scan the new PlayList
                MediaScannerConnection.ScanFile(
                    Android.App.Application.Context,
                    new string[] { playlistPath }, 
                    null,
                    null
                    );
                
                DisplayAlert("message", "Song added to the playlist.", "ok");

            }
            catch (Exception ex)
            {
                DisplayAlert("message", $"An error occurred: {ex.Message}", "ok");
            }
        }

    }