using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Binding5
{
    public class IntegerToBooleanConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, string language )
        {
            int num = int.Parse( value.ToString() );
            if ( num != 0 )
                return true;
            else
                return false;

        }

        public object ConvertBack( object value, Type targetType, object parameter, string language )
        {
            throw new NotImplementedException();
        }
    }
}
