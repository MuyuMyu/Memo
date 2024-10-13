using MaterialDesignColors; // 引入 Material Design 颜色库
using MaterialDesignColors.ColorManipulation; // 引入颜色操作库
using MaterialDesignThemes.Wpf; // 引入 Material Design WPF 主题
using Prism.Commands; // 引入 Prism 的命令功能
using Prism.Mvvm; // 引入 Prism 的 MVVM 功能
using System; // 引入系统命名空间
using System.Collections.Generic; // 引入集合
using System.Linq; // 引入 LINQ
using System.Text; // 引入文本
using System.Threading.Tasks; // 引入异步任务
using System.Windows.Media; // 引入 WPF 的媒体功能

namespace Memo.ViewModels
{
    /// <summary>
    /// 皮肤视图模型，用于管理应用程序主题和颜色方案。
    /// </summary>
    public class SkinViewModel : BindableBase
    {
        private bool _isDarkTheme; // 记录是否为深色主题
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value)) // 检查属性是否更改
                {
                    // 修改主题的基础主题
                    ModifyTheme(theme => theme.SetBaseTheme(value ? BaseTheme.Dark : BaseTheme.Light));
                }
            }
        }

        // 获取可用的色板
        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        // 改变色调的命令
        public DelegateCommand<object> ChangeHueCommand { get; private set; }

        private readonly PaletteHelper paletteHelper = new PaletteHelper(); // PaletteHelper 实例

        /// <summary>
        /// 构造函数
        /// </summary>
        public SkinViewModel()
        {
            ChangeHueCommand = new DelegateCommand<object>(ChangeHue); // 初始化改变色调的命令
        }

        /// <summary>
        /// 修改主题的静态方法
        /// </summary>
        /// <param name="modificationAction">修改主题的操作</param>
        private static void ModifyTheme(Action<Theme> modificationAction)
        {
            var paletteHelper = new PaletteHelper(); // 创建 PaletteHelper 实例
            Theme theme = paletteHelper.GetTheme(); // 获取当前主题
            modificationAction?.Invoke(theme); // 应用修改
            paletteHelper.SetTheme(theme); // 设置修改后的主题
        }

        /// <summary>
        /// 改变主题色调
        /// </summary>
        /// <param name="obj">新的颜色对象</param>
        private void ChangeHue(object obj)
        {
            var hue = (Color)obj; // 将参数转换为 Color 对象
            Theme theme = paletteHelper.GetTheme(); // 获取当前主题

            // 修改主题的色调
            theme.PrimaryLight = new ColorPair(hue.Lighten()); // 变亮
            theme.PrimaryMid = new ColorPair(hue); // 中间色
            theme.PrimaryDark = new ColorPair(hue.Darken()); // 变暗
            paletteHelper.SetTheme(theme); // 设置修改后的主题
        }
    }
}
