   M S B A M L   `   `   `  ����?  ;Memo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Memo.ExtensionsMemo.Extensions  5	      'clr-namespace:Memo.Views
Memo.Views  5
          Memo.Views.LoginView   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5      _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a:md1http://materialdesigninxaml.net/winfx/xaml/themes  5      [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 5      *passclr-namespace:Memo.Extensions  	 5	      &localclr-namespace:Memo.Views  	 5
      $	��350��5      $	��600��5      # 	 Prism.Services.Dialogs.Dialog    WindowStyle  5      �� 5   
    �� 
TargetType" �6   .�5      ��  �� Property � �� �� Value$	 600��6&   �� 5       � ��$	 350��6'   �� 5       �@�SizeToContent$ WidthAndHeight=�6.   �� 5       �@�
ResizeMode$ NoResize=�6+   �� 5       �@ WindowStartupLocation$ CenterScreen=�6C   5      .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $
��1.5* �6   �� 5      .+�5   
   '  !MaterialDesignThemes.Wpf.Snackbar  -   5          Name$ LoginSnakeBar��5      $��2��5       K� ZIndex$ 1��5          MessageQueue 5!      4  .MaterialDesignThemes.Wpf.MessageQueueExtension  �� 5#   
   $��50q�6   $��/Images/login.png��6   7  1MaterialDesignThemes.Wpf.Transitions.Transitioner  5%   
   $��1��6    �� SelectedIndex 6*   �� *SelectIndex+	 �� FallbackValue$	 0��.@�5&      <  6MaterialDesignThemes.Wpf.Transitions.TransitionerSlide  .��5'      ]� $��15q�6   $��Center=�6(   .+�5(      �� $
��0,10q�5)      $	��Top=�5*      $��22(�5+      $
��Bold"�5,      $��欢迎使用��5-      �� 5/      $
��0,10q�50      )  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 请输入账号��51      $	��Top=�52      ��53      �� *
UserName+G� 54      $
��0,10q�55      $
 请输入密码��56      $	��Top=�58      (   "Memo.Extensions.PassWordExtensions   PassWord 57      �� *
PassWord+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�*  $Microsoft.Xaml.Behaviors.Interaction   	Behaviors 59      &	    Memo.Extensions.PasswordBehavior	  5:      5<      �� 5>      $��Login��5@      $��登录系统��5A      $	��Top=�5B      ��5?      �� *ExecuteCommand+5B      ]� 5D      $	��0,5q�6    $��False��6-   .+�5E      �� $��注册账号��6$      Triggers 5F      +
  %Microsoft.Xaml.Behaviors.EventTrigger
  5G   "    
  	EventName$ MouseLeftButtonDown��61   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5H   &   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     CommandParameter$ ResgiterPage��6_      Command 6<   �� *ExecuteCommand+6_   5I   #   5K      �� 5L      $��Right=�6$   $��忘记密码?��6;   5M      5N      5O        5Q      .��5R      ]� $��15q�6   $��Center=�6(   .+�5S      �� $
��0,10q�5T      $	��Top=�5U      $��22(�5V      $
��Bold"�5W      $��注册账号��5X      �� 5Z      $	��0,5q�5[      $
 请输入用户名��5\      $	��Top=�5]      ��5^      �� *UserDto.Account+�� 5_      $	��0,5q�5`      $
 请输入账号��5a      $	��Top=�5b      ��5c      �� *UserDto.UserName+G� 5e      $	��0,5q�5f      $
 请输入密码��5g      $	��Top=�5i       5h      �� *UserDto.PassWord+$ TwoWay=�$ PropertyChanged=� 5j      	  5k      5m      G� 5o      $	��0,5q�5p      $
 请再次输入密码��5q      $	��Top=�5s       5r      �� *UserDto.NewPassWord+$ TwoWay=�$ PropertyChanged=� 5t      	  5u      5w      �� 5y      $��Resgiter��5{      $��注册账号��5|      $	��Top=�5}      ��5z      �� *ExecuteCommand+5}      �� 5      $
��0,10q�5�      $��Return��5�      $��返回登录��5�      $	��Top=�5�          MaterialDesignOutlinedButton#��[  5�      ��5�      �� *ExecuteCommand+5�      5�      5�      5�      5�      5�      