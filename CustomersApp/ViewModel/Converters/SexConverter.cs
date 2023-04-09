using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomersApp.ViewModel.Converters;

public class SexConverter : IValueConverter
{
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string sex)
        {
            return sex.ToCharArray()[0];
        }

        return null;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is char sex)
        {
            return sex + "";
        }

        return null;
    }
}