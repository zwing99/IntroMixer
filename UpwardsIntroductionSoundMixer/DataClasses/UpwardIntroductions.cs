// -----------------------------------------------------------------------
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
        /// Creates the fake data.
        /// </summary>
        /// <returns></returns>
        public static UpwardIntroductions CreateFakeData()
        {
            UpwardIntroductions upIntros = new UpwardIntroductions();

            upIntros.TeamIntroductions = new List<TeamIntroduction>();
            upIntros.TeamIntroductions.Add(new TeamIntroduction() { Coach = "Zac Oler", TeamName = "Pengins", FilePath = @"C:\Users\olerza\Downloads\mp3Samples\m1.m4a", Name = "Penguins" });
            upIntros.TeamIntroductions.Add(new TeamIntroduction() { Coach = "Becky Oler", TeamName = "Dazzlers", FilePath = @"C:\Users\olerza\Downloads\mp3Samples\m2.mp3", Name = "Dazzlers" });
            upIntros.TeamIntroductions.Add(new TeamIntroduction() { Coach = "Kyle Stark", TeamName = "Singers", FilePath = @"C:\Users\olerza\Downloads\mp3Samples\m3.mp3", Name = "Singers" });

            upIntros.IntroductionMusics = new List<IntroductionMusic>();
            upIntros.IntroductionMusics.Add(new IntroductionMusic() { FilePath = @"C:\Users\olerza\Downloads\mp3Samples\Fingerpicking3.mp3", Name = "Finger Picking 3" });
            upIntros.IntroductionMusics.Add(new IntroductionMusic() { FilePath = @"C:\Users\olerza\Downloads\mp3Samples\Fingerpicking4.mp3", Name = "Finger Picking 4" });
            upIntros.IntroductionMusics.Add(new IntroductionMusic() { FilePath = @"C:\Users\olerza\Downloads\mp3Samples\Fingerpicking5.mp3", Name = "Finger Picking 5" });

            return upIntros;
        }

        public static UpwardIntroductions LoadUpwardIntros()
        {
            UpwardIntroductions upIntros = new UpwardIntroductions();
            upIntros.TeamIntroductions = new List<TeamIntroduction>();
            upIntros.IntroductionMusics = new List<IntroductionMusic>();

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
    }
}
