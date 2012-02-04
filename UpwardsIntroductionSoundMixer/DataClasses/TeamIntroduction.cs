// ----------------------------------------------------------------------
// <copyright file="TeamIntroduction.cs" company="Oler Productions">
//     Copyright © Oler Productions. All right reserved
// </copyright>
//
// ------------------------------------------------------------------------

namespace UpwardsIntroductionSoundMixer.DataClasses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a Team Introduction Sound File
    /// </summary>
    public class TeamIntroduction : SoundFileBase
    {
        /// <summary>
        /// Gets or sets the name of the team.
        /// </summary>
        /// <value>
        /// The name of the team.
        /// </value>
        public string TeamName { get; set; }

        /// <summary>
        /// Gets or sets the coach.
        /// </summary>
        /// <value>
        /// The coach.
        /// </value>
        public string Coach { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.TeamName, this.Coach);
        }
    }
}
