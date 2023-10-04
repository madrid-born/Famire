using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.Views;

namespace Music_Player.Pages.ViewPages ;

    public partial class ViewPlays : ContentPage
    {
        private SongPlayer _player;
        public ViewPlays(SongPlayer player)
        {
            InitializeComponent();
            _player = player;
            SongContent.Content = new SongsPlayView(player);
        }
        
        protected override void OnAppearing()
        {
            _player.ReloadPlayPage();
        }
    }