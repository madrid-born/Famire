using System.Text.Json;
using System.Text.Json.Serialization;
using Music_Player.Models;


namespace Music_Player.Methods ;

    public class SongConverter : JsonConverter<Song>
    {
        public override Song Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
            {
                JsonElement root = doc.RootElement;
                string direction = root.GetProperty("Direction").GetString();
                string title = root.GetProperty("Title").GetString();
                string artist = root.GetProperty("Artist").GetString();
                string album = root.GetProperty("Album").GetString();
                string albumArtist = root.GetProperty("AlbumArtist").GetString();
                int trackNumber = root.GetProperty("TrackNumber").GetInt32();
                string genre = root.GetProperty("Genre").GetString();
                int year = root.GetProperty("Year").GetInt32();
                byte[] coverArt = null;
                int duration = root.GetProperty("Duration").GetInt32();

                return new Song(  direction,  title,  artist,  album,
                     albumArtist,  trackNumber,  genre,  year,  duration, coverArt);
            }
        }

        public override void Write(Utf8JsonWriter writer, Song value, JsonSerializerOptions options)
        {
            // Implement custom serialization logic if needed
        }
    }