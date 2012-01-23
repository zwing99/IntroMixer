﻿// -----------------------------------------------------------------------
// <copyright file="UpwardIntroductions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UpwardsIntroductionSoundMixer.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;

    /// <summary>
    /// TODO: Update summary.
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
        /// Gets or sets the queue.
        /// </summary>
        /// <value>
        /// The queue.
        /// </value>
        public List<Tuple<TeamIntroduction, IntroductionMusic>> Queue { get; set; }

        /// <summary>
        /// Loads the upward intros.
        /// </summary>
        /// <returns></returns>
        public static UpwardIntroductions LoadUpwardIntros()
        {
            UpwardIntroductions upIntros = new UpwardIntroductions();
            upIntros.TeamIntroductions = new List<TeamIntroduction>();
            upIntros.IntroductionMusics = new List<IntroductionMusic>();
            upIntros.Queue = new List<Tuple<TeamIntroduction, IntroductionMusic>>();

            string[] teams = Directory.GetFiles("c:\\upwardintros\\teams\\");
            teams = teams.OrderBy(t => t.ToString()).ToArray();
            foreach (string team in teams)
            {
                string name = Path.GetFileNameWithoutExtension(team);
                string[] splitname = name.Split('-');
                upIntros.TeamIntroductions.Add(new TeamIntroduction() { FilePath = team, TeamName = splitname[0].Trim(), Coach = splitname[1].Trim(), Name = name });
            }

            string[] musics = Directory.GetFiles("c:\\upwardintros\\intromusic\\");
            musics = musics.OrderBy(m => m.ToString()).ToArray();
            foreach (string music in musics)
            {
                string name = Path.GetFileNameWithoutExtension(music);
                upIntros.IntroductionMusics.Add(new IntroductionMusic() { FilePath = music, Name = name });
            }

            return upIntros;
        }

        public void CreateFakeQueue()
        {
            this.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(this.TeamIntroductions[0], this.IntroductionMusics[0]));
            this.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(this.TeamIntroductions[1], this.IntroductionMusics[1]));
            this.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(this.TeamIntroductions[2], this.IntroductionMusics[2]));
            this.Queue.Add(new Tuple<TeamIntroduction, IntroductionMusic>(this.TeamIntroductions[3], this.IntroductionMusics[0]));
        }
    }
}