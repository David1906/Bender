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
    public class LabelItemEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var mode = (Modes)values[0];
            var isEditable = (bool)values[1];

            return isEditable || mode != Modes.Fixed && mode != Modes.Disabled;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { false, false };
        }
    }
}