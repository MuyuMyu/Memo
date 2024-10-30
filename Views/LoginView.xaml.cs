using Memo.Extensions;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memo.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView(IEventAggregator aggregator)
        {
            InitializeComponent();

            // 注册提示消息
            aggregator.ResgiterMessage(arg =>
            {
                if (!string.IsNullOrEmpty(arg.Message)) // 检查消息内容是否有效
                {
                    LoginSnakeBar.MessageQueue.Enqueue(arg.Message);
                }
                else
                {
                    // 处理消息内容为空的情况，例如记录日志
                    Console.WriteLine("Received a null or empty message in LoginView.");
                }
            }, "Login");
        }
    }
}
