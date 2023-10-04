using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.ViewPages;

namespace Music_Player.Pages.SingleContentPages ;

    public partial class PlayListPage : ContentPage
    {
        private PlayList CurrentPlayList { get; set; }
        private readonly SongPlayer _player;
        public PlayListPage(SongPlayer player, PlayList playList)
        {
            InitializeComponent();
            _player = player;
            try
            {
                playList.Songs =  new AudioFileHelper(Android.App.Application.Context).GetSongs(true, playList);

            }
            catch (Exception e)
            {
                DisplayAlert("ok", e.Message, "ok");

            }
            //playList.LoadSongs();
            SongContent.Content = new Views.SongsListView(player, playList);
        }

        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }