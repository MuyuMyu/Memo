using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Memo.Extensions
{
    public class PassWordExtensions
    {

        //附加属性 PassWordProperty：
        //使用 DependencyProperty.RegisterAttached 方法注册一个附加属性 PassWord，类型为 string，默认值为 string.Empty，并指定了属性改变时的回调方法 OnPassWordPropertyChanged。

        //GetPassWord 和 SetPassWord 方法：
        //分别用于获取和设置附加属性的值。它们接受一个 DependencyObject，这是 WPF 中所有可依赖对象的基类。

        //OnPassWordPropertyChanged 方法：
        //当 PassWord 属性的值发生变化时，这个方法会被调用。它将当前的 PasswordBox 的密码设置为新值（如果它们不相等）。

        // 获取附加属性的值
        public static string GetPassWord(DependencyObject obj)
        {
            return (string)obj.GetValue(PassWordProperty);
        }

        // 设置附加属性的值
        public static void SetPassWord(DependencyObject obj, string value)
        {
            obj.SetValue(PassWordProperty, value);
        }

        // 定义附加属性
        public static readonly DependencyProperty PassWordProperty =
            DependencyProperty.RegisterAttached("PassWord", typeof(string), typeof(PassWordExtensions), new FrameworkPropertyMetadata(string.Empty, OnPassWordPropertyChanged));

        // 属性改变时的回调方法
        static void OnPassWordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var passWord = sender as PasswordBox;// 尝试将 sender 转换为 PasswordBox
            string password = (string)e.NewValue;// 获取新值

            // 如果 PasswordBox 存在且当前密码与新值不相等，则更新 PasswordBox 的密码
            if (passWord != null && passWord.Password != password)
                passWord.Password = password;
        }
    }

    /// <summary>
    /// OnAttached 方法：
    /// 当行为附加到 PasswordBox 时，这个方法会被调用，注册 PasswordChanged 事件。
    /// 
    /// AssociatedObject_PasswordChanged 方法：
    /// 这是处理密码变化的事件处理程序。当 PasswordBox 的密码发生变化时，它会被调用。此时，它会检查 PasswordBox 的当前密码与附加属性中的密码是否相同。如果不同，则更新附加属性的值。
    /// 
    /// OnDetaching 方法：
    /// 当行为从 PasswordBox 中移除时，会取消对 PasswordChanged 事件的订阅。
    /// </summary>

    public class PasswordBehavior : Behavior<PasswordBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PasswordChanged += AssociatedObject_PasswordChanged;// 订阅 PasswordChanged 事件
        }

        private void AssociatedObject_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox passwordBox = sender as PasswordBox;// 获取引发事件的 PasswordBox
            string password = PassWordExtensions.GetPassWord(passwordBox); // 获取当前密码属性值

            // 如果 PasswordBox 存在且其当前密码与属性值不相等，则更新属性
            if (passwordBox != null && passwordBox.Password != password)
                PassWordExtensions.SetPassWord(passwordBox, passwordBox.Password);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= AssociatedObject_PasswordChanged;// 取消订阅 PasswordChanged 事件
        }
    }
}