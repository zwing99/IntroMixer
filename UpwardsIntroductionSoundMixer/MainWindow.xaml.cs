// ----------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="Oler Productions">
//     Copyright © Oler Productions. All right reserved
// </copyright>
//
// ------------------------------------------------------------------------

namespace UpwardsIntroductionSoundMixer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Timers;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using System.Windows.Threading;
    using UpwardsIntroductionSoundMixer.DataClasses;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The Updward Introduction Data
        /// </summary>
        private UpwardIntroductions upwardIntros;

        /// <summary>
        /// The Timer for delaying stoping and starting
        /// </summary>
        private DispatcherTimer delayTimer;

        /// <summary>
        /// The timer for reseting the other music index
        /// </summary>
        private DispatcherTimer otherMusicTimer;

        /// <summary>
        /// Intro is Playing
        /// </summary>
        private bool isPlaying;

        /// <summary>
        /// The playing index
        /// </summary>
        private int playingIndex = 0;

        /// <summary>
        /// Other music playing
        /// </summary>
        private bool otherMusicPlaying;

        /// <summary>
        /// The other playing index
        /// </summary>
        private int otherMusicPlayingIndex = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Loaded event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.upwardIntros = UpwardIntroductions.LoadUpwardIntros();
            ////upIntros.CreateFakeQueue();
            this.DataContext = this.upwardIntros;
            Teams.Items.Filter = this.FilterTeams;
            Intros.Items.Filter = this.FilterIntros;
            this.isPlaying = false;
            this.Resources.Add(SystemColors.ControlBrushKey, SystemColors.HighlightBrush);
            ////this.Resources.Add(SystemColors.ControlTextBrushKey, SystemColors.HighlightTextBrush);
        }

        /// <summary>
        /// Filters the teams.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>true if filtered</returns>
        private bool FilterTeams(object item)
        {
            if (TeamFilterTextBox.Text == string.Empty)
            {
                return true;
            }

            TeamIntroduction team = item as TeamIntroduction;
            if (team.ToString().ToLower().Contains(TeamFilterTextBox.Text.ToLower()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Filters the intros.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>true if filtered</returns>
        private bool FilterIntros(object item)
        {
            if (IntroFilterTextBox.Text == string.Empty)
            {
                return true;
            }

            IntroductionMusic music = item as IntroductionMusic;
            if (music.ToString().ToLower().Contains(IntroFilterTextBox.Text.ToLower()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Handles the Click event of the PlayStopButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PlayStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.isPlaying)
            {
                this.Stop();
            }
            else
            {
                if (this.upwardIntros.Queue.Count <= 0)
                {
                    return;
                }

                this.playingIndex = this.Queue.SelectedIndex;
                this.Play();
            }
        }

        /// <summary>
        /// Handles the Click event of the PlayStopOtherButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void PlayStopOtherButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.otherMusicPlaying)
            {
                this.OtherMusic.Pause();
                this.otherMusicPlaying = false;
                this.otherMusicTimer = new DispatcherTimer();
                this.otherMusicTimer.Interval = TimeSpan.FromSeconds(5);
                this.otherMusicTimer.Tick += (o, ev) =>
                {
                    this.OtherMusic.Stop();
                    this.otherMusicPlayingIndex++;
                    if (this.otherMusicPlayingIndex >= this.upwardIntros.OtherMusic.Count)
                    {
                        this.otherMusicPlayingIndex = 0;
                    }

                    this.OtherMusic.Source = new Uri(this.upwardIntros.OtherMusic[this.otherMusicPlayingIndex].FilePath);
                    this.otherMusicTimer.Stop();
                };

                this.otherMusicTimer.Start();
            }
            else
            {
                if (this.otherMusicTimer != null)
                {
                    this.otherMusicTimer.Stop();
                }

                if (this.upwardIntros.OtherMusic.Count <= 0)
                {
                    return;
                }

                if (this.OtherMusic.Source == null)
                {
                    this.OtherMusic.Source = new Uri(this.upwardIntros.OtherMusic[this.otherMusicPlayingIndex].FilePath);
                }

                this.OtherMusic.Play();
                this.otherMusicPlaying = true;
            }
        }

        /// <summary>
        /// Handles the MediaEnded event of the OtherMusic control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OtherMusic_MediaEnded(object sender, RoutedEventArgs e)
        {
            this.otherMusicPlayingIndex++;
            if (this.otherMusicPlayingIndex >= this.upwardIntros.OtherMusic.Count)
            {
                this.otherMusicPlayingIndex = 0;
            }

            this.OtherMusic.Source = new Uri(this.upwardIntros.OtherMusic[this.otherMusicPlayingIndex].FilePath);
        }

        /// <summary>
        /// Plays the media.
        /// </summary>
        private void Play()
        {
            this.IntrosMedia.Stop();
            this.TeamsMedia.Stop();
            this.TeamsMedia.Source = new Uri(this.upwardIntros.Queue[this.playingIndex].Item1.FilePath);
            this.IntrosMedia.Source = new Uri(this.upwardIntros.Queue[this.playingIndex].Item2.FilePath);
            this.IntrosMedia.Play();
            this.delayTimer = new DispatcherTimer();
            this.delayTimer.Interval = TimeSpan.FromSeconds(2);
            this.delayTimer.Tick += (o, ev) =>
            {
                this.TeamsMedia.Play();
                this.delayTimer.Stop();
            };

            this.delayTimer.Start();
            this.isPlaying = true;
        }

        /// <summary>
        /// Stops the media.
        /// </summary>
        private void Stop()
        {
            if (this.delayTimer != null)
            {
                this.delayTimer.Stop();
            }

            this.TeamsMedia.Stop();
            this.IntrosMedia.Stop();
            this.isPlaying = false;
            this.playingIndex = 0;
        }

        /// <summary>
        /// Handles the MediaEnded event of the TeamsMedia control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void TeamsMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            Storyboard sb = new Storyboard();
            DoubleAnimation da = new DoubleAnimation(0.0, TimeSpan.FromSeconds(5.0));
            sb.Children.Add(da);
            sb.FillBehavior = FillBehavior.Stop;
            Storyboard.SetTarget(da, IntrosMedia);
            Storyboard.SetTargetProperty(da, new PropertyPath(MediaElement.VolumeProperty));
            sb.Completed += (o, ev) =>
            {
                IntrosMedia.Stop();
                playingIndex += 1;
                if (playingIndex < this.upwardIntros.Queue.Count)
                {
                    this.Play();
                }
                else
                {
                    isPlaying = false;
                }
            };

            IntrosMedia.BeginStoryboard(sb);
        }

        /// <summary>
        /// Handles the Click event of the AddToQueue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void AddToQueue_Click(object sender, RoutedEventArgs e)
        {
            this.upwardIntros.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(Teams.SelectedItem as TeamIntroduction, Intros.SelectedItem as IntroductionMusic));
            this.Queue.Items.Refresh();
            if (this.upwardIntros.Queue.Count == 1)
            {
                this.Queue.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.KeyEventArgs"/> instance containing the event data.</param>
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            this.Teams.Items.Filter = this.FilterTeams;
            this.Intros.Items.Filter = this.FilterIntros;
        }

        /// <summary>
        /// Handles the Click event of the ClearQueue control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void ClearQueue_Click(object sender, RoutedEventArgs e)
        {
            this.upwardIntros.Queue.Clear();
            this.Queue.Items.Refresh();
        }
    }
}
