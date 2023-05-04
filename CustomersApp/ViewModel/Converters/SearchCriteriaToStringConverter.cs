using System;
using System.Globalization;
using System.Windows.Data;

namespace CustomersApp.ViewModel.Converters;

public class SearchCriteriaToStringConverter : IValueConverter
{
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // no need for that, I guess
        return null;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SearchCriteria criteria)
        {
            switch (criteria)
            {
                case SearchCriteria.NameAndSurname:
                    return $"Wyszukiwanie (Imię i nazwisko)";
                case SearchCriteria.CertificateNumber:
                    return $"Wyszukiwanie (Numer świadectwa)";
                case SearchCriteria.Sex:
                    return $"Wyszukiwanie (Płeć)";
                case SearchCriteria.Address:
                    return $"Wyszukiwanie (Adres zamieszkania)";
                case SearchCriteria.DateOfBirth:
                    return $"Wyszukiwanie (Data urodzenia)";
                case SearchCriteria.PlaceOfBirth:
                    return $"Wyszukiwanie (Miejsce urodzenia)";
                case SearchCriteria.DateOfDeath:
                    return $"Wyszukiwanie (Data zgonu)";
                case SearchCriteria.PlaceOfDeath:
                    return $"Wyszukiwanie (Miejsce zgonu)";
                case SearchCriteria.DeathCertificateNumber:
                    return $"Wyszukiwanie (Numer aktu zgonu)";
                case SearchCriteria.IssueDate:
                    return $"Wyszukiwanie (Data zlecenia)";
                case SearchCriteria.CremationDate:
                    return $"Wyszukiwanie (Data kremacji)";
                case SearchCriteria.IssuedBy:
                    return $"Wyszukiwanie (Akt zgonu wydany przez)";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        return null;
    }
}