using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UpwardsIntroductionSoundMixer.DataClasses;
using System.Timers;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace UpwardsIntroductionSoundMixer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private UpwardIntroductions upIntros;
        private bool isPlaying;
        int playingItem = 0;

        public MainWindow()
        {
            InitializeComponent();

            upIntros = UpwardIntroductions.LoadUpwardIntros();
            //upIntros.CreateFakeQueue();
            this.DataContext = upIntros;
            Teams.Items.Filter = FilterTeams;
            Intros.Items.Filter = FilterIntros;
            isPlaying = false;
        }

        private bool FilterTeams(object item)
        {
            if (TeamFilterTextBox.Text == string.Empty)
                return true;

            TeamIntroduction team = item as TeamIntroduction;
            if (team.ToString().ToLower().Contains(TeamFilterTextBox.Text.ToLower()))
                return true;

            return false;
        }

        private bool FilterIntros(object item)
        {
            if (IntroFilterTextBox.Text == string.Empty)
                return true;

            IntroductionMusic music = item as IntroductionMusic;
            if (music.ToString().ToLower().Contains(IntroFilterTextBox.Text.ToLower()))
                return true;

            return false;
        }

        private void PlayStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (isPlaying)
            {
                this.Stop();
            }
            else
            {
                if (upIntros.Queue.Count <= 0)
                    return;
                playingItem = this.Queue.SelectedIndex;
                this.Play();
            }
        }

        private void Play()
        {

            IntrosMedia.Stop();
            TeamsMedia.Stop();
            TeamsMedia.Source = new Uri(upIntros.Queue[playingItem].Item1.FilePath);
            IntrosMedia.Source = new Uri(upIntros.Queue[playingItem].Item2.FilePath);
            IntrosMedia.Play();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += (o, ev) =>
            {
                TeamsMedia.Play();
                timer.Stop();
            };
            timer.Start();
            isPlaying = true;
        }

        private void Stop()
        {
            if (timer != null)
                timer.Stop();
            TeamsMedia.Stop();
            IntrosMedia.Stop();
            isPlaying = false;
            playingItem = 0;
        }

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
                playingItem += 1;
                if (playingItem < upIntros.Queue.Count)
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

        private void AddToQueue_Click(object sender, RoutedEventArgs e)
        {
            this.upIntros.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(Teams.SelectedItem as TeamIntroduction, Intros.SelectedItem as IntroductionMusic));
            this.Queue.Items.Refresh();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Teams.Items.Filter = FilterTeams;
            Intros.Items.Filter = FilterIntros;
        }

        private void ClearQueue_Click(object sender, RoutedEventArgs e)
        {
            this.upIntros.Queue.Clear();
            this.Queue.Items.Refresh();
        }
    }
}
