   M S B A M L   `   `   `  ����?  ;Memo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Memo.ExtensionsMemo.Extensions  5	      'clr-namespace:Memo.Views
Memo.Views  5
          Memo.Views.LoginView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 5      *passclr-namespace:Memo.Extensions  	 5	      &localclr-namespace:Memo.Views  	 5
      # 	 Prism.Services.Dialogs.Dialog    WindowStyle  5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value$	 600��6&   �� 5       � ��$	 350��6'   �� 5       �@�SizeToContent$ WidthAndHeight=�6.   �� 5       �@�
ResizeMode$ NoResize=�6+   �� 5       �@ WindowStartupLocation$ CenterScreen=�6C   5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      .+�5   
   '  !MaterialDesignThemes.Wpf.Snackbar  -   5          Name$ LoginSnakeBar��5      $��2��5       K� ZIndex$ 1��5         MessageQueue 5       4  .MaterialDesignThemes.Wpf.MessageQueueExtension  �� 5"   
   $��50q�6   $��/Images/login.png��6   7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  5$   
   $��1��6    �� SelectedIndex 6*   �� *SelectIndex+	 �� FallbackValue$	 0��.@�5%      <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  .��5&      ]� $��15q�6   $��Center=�6(   .+�5'      �� $
��0,10q�5(      $	��Top=�5)      $��22(�5*      $
��Bold"�5+      $��欢迎使用��5,      �� 5.      $
��0,10q�5/      )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 请输入账号��50      $	��Top=�51      ��52      �� *
UserName+G� 53      $
��0,10q�54      $
 请输入密码��55      $	��Top=�57      (   "Memo.Extensions.PassWordExtensions   PassWord 56      �� *
PassWord+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 58      &	    Memo.Extensions.PasswordBehavior	  59      5;      �� 5=      $��Login��5?      $��登录系统��5@      $	��Top=�5A      ��5>      �� *ExecuteCommand+5A      ]� 5C      $	��0,5q�6    $��False��6-   .+�5D      �� $��注册账号��6$      Triggers 5E      +
  %Microsoft.Xaml.Behaviors.EventTrigger
  5F   "    
  	EventName$ MouseLeftButtonDown��61   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5G   &   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter$ ResgiterPage��6_      Command 6<   �� *ExecuteCommand+6_   5H   #   5J      �� 5K      $��Right=�6$   $��忘记密码?��6;   5L      5M      5N        5P      .��5Q      ]� $��15q�6   $��Center=�6(   .+�5R      �� $
��0,10q�5S      $	��Top=�5T      $��22(�5U      $
��Bold"�5V      $��注册账号��5W      �� 5Y      $	��0,5q�5Z      $
 请输入用户名��5[      $	��Top=�5\      ��5]      �� *UserDto.Account+�� 5^      $	��0,5q�5_      $
 请输入账号��5`      $	��Top=�5a      ��5b      �� *UserDto.UserName+G� 5d      $	��0,5q�5e      $
 请输入密码��5f      $	��Top=�5h       5g      �� *UserDto.PassWord+$ TwoWay=�$ PropertyChanged=� 5i      	  5j      5l      G� 5n      $	��0,5q�5o      $
 请再次输入密码��5p      $	��Top=�5r       5q      �� *UserDto.NewPassWord+$ TwoWay=�$ PropertyChanged=� 5s      	  5t      5v      �� 5x      $��Resgiter��5z      $��注册账号��5{      $	��Top=�5|      ��5y      �� *ExecuteCommand+5|      �� 5~      $
��0,10q�5      $��Return��5�      $��返回登录��5�      $	��Top=�5�          MaterialDesignOutlinedButton#��[  5�      ��5�      �� *ExecuteCommand+5�      5�      5�      5�      5�      5�      