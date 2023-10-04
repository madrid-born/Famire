using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.ViewPages;

namespace Music_Player.Pages.SingleContentPages ;

    public partial class GenrePage : ContentPage
    {
        private readonly SongPlayer _player;
        
        public GenrePage(SongPlayer player, Genre genre)
        {
            InitializeComponent();
            _player = player;
            //
            Name.Text = "name : " + genre.Name;
            //
            SongContent.Content = new Views.SongsListView(player, genre);
        }
        

        private async void Play_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PlayPage(_player));
        }
    }