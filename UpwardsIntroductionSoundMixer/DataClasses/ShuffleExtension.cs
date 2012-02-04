// ----------------------------------------------------------------------
// <copyright file="ShuffleExtension.cs" company="Oler Productions">
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
    /// Extension for shuffling lists
    /// </summary>
    public static class ShuffleExtension
    {
        /// <summary>
        /// Shuffles the specified list.
        /// </summary>
        /// <typeparam name="T">A Generic Type.</typeparam>
        /// <param name="list">The list.</param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
