using PokeTypeWeakness.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace PokeTypeWeakness.Converters
{
    public class TypeQuizIsTypeSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(parameter is Label label)
            {
                string naturalID = label.Text;
                if(value is IEnumerable<string> electedTypeNaturalIDs)
                {
                    return electedTypeNaturalIDs.Contains(naturalID);
                }
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
