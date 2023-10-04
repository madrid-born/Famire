using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.ViewPages;

namespace Music_Player.Pages.SingleContentPages ;

    public partial class ArtistPage : ContentPage
    {
        private readonly SongPlayer _player;

        public ArtistPage(SongPlayer player, Artist artist)
        {
            InitializeComponent();
            _player = player;
            //
            artist.ReadData();
            Name.Text = "name : " + artist.Name;
            Bio.Text = "Bio : " + artist.Bio;
            //
            SongContent.Content = new Views.SongsListView(player, artist);
        }

        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }