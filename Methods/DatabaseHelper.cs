using Android.Database.Sqlite;
using Android.Content;

namespace Music_Player.Methods ;
    public class DatabaseHelper : SQLiteOpenHelper
    {
        private new const string DatabaseName = "MyDatabase.db";
        private const int DatabaseVersion = 1;

        public DatabaseHelper(Context context) : base(context, DatabaseName, null, DatabaseVersion)
        {
        }

        public override void OnCreate(SQLiteDatabase db)
        {
            db.ExecSQL(@"
            CREATE TABLE IF NOT EXISTS NextOn (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Direction TEXT,
            Title TEXT,
            Artist TEXT,
            Album TEXT,
            AlbumArtist TEXT,
            TrackNumber INTEGER,
            Genre TEXT,
            Year INTEGER,
            Comment TEXT,
            Lyrics TEXT,
            CoverArt BLOB,
            Duration INTEGER
            )"); 
            db.ExecSQL(@"
            CREATE TABLE IF NOT EXISTS Queue (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            Name TEXT,
            Direction TEXT,
            Title TEXT,
            Artist TEXT,
            Album TEXT,
            AlbumArtist TEXT,
            TrackNumber INTEGER,
            Genre TEXT,
            Year INTEGER,
            Comment TEXT,
            Lyrics TEXT,
            CoverArt BLOB,
            Duration INTEGER
            )"); 
            // Create your database tables here
            // Example: db.ExecSQL("CREATE TABLE IF NOT EXISTS MyTable (ID INTEGER PRIMARY KEY, Name TEXT)");
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            // Handle database upgrades here if needed
        }
    }
