using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Music_Player.Methods;
using SQLite;
using File = TagLib.File;
using IPicture = TagLib.IPicture;

namespace Music_Player.Models ;

    public class Song
    {
        public string Name { get; set; }
        public string Direction { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AlbumArtist { get; set; }
        public int TrackNumber { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public byte[] CoverArt { get; set; }
        public int Duration { get; set; }
        
        public Song( string direction, string title, string artist, string album,
            string albumArtist, int trackNumber, string genre, int year, int duration, byte[] coverArt)
        {
            Name = Functions.Rculs(direction);
            Direction = direction;
            Title = title;
            Artist = artist;
            Album = album;
            AlbumArtist = albumArtist;
            TrackNumber = trackNumber;
            Genre = genre;
            Year = year;
            Duration = duration;
            CoverArt = coverArt;
        }
        
        

        public Song(string direction)
        {
            // for times you read musics list directly from storage
            Direction = direction;
            Name = Functions.Rculs(direction);
            //ReadEssential();
        }

        public Song(string songDirection, string playListDirection)
        {
            // for times you read musics list from a PlayList
            Direction = "/storage/emulated/0" + songDirection.Substring(2);
            //Direction = playListDirection + songDirection;
            Name = Functions.Rculs(Direction);
        }
        public Song(string direction, string album, int i)
        {
            Direction = direction;
            Name = Functions.Rculs(direction);
            Album = album;
        }

        public void ReadEssential()
        {
            try
            {
                FilePathAbstraction fileAbstraction = new FilePathAbstraction(Direction);
                using var mp3File = File.Create(fileAbstraction) ;
                // Extract metadata
                Title = mp3File.Tag.Title;
                Artist = mp3File.Tag.FirstPerformer;
                Album = mp3File.Tag.Album;
                Genre = mp3File.Tag.Genres.FirstOrDefault();
            }
            catch (Exception ex)
            {
               
            }
        }
        public void oo()
        {
            

            //File mp3File = File.Create(new StreamFileAbstraction(Direction, stream, stream));

            //string mp3FilePath = "path_to_your_mp3_file.mp3";
            
            try
            {
                FilePathAbstraction fileAbstraction = new FilePathAbstraction(Direction);
                using var mp3File = File.Create(fileAbstraction) ;
                // Extract metadata
                Title = mp3File.Tag.Title;
                Artist = mp3File.Tag.FirstPerformer;
                Album = mp3File.Tag.Album;
                AlbumArtist = mp3File.Tag.AlbumArtists.FirstOrDefault();
                TrackNumber = (int)mp3File.Tag.Track;
                Genre = mp3File.Tag.Genres.FirstOrDefault();
                Year = (int)mp3File.Tag.Year;
                //Comment = mp3File.Tag.Comment;
                //Lyrics = mp3File.Tag.Lyrics; 
                TimeSpan duration = mp3File.Properties.Duration;
                double durationInSeconds = duration.TotalSeconds;
                Duration = (int)durationInSeconds;
                ////CoverArt = mp3File.Tag.;
                //CoverArt = mp3File.Tag.Pictures.FirstOrDefault();

                // You can access more metadata properties as needed

                // If you want to retrieve the cover art image as a byte array
                //byte[] coverArtData = CoverArt?.Data.Data;

                // Now you can use the extracted metadata in your UI
            }
            catch (Exception ex)
            {
                
            }
        }
    }