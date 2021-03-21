using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace DesktopClient.Converters
{
    /// <summary>
    /// Class that converts a bool value to a visibility value.
    /// </summary>
    public class InvertableBooleanToVisibilityConverter : IValueConverter
    {
        enum Parameters
        {
            Normal, Inverted
        }

        /// <summary>
        /// Method that converts a bool (true, false) value to a visibility value(Visible, Collapsed).
        /// </summary>
        /// <param name="value">input parameter of bool type</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"> Operation parameter - Normal or Inverted</param>
        /// <param name="culture"></param>
        /// <returns>Visibility value</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            var direction = (Parameters)Enum.Parse(typeof(Parameters), (string)parameter);

            if (direction == Parameters.Inverted)
                return !boolValue ? Visibility.Visible : Visibility.Collapsed;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Method that converts visibility (Visible, Collapsed) to bool (true, false).
        /// </summary>
        /// <param name="value">input parameter of type Visibility</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Operation parameter - Normal or Inverted</param>
        /// <param name="culture"></param>
        /// <returns>bool</returns>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            var VisibilityValue = (Visibility)value;
            var direction = (Parameters)Enum.Parse(typeof(Parameters), (string)parameter);

            if (direction == Parameters.Inverted)
            {
                if (VisibilityValue == Visibility.Visible) return false;
                else return true;
            }

            if (VisibilityValue == Visibility.Visible) return true;
            else return false;
        }
    }
}
