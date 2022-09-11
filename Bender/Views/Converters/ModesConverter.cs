using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Bender.Enums;
using Bender.Extensions;

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