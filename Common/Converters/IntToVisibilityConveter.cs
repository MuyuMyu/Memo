using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace Memo.Common.Converters
{
    // 实现 IValueConverter 接口的转换器，用于将 int 转换为 Visibility 类型
    public class IntToVisibilityConveter : IValueConverter
    {
        // 将 int 转换为 Visibility
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 确保输入值不为 null，并成功转换为 int 类型
            if (value != null && int.TryParse(value.ToString(), out int result))
            {
                // 如果转换结果为 0，则返回 Visibility.Visible
                if (result == 0)
                    return Visibility.Visible;
            }
            // 其他情况返回 Visibility.Hidden
            return Visibility.Hidden;
        }

        // ConvertBack 未实现，因为在这种情况下没有需要从 Visibility 转换回 int 的需求
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
