using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//主要用于管理和存储与视图区域相关的常量
namespace Memo.Extensions
{
    /// <summary>
    /// 1. PrismManager 类
    /// 静态类：
    /// PrismManager 被声明为 static 类，这意味着它不能被实例化，所有成员都必须是静态的。这样做的目的是为了提供一组公共常量，便于在应用程序的不同部分使用。
    /// 
    /// 2. 视图区域常量
    /// MainViewRegionName：
    /// 这是一个 public static readonly 字段，表示主视图区域的名称。该名称可以用于在 Prism 的区域管理中标识特定的视图区域。
    /// 用途：当您需要在应用程序的主视图中进行视图导航时，可以使用这个常量作为区域的标识符。
    /// 
    /// SettingsViewRegionName：
    /// 这是另一个 public static readonly 字段，表示设置页区域的名称。
    /// 用途：同样，它可以在应用程序的设置视图中用于视图导航或区域管理。
    /// </summary>
    public static class PrismManager
    {
        /// <summary>
        /// 首页区域
        /// </summary>
        public static readonly string MainViewRegionName = "MainViewRegion";

        /// <summary>
        /// 设置页区域
        /// </summary>
        public static readonly string SettingsViewRegionName = "SettingsViewRegion";
    }
}
