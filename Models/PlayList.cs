using Music_Player.Methods;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Music_Player.Models ;

    public class PlayList
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Direction { get; set; }
        public List<Song> Songs { get; set; } = new List<Song>();

        public string err { get; set; } = "hey";
        public PlayList(string direction)
        {
            Direction = direction;
            Name = Functions.Rculs(direction.Substring(0, direction.Length - 4));
        }
        
        public PlayList(string direction, string name)
        {
            Name = name;
            Direction = direction;
        }
        
        public PlayList(long id, string direction, string name)
        {
            Id = id;
            Name = name;
            Direction = direction;
        }

        public void LoadSongs()
        {
            var allSongs = Functions.ReadAllSongs();
            string[] lines = File.ReadAllLines(Direction);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("#"))
                {
                    var matchingSong = allSongs.FirstOrDefault(song => song.Direction == line);
                    err = line;
                    if (matchingSong != null)
                    {
                        Songs.Add(matchingSong);
                    }
                }
            }
            

            //Songs.OrderBy(song => song.Name);
        }
    }