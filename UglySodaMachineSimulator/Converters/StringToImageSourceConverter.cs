using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace UglySodaMachineSimulator.Converters
{
    //[ValueConversion(typeof(string), typeof(BitmapImage))]
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = string.Format("/UglySodaMachineSimulator;component/Images/{0}", value);
            var uri = new Uri(path, UriKind.Absolute);
            return new BitmapImage(uri);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
