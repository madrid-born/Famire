namespace Music_Player.Models ;
using File = TagLib.File;
using IPicture = TagLib.IPicture;

    public class Album
    {
        public string Name { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
        public Image Picture { get; set; }
        public string Artist { get; set; }
        public string ReleaseDate { get; set; }
        public string Genre { get; set; }
        public string Label { get; set; }
        public int Duration { get; set; }
        public string Producer { get; set; }
        public string Details { get; set; }

        public Album(string name)
        {
            Name = name;
        }
        
        public Album(string name, string artist)
        {
            Name = name;
            Artist = artist;
        }
        
        public void ReadData()
        {

            try
            {
                FilePathAbstraction fileAbstraction = new FilePathAbstraction(Songs[0].Direction);
                using var mp3File = File.Create(fileAbstraction) ;
               
                Artist = mp3File.Tag.FirstPerformer;
                Genre = mp3File.Tag.Genres.FirstOrDefault();
                TimeSpan duration = mp3File.Properties.Duration;
                double durationInSeconds = duration.TotalSeconds;
                Duration = (int)durationInSeconds;
            }
            catch (Exception ex)
            {
                // handle error
            }
        }
    }