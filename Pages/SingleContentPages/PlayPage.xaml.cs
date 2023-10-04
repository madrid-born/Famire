using Music_Player.Methods;
using Music_Player.Models;

namespace Music_Player.Pages.ViewPages ;

    public partial class PlayPage : ContentPage
    {
        public readonly SongPlayer Player;

        private Song _currentSong;

        private bool _isValueChangedEventSubscribed = true;
        private readonly EventHandler<ValueChangedEventArgs> _progressSliderValueChanged;
        private bool IsSeeking { get; set; } = false;

        public PlayPage(SongPlayer player)
        {
            InitializeComponent();
            Player = player;
            LoadPage();
            UpdateProgressSlider();
            _progressSliderValueChanged = ProgressSlider_ValueChanged;
            ProgressSlider.ValueChanged += _progressSliderValueChanged;
            Player.PlayPageEvent += OnPlayPage;
        }

        private void LoadPage()
        {
            _currentSong = Player.CurrentSong;
            PassedTime.Text = "00:00";
            TotalTime.Text = TimeSpan.FromSeconds(Player.TotalDuration).ToString(@"mm\:ss");
            Title.Text = _currentSong.Title;
            Artist.Text = _currentSong.Artist;
            Album.Text = _currentSong.Album;
            PlayPauseButton.Source = "pause.svg";
            ShuffleButton.Source = Player.Shuffle ? "shuffle2.svg" : "shuffle.svg";
            
            RepeatButton.Source = Player.RepeatState switch {0 => "repeat.svg",1 => "repeat2.svg",_ => "repeat_one.svg"};
            Player.OnPositionChanged += Player_OnPositionChanged;
            var progressTimer = new Timer(UpdateProgressSlider, null, 0, 100);
            var elapsedTimeTimer = new System.Threading.Timer(UpdateElapsedTimeLabel, null, 0, 1000); // Update every 1000 milliseconds (1 second)

            //CoverArt.Source = song.CoverArt;
        }
        
        [Obsolete("Obsolete")]
        private void UpdateElapsedTimeLabel(object state)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (Player is { CurrentPosition: > 0 })
                {
                    
                    TimeSpan elapsedTime = TimeSpan.FromSeconds(Player.CurrentPosition);
                    PassedTime.Text = elapsedTime.ToString(@"mm\:ss"); // Format as mm:ss
                }
            });
        }

        private void UpdateProgressSlider()
        {
            if (IsSeeking || Player is not {TotalDuration: > 0 }) return;
            double progress = Player.CurrentPosition / Player.TotalDuration;
            ProgressSlider.Value = progress;

        }
        
        [Obsolete("Obsolete")]
        private void UpdateProgressSlider(object state)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!IsSeeking && Player is { TotalDuration: > 0 })
                {
                    double progress = Player.CurrentPosition / Player.TotalDuration;

                    // Temporarily unsubscribe from the ValueChanged event
                    if (_isValueChangedEventSubscribed)
                    {
                        ProgressSlider.ValueChanged -= _progressSliderValueChanged;
                        _isValueChangedEventSubscribed = false;
                    }

                    // Set the slider value without triggering the ValueChanged event
                    ProgressSlider.Value = progress;

                    // Re-subscribe to the ValueChanged event
                    ProgressSlider.ValueChanged += _progressSliderValueChanged;
                    _isValueChangedEventSubscribed = true;
                }
            });
        }
        
        private void ProgressSlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (!IsSeeking)
            {
                double newPosition = e.NewValue * Player.TotalDuration;
                Player.SeekTo(newPosition);
            }
        }

        private void Player_OnPositionChanged(object sender, EventArgs e)
        {
            UpdateProgressSlider();
        }

        private void Player_OnStateChanged(object sender, bool isPlaying)
        {
            // Update the PlayPauseButton and other UI elements based on the player state
        }
        
        private void PlayPauseTapped(object sender, EventArgs e)
        {
            if (Player.State)
            {
                Player.Pause();
                PlayPauseButton.Source = "play.svg";
            }
            else
            {
                Player.Resume();
                PlayPauseButton.Source = "pause.svg";
            }
        }

        private void BackTapped(object sender, EventArgs e)
        {
            Player.Previous();
        }
        private void NextTapped(object sender, EventArgs e)
        {
            Player.Next();
        }
        
        private void ShuffleTapped(object sender, EventArgs e)
        {
            Player.ShuffleTap();
            ShuffleButton.Source = Player.Shuffle ? "shuffle2.svg" : "shuffle.svg";
        }

        private void RepeatTapped(object sender, EventArgs e)
        {
            Player.RepeatTap();
            RepeatButton.Source = Player.RepeatState switch {0 => "repeat.svg",1 => "repeat2.svg",_ => "repeat_one.svg"};
        }

        /*private void ColorChanger()
        {
            string svgFilePath = "Music_Player.Resources.Images.repeat.svg";
            using var stream = typeof(PlayPage).Assembly.GetManifestResourceStream(svgFilePath);
            using var reader = new StreamReader(stream);
            var svgContent = reader.ReadToEnd();
            svgContent = svgContent.Replace("#FFFFFF", "#00FFFF");
            //var svgImage = new SvgImage();
            //svgImage.Source = new SvgImageSource(new FileImageSource { File = "modified.svg" });
        }*/
        
        private async void list_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ViewPlays(Player));
        }
        
        private void OnPlayPage(object sender, EventArgs e)
        {
            LoadPage();
        }
    }