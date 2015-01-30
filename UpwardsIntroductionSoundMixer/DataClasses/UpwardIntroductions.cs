// ----------------------------------------------------------------------
// <copyright file="UpwardIntroductions.cs" company="Oler Productions">
//     Copyright © Oler Productions. All right reserved
// </copyright>
//
// ------------------------------------------------------------------------

namespace UpwardsIntroductionSoundMixer.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Data Sources for Upward Introductions
    /// </summary>
    public class UpwardIntroductions
    {
        /// <summary>
        /// Gets or sets the team introductions.
        /// </summary>
        /// <value>
        /// The team introductions.
        /// </value>
        public List<TeamIntroduction> TeamIntroductions { get; set; }

        /// <summary>
        /// Gets or sets the introduction musics.
        /// </summary>
        /// <value>
        /// The introduction musics.
        /// </value>
        public List<IntroductionMusic> IntroductionMusics { get; set; }

        /// <summary>
        /// Gets or sets the other music.
        /// </summary>
        /// <value>
        /// The other music.
        /// </value>
        public List<IntroductionMusic> OtherMusic { get; set; }

        /// <summary>
        /// Gets or sets the queue.
        /// </summary>
        /// <value>
        /// The queue.
        /// </value>
        public List<Tuple<TeamIntroduction, IntroductionMusic>> Queue { get; set; }

        /// <summary>
        /// Loads the upward intros.
        /// </summary>
        /// <returns>A The Upward DataSource</returns>
        public static UpwardIntroductions LoadUpwardIntros()
        {
            UpwardIntroductions upwardIntros = new UpwardIntroductions();
            upwardIntros.TeamIntroductions = new List<TeamIntroduction>();
            upwardIntros.IntroductionMusics = new List<IntroductionMusic>();
            upwardIntros.OtherMusic = new List<IntroductionMusic>();
            upwardIntros.Queue = new List<Tuple<TeamIntroduction, IntroductionMusic>>();

            string[] teams = Directory.GetFiles(Properties.Settings.Default.teams_folder);
            teams = teams.OrderBy(t => t.ToString()).ToArray();
            foreach (string team in teams)
            {
                if (Path.GetExtension(team).Length == 4)
                {
                    string name = Path.GetFileNameWithoutExtension(team);
                    string[] splitname = name.Split('-');
                    if (!name.StartsWith("."))
                    {
                        upwardIntros.TeamIntroductions.Add(new TeamIntroduction() { FilePath = team, TeamName = splitname[0].Trim(), Coach = splitname[1].Trim(), Name = name });
                    }
                }
            }

            string[] musics = Directory.GetFiles(Properties.Settings.Default.intromusic_folder);
            musics = musics.OrderBy(m => m.ToString()).ToArray();
            foreach (string music in musics)
            {
                if (Path.GetExtension(music).Length == 4)
                {
                    string name = Path.GetFileNameWithoutExtension(music);
                    if (!name.StartsWith("."))
                    {
                        upwardIntros.IntroductionMusics.Add(new IntroductionMusic() { FilePath = music, Name = name });
                    }
                }
            }

            string[] others = Directory.GetFiles(Properties.Settings.Default.othermusic_folder);
            foreach (string music in others)
            {
                if (Path.GetExtension(music).Length == 4)
                {
                    string name = Path.GetFileNameWithoutExtension(music);
                    if (!name.StartsWith("."))
                    {
                        upwardIntros.OtherMusic.Add(new IntroductionMusic() { FilePath = music, Name = name });
                    }
                }
            }

            upwardIntros.OtherMusic.Shuffle();

            return upwardIntros;
        }

        /// <summary>
        /// Creates the fake queue.
        /// </summary>
        public void CreateFakeQueue()
        {
            this.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(this.TeamIntroductions[0], this.IntroductionMusics[0]));
            this.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(this.TeamIntroductions[1], this.IntroductionMusics[1]));
            this.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(this.TeamIntroductions[2], this.IntroductionMusics[2]));
            this.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(this.TeamIntroductions[3], this.IntroductionMusics[0]));
        }
    }
}
