using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Memo.Common.Converters
{
    // 实现 IValueConverter 接口的转换器，用于将 int 转换为 bool，以及将 bool 转换回 int
    public class IntToBoolConveter : IValueConverter
    {
        // 将 int 转换为 bool
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 确认输入值不为 null，并且能够成功转换为 int
            if (value != null && int.TryParse(value.ToString(), out int result))
            {
                // 如果转换结果为 0，则返回 false
                if (result == 0)
                    return false;
            }
            // 非 0 的情况下返回 true
            return true;
        }

        // 将 bool 转换回 int
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 确认输入值不为 null，并且能够成功转换为 bool
            if (value != null && bool.TryParse(value.ToString(), out bool result))
            {
                // 如果转换结果为 true，则返回 1
                if (result)
                    return 1;
            }
            // 如果是 false 或不能转换，返回 0
            return 0;
        }
    }
}
