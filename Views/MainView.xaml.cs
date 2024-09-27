using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;

namespace Memo.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();


            //最小化
            btnMin.Click += (s, e) => { this.WindowState = WindowState.Minimized; };

            //最大化
            btnMax.Click += (s, e) =>
            {
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                    btnMax.Content = "☐";
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    btnMax.Content = "❐";
                }
            };

            //关闭窗口
            btnClose.Click += async (s, e) =>
            {

                this.Close();
            };

            //拖动窗口
            ColorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };

            //双击放大或缩小窗口
            ColorZone.MouseDoubleClick += (s, e) =>
            {
                if (this.WindowState == WindowState.Normal)
                {
                    this.WindowState = WindowState.Maximized;
                    btnMax.Content = "❐";
                }
                else
                {
                    this.WindowState = WindowState.Normal;
                    btnMax.Content = "☐";
                }
                    
            };

            menuBar.SelectionChanged += (s, e) =>
            {
                drawerHost.IsLeftDrawerOpen = false;
            };

        }
    }
}
