using Bender.Enums;
using Bender.Extensions;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System;

namespace Bender.Views.Converters
{
    public class ModesConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Modes)value).ToDescriptionString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return EnumExtensions.GetValueFromDescription<Modes>((string)value);
        }
    }
}