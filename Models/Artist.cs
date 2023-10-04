namespace Music_Player.Models ;

    public class Artist
    {
        public string Name { get; set; }
        public Image Picture { get; set; }
        public string Bio { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();

        public Artist(string name)
        {
            Name = name;
        }
        
        
        public void ReadData()
        {
            try
            {
                Bio = $"yo, this is {Name} bio";
                
            }
            catch (Exception ex)
            {
                // handle error
            }
        }
    }