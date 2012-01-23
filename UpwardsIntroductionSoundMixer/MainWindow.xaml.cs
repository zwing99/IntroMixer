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
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = UpwardIntroductions.LoadUpwardIntros();
            //UpwardIntroductions test = UpwardIntroductions.LoadUpwardIntros();
            //IntrosMedia.SourceUpdated += new EventHandler<DataTransferEventArgs>(IntrosMedia_SourceUpdated);
            Teams.Items.Filter = FilterTeams;
            Intros.Items.Filter = FilterIntros;
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

        void IntrosMedia_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            System.Console.WriteLine("foo: " + IntrosMedia.Source);
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            IntrosMedia.Stop();
            TeamsMedia.Stop();
            IntrosMedia.Play();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += new EventHandler(DelayStart);
            timer.Start();
        }

        void DelayStart(object sender, EventArgs e)
        {
            TeamsMedia.Play();
            timer.Stop();
        }

        //private void StopButton_Click(object sender, RoutedEventArgs e)
        //{
        //    DispatcherTimer timer = new DispatcherTimer();
        //    timer.Interval = TimeSpan.FromSeconds(2);
        //    timer.Tick += new EventHandler(timer_Tick);
        //    timer.Start();
        //}

        //void timer_Tick(object sender, EventArgs e)
        //{
        //    TeamsMedia.Stop();
        //    IntrosMedia.Stop();
        //    ((DispatcherTimer)sender).Stop();
        //}

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            if(timer != null)
                timer.Stop();
            TeamsMedia.Stop();
            IntrosMedia.Stop();
        }


        private void Teams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeamsMedia.Stop();
            IntrosMedia.Stop();
        }

        private void Intros_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeamsMedia.Stop();
            IntrosMedia.Stop();
        }

        private void TeamsMedia_MediaEnded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer timer = new DispatcherTimer();
            Storyboard sb = new Storyboard();
            sb.Completed += (o, ev) =>
            {
                //Binding left = new Binding("Value") { Source = Mixer };
                //left.Converter = (IValueConverter)this.FindResource("MixerConverter");
                //left.ConverterParameter = "Left";
                //Binding right = new Binding("Value") { Source = Mixer };
                //right.Converter = (IValueConverter)this.FindResource("MixerConverter");
                //right.ConverterParameter = "Right";
                IntrosMedia.Stop();
                //IntrosMedia.SetBinding(MediaElement.VolumeProperty, left);
                //TeamsMedia.SetBinding(MediaElement.VolumeProperty, right);
                //Mixer.Value = 50;
            };
            DoubleAnimation da = new DoubleAnimation(0.0, TimeSpan.FromSeconds(2.0));
            sb.Children.Add(da);
            sb.FillBehavior = FillBehavior.Stop;
            Storyboard.SetTarget(da, IntrosMedia);
            Storyboard.SetTargetProperty(da, new PropertyPath(MediaElement.VolumeProperty));
            IntrosMedia.BeginStoryboard(sb);
            //timer.Interval = TimeSpan.FromSeconds(2);
            //timer.Tick += new EventHandler(DelayStop);
            //timer.Start();
        }

        void DelayStop(object sender, EventArgs e)
        {
            IntrosMedia.Stop();
            ((DispatcherTimer)sender).Stop();
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Teams.Items.Filter = FilterTeams;
            Intros.Items.Filter = FilterIntros;
        }
    }
}
