using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Provider;
using Music_Player.Methods;
using Music_Player.Models;
using Music_Player.Pages.Views;
using TagLib;
using TagLib.Riff;

namespace Music_Player.Pages.AppPages ;

    public partial class Search : ContentPage
    {
        private SongPlayer _player;
        private List<Song> _allSongs;
        private List<Album> Albums;
        private List<Artist> Artists;
        private List<Genre> Genres;

        public Search(SongPlayer player, List<Song> allSongs)
        {
            InitializeComponent();
            _player = player;
            _allSongs = allSongs;
            Albums = Functions.OrganizeSongsIntoAlbums(allSongs);
            Artists = Functions.OrganizeSongsIntoArtists(allSongs);
            Genres = Functions.OrganizeSongsIntoGenres(allSongs);
        }

        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue == null) return;
            //DisplayAlert("err", e.NewTextValue, "ss");
            var (albums, artists, genres, songs) = SearchAllSongs(e.NewTextValue);
            //SongContent.Content = new SongsView(_player, _allSongs);
            AlbumContent.Content = new AlbumsView(_player, albums);
            ArtistContent.Content = new ArtistView(_player, artists);
            GenreContent.Content = new GenreView(_player, genres);
            SongContent.Content = new SongsView(_player, songs);
        }

        private (List<Album>, List<Artist>, List<Genre>, List<Song>) SearchAllSongs(string searchValue)
        {
            var matchingAlbums = Albums
                .Where(album => album.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                .Select(album => album.Name).ToList();

            var matchingArtists = Artists
                .Where(artist => artist.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                .Select(artist => artist.Name).ToList();

            var matchingGenres = Genres
                .Where(genre => genre.Name.Contains(searchValue, StringComparison.OrdinalIgnoreCase))
                .Select(genre => genre.Name).ToList();

            var albums = Albums.Where(album => matchingAlbums.Contains(album.Name)).ToList();
            var artists = Artists.Where(artist => matchingArtists.Contains(artist.Name)).ToList();
            var genres = Genres.Where(genre => matchingGenres.Contains(genre.Name)).ToList();
            var songs = _allSongs.Where(song => song.Title.Contains(searchValue, StringComparison.OrdinalIgnoreCase)).ToList();

            return (albums, artists, genres, songs);
            
        }
    }