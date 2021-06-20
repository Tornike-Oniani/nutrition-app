using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace NutritionApp.Converters
{
    class NutrientToColorConverter : IValueConverter
    {

        public ResourceDictionary ColorDictionary { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string nutrient = value as string;
            if (nutrient == "Calories") { return ColorDictionary["SecondaryColorBrush"]; }

            return ColorDictionary["TertiaryColorBrush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
