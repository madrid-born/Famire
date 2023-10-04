using Android.OS.Storage;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.ViewPages;
using Permissions = Xamarin.Essentials.Permissions;
using PermissionStatus = Xamarin.Essentials.PermissionStatus;

namespace Music_Player.Pages.AppPages ;

    public partial class Home : ContentPage
    {
        private readonly SongPlayer _player;
        private readonly List<Song> _allSongs;
        public Home(SongPlayer player)
        {
            InitializeComponent();
            //RequestStoragePermission();
            _player = player;
            _allSongs = Functions.ReadAllSongs();
        }
        
        private async void Search_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Search(_player, _allSongs));
        }

        private async void Albums_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewAlbums(_player , Functions.OrganizeSongsIntoAlbums(_allSongs)));
        }

        private async void Artists_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewArtists(_player, Functions.OrganizeSongsIntoArtists(_allSongs)));
        }
        
        private async void Genres_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewGenres(_player, Functions.OrganizeSongsIntoGenres(_allSongs)));
        }
        
        private async void PlayLists_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewPlayLists(_player, Functions.ReadAllPlayLists()));
        }

        private async void Folders_OnClicked(object sender, EventArgs e)
        {
            /*if (_player.NextOn != null)
            {
                using var dbContext = new AppDbContext();
                try
                {
                    var newItem = _player.NextOn;
                    dbContext.NextOns.Add(newItem);
                    dbContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    DisplayAlert("sss", exception.Message, "Dwdw");
                }
            }
            else
            {
                await DisplayAlert("ddd", "nulllle", "de");
            }*/
            //await Navigation.PushAsync(new ViewFolders(_player, _allSongs));
        }
        private async void Songs_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewSongs(_player, _allSongs));
        }

        private async void Play_OnClicked(object sender, EventArgs e)
        {           
            await Navigation.PushAsync(new PlayPage(_player));
        }

        private async void RequestStoragePermission()
        {
            try
            {
                var status = await Permissions.RequestAsync<Permissions.StorageRead>();

                if (status == PermissionStatus.Granted)
                {
                    await DisplayAlert("ss", "ok", "SA");
                }
                else
                {
                    await DisplayAlert("ss", " not ok", "SA");
                
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("ss", ex.Message, "SA");
            }
        }
    }