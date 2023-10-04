namespace Music_Player.Models ;

    public class Genre
    {
        public string Name { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();
        
        public Genre(string name)
        {
            Name = name;
        }
    }