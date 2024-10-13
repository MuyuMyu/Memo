using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Memo.Common.Converters
{
    // 将 Color 类型转换为 Brush 类型的转换器，用于在数据绑定中使用
    [ValueConversion(typeof(Color), typeof(Brush))]
    public class ColorToBrushConverter : IValueConverter
    {
        // 将 Color 转换为 Brush
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 如果输入值是 Color 类型，则转换为 SolidColorBrush 类型
            if (value is Color color)
            {
                return new SolidColorBrush(color);// 使用 SolidColorBrush 包装 Color 对象
            }
            return Binding.DoNothing;// 如果值不是 Color 类型，返回 Binding.DoNothing 表示不进行绑定更新
        }

        // 将 Brush 转换回 Color
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 如果输入值是 SolidColorBrush 类型，则提取其中的 Color 值
            if (value is SolidColorBrush brush)
            {
                return brush.Color;// 返回 SolidColorBrush 中的 Color 值
            }
            return default(Color);// 如果值不是 SolidColorBrush 类型，返回默认的 Color 值（通常为透明）
        }
    }
}
