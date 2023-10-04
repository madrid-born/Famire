using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.ViewPages;

namespace Music_Player.Pages.SingleContentPages ;

    public partial class AlbumPage : ContentPage
    {
        private readonly SongPlayer _player;

        public AlbumPage(SongPlayer player, Album album)
        {
            InitializeComponent();
            _player = player;
            //
            album.ReadData();
            Name.Text = "name : " + album.Name;
            Artist.Text = "Artist : " + album.Artist;
            Duration.Text = "Duration : " + album.Duration.ToString();
            //
            SongContent.Content = new Views.SongsListView(_player, album);
            //SongContent.Content = new Views.SongsListView(player, album.Songs, "Album");
        }
        
        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }