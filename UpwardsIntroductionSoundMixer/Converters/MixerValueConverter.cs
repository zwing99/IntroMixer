// -----------------------------------------------------------------------
// <copyright file="MixerValueConverter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace UpwardsIntroductionSoundMixer.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MixerValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int returnValue = 0;
            int valueint = (int)(double)value;
            string param = parameter.ToString();
            if (param == "Right")
            {
                if (valueint > 100) returnValue = 100;
                else returnValue = valueint;
            }
            if (param == "Left")
            {
                if (valueint < 100) returnValue = 100;
                else returnValue = 200 - valueint;
            }

            return returnValue / 100.0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
