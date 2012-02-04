// ----------------------------------------------------------------------
// <copyright file="SoundFileBase.cs" company="Oler Productions">
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
    /// Base for Sound Files
    /// </summary>
    public abstract class SoundFileBase
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the file path.
        /// </summary>
        /// <value>
        /// The file path.
        /// </value>
        public string FilePath { get; set; }
    }
}
