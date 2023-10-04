using Music_Player.Models;

namespace Music_Player.Methods ;
    
    public class SongPlayer
    {
        public Android.Media.MediaPlayer MediaPlayer;
        public List<Song> OriginalList;
        public Song CurrentSong;
        public List<Song> PreviousOn = new ();
        public List<Song> Queue = new ();
        public List<Song> NextOn = new();
        public bool Shuffle = false;
        public int RepeatState = 0; //0 means no repeat, 1 means repeat this song only, 2 means repeat the list of play
        public bool State;
        public event EventHandler PlayPageEvent;
        public event EventHandler SongsPlayViewEvent;
        public event EventHandler OnPositionChanged;
        public event EventHandler<bool> OnStateChanged;
        public double CurrentPosition => MediaPlayer.CurrentPosition / 1000.0; // in seconds
        public double TotalDuration => MediaPlayer.Duration / 1000.0; // in seconds

        public SongPlayer()
        {
            MediaPlayer = new Android.Media.MediaPlayer();
        }
        
        public void Play()
        {
            MediaPlayer ??= new Android.Media.MediaPlayer();
            MediaPlayer.Completion -= SongEnd;
            MediaPlayer.Reset();
            MediaPlayer.SetDataSource(CurrentSong.Direction);
            MediaPlayer.Prepare();
            MediaPlayer.Start();
            State = true;
            MediaPlayer.Completion += SongEnd; 
        }

        public void SongEnd(object sender, EventArgs e)
        {
            switch (RepeatState)
            {
                case 1:
                    Next();
                    break;
                case 2:
                    Play();
                    break;
                default:
                    if (Queue.Count == 0 && NextOn.Count == 0) 
                    {
                        Pause();
                        
                    }
                    else
                    {
                        Next();
                    }
                    break;
            }
        }

        public void PlayFromPreviousOn(Song sentSong)
        {
            var currentIndex = PreviousOn.FindIndex(song => song.Equals(sentSong));
            NextOn.Insert(0, CurrentSong);
            for (var i = PreviousOn.Count - 1; i > currentIndex; i--)
            {
                NextOn.Insert(0, PreviousOn[^1]);
                PreviousOn.RemoveAt(PreviousOn.Count-1);
            }
            CurrentSong = PreviousOn[^1];
            PreviousOn.RemoveAt(PreviousOn.Count-1);
            Play();
            ReloadPlayPage();
            ReloadSongsPlayView();
        }

        public void PlayFromQueue(Song sentSong)
        {
            var currentIndex = Queue.FindIndex(song => song.Equals(sentSong));
            PreviousOn.Add(CurrentSong);
            CurrentSong = Queue[currentIndex];
            Queue.RemoveAt(currentIndex);
            Play();
            ReloadPlayPage();
            ReloadSongsPlayView();
        }
        
        public void PlayFromNextOn(Song sentSong)
        {
            var currentIndex = NextOn.FindIndex(song => song.Equals(sentSong));
            PreviousOn.Add(CurrentSong);
            for (var i = 0; i < currentIndex; i++)
            {
                PreviousOn.Add(NextOn[0]);
                NextOn.RemoveAt(0);
            }
            CurrentSong = NextOn[0];
            NextOn.RemoveAt(0);
            Play();
            ReloadPlayPage();
            ReloadSongsPlayView();
        }
        
        public void CalculateOrganizedList(Song sentSong)
        {
            PreviousOn = new List<Song>();
            NextOn = new List<Song>();
            CurrentSong = sentSong;
            var currentIndex = OriginalList.FindIndex(song => song.Equals(CurrentSong));
            for (int i = 0; i < currentIndex; i++)
            {
                PreviousOn.Add(OriginalList[i]);
            }
            for (int i = currentIndex + 1; i < OriginalList.Count; i++)
            {
                NextOn.Add(OriginalList[i]);
            }
        }
        
        public void CalculateShuffleList(Song sentSong)
        {
            Random random = new Random();
            PreviousOn = new List<Song>();
            NextOn = new List<Song>();
            CurrentSong = sentSong;
            var tempList = new List<Song>(OriginalList);
            tempList.RemoveAt(tempList.FindIndex(song => song.Equals(CurrentSong)));
            for (var i = 0; i < tempList.Count; i++)
            {
                var j = random.Next(0, tempList.Count);
                NextOn.Add(tempList[j]);
                tempList.RemoveAt(j);
            }
        }

        public void AddToNextOn(Song song)
        {
            NextOn.Insert(0, song);
        }
        
        public void MoveTopNextOn(Song song)
        {
            NextOn.RemoveAt(NextOn.FindIndex(matchSong => matchSong.Equals(song)));
            NextOn.Insert(0, song);
        }
        
        public void AddToQueue(Song song)
        {
            Queue.Add(song);
        }

        public void RemoveFromPrevious(Song song)
        {
            PreviousOn.RemoveAt(PreviousOn.FindIndex(matchSong => matchSong.Equals(song)));
        }

        public void RemoveFromQueue(Song song)
        {
            Queue.RemoveAt(Queue.FindIndex(matchSong => matchSong.Equals(song)));
        }

        public void RemoveFromNextOn(Song song)
        {
            NextOn.RemoveAt(NextOn.FindIndex(matchSong => matchSong.Equals(song)));
        }

        public void ShuffleTap()
        {
            Shuffle = !Shuffle;
            if (Shuffle)
            {
                CalculateShuffleList(CurrentSong);
            }
            else
            {
                CalculateOrganizedList(CurrentSong);
            }

        }

        public void RepeatTap()
        {
            RepeatState = RepeatState switch {1 => 2, 2 => 0,_ => 1};
        }
        
        public void Next()
        {
            if (Queue.Count != 0)
            {
                PreviousOn.Add(CurrentSong);
                CurrentSong = Queue[0];
                Queue.RemoveAt(0);
            }
            else
            {
                if (NextOn.Count != 0)
                {
                    PreviousOn.Add(CurrentSong);
                    CurrentSong = NextOn[0];
                    NextOn.RemoveAt(0);
                }
                else
                {
                    if (PreviousOn.Count !=0)
                    {
                        NextOn.AddRange(PreviousOn);
                        NextOn.Add(CurrentSong);
                        PreviousOn = new List<Song>();
                        CurrentSong = NextOn[0];
                        NextOn.RemoveAt(0);
                    }
                }
            }
            Play();
            ReloadPlayPage();
        }
        
        public void Previous()
        {
            if (PreviousOn.Count != 0 && CurrentPosition < 2)
            {
               NextOn.Insert(0, CurrentSong);
               CurrentSong = PreviousOn[^1];
               PreviousOn.RemoveAt(PreviousOn.Count - 1); 
            }
            Play();
            ReloadPlayPage();
        }
        
        public void Pause()
        {
            if (MediaPlayer is not { IsPlaying: true }) return;
            MediaPlayer.Pause();
            State = false;
        }

        public void Resume()
        {
            if (MediaPlayer is not { IsPlaying: false }) return;
            MediaPlayer.Start();
            State = true;
        }

        public void SeekTo(double positionInSeconds)
        {
            if (MediaPlayer == null) return;
            var newPosition = (int)(positionInSeconds * 1000);
            MediaPlayer.SeekTo(newPosition);
        }

        public void Stop()
        {
            if (MediaPlayer == null) return;
            MediaPlayer.Stop();
            MediaPlayer.Release();
            State = false;
            MediaPlayer = null;
        }
        
        public void ReloadPlayPage()
        {
            PlayPageEvent?.Invoke(this, EventArgs.Empty);
        }
        
        public void ReloadSongsPlayView()
        {
            SongsPlayViewEvent?.Invoke(this, EventArgs.Empty);
        }
    }