// -----------------------------------------------------------------------
// <copyright file="TeamIntroduction.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UpwardsIntroductionSoundMixer.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TeamIntroduction : SoundFileBase
    {
        public string TeamName { get; set; }

        public string Coach { get; set; }

        public override string  ToString()
        {
            return string.Format("{0} - {1}", this.TeamName, this.Coach);
        }
    }
}
