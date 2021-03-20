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
    /// Klasa konwertująca wartość bool na wartość widoczności.
    /// </summary>
    public class InvertableBooleanToVisibilityConverter : IValueConverter
    {
        enum Parameters
        {
            Normal, Inverted
        }

        /// <summary>
        /// Metoda konwertująca wartość bool (true, false) na wartość widoczności (Visible, Collapsed).
        /// </summary>
        /// <param name="value">parametr wejscia typu bool</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Parametr działania - Normal(Normalny) lub Inverted(Odwrotny)</param>
        /// <param name="culture"></param>
        /// <returns>Wartość typu Visibility</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var boolValue = (bool)value;
            var direction = (Parameters)Enum.Parse(typeof(Parameters), (string)parameter);

            if (direction == Parameters.Inverted)
                return !boolValue ? Visibility.Visible : Visibility.Collapsed;

            return boolValue ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Metoda konwertująca wartość widoczności (Visible, Collapsed) na wartość  bool (true, false).
        /// </summary>
        /// <param name="value">parametr wejscia typu Visibility</param>
        /// <param name="targetType"></param>
        /// <param name="parameter">Parametr działania - Normal(Normalny) lub Inverted(Odwrotny)</param>
        /// <param name="culture"></param>
        /// <returns>Wartość typu bool</returns>
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
