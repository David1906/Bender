using Bender.Enums;
using Bender.Extensions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System;

namespace Bender.Views.Converters
{
    public class StateCRUDToIsEnabled : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var state = (StatesCRUD)value;
            return state == StatesCRUD.Update || state == StatesCRUD.Create;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return false;
        }
    }
}