   M S B A M L   `   `   `  ����?  ;Memo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:MemoMemo  5   	   1clr-namespace:Memo.ExtensionsMemo.Extensions  5   	   1clr-namespace:Memo.ViewModelsMemo.ViewModels  5   0       Memo.Views.MainView   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   localclr-namespace:Memo  5   	   'extclr-namespace:Memo.Extensions  5   	   _ [Microsoft.Xaml.Behaviors, Version=1.1.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a3i+http://schemas.microsoft.com/xaml/behaviors  5   	   [ WPrism.DryIoc.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59T	 PPrism.Wpf, Version=8.1.97.5141, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&prismhttp://prismlibrary.com/  	 5   	   0
viewmodelsclr-namespace:Memo.ViewModels  	 60     � Title$  
MainWindow��5   	   $
��1280��5   	   $	��768��5   	   ! 	 Prism.Mvvm.ViewModelLocator   AutoWireViewModel True5   	    � AllowsTransparency$
 True��5   	      MaterialDesignPaper#���   5   	   $��微软雅黑)�5   	   $��13(�5   	   $��Regular"�5   	     MaterialDesignBody#���  5   	   &   System.Windows.Media.TextOptions   TextFormattingMode$ Ideal=�5   	      TextRenderingMode$
 Auto=�5   	    � WindowStartupLocation$ CenterScreen=�5   	    � WindowStyle$
 None=�5   	   .��5$      )  #MaterialDesignThemes.Wpf.DialogHost  -   5          Name$ 
DialogHost��5%          DialogTheme$ Inherit=�5$       	   
Identifier$
	 Root��5&      .��5-   
   )  #MaterialDesignThemes.Wpf.DrawerHost  -   5        
  Name$
 
drawerHost��5.         IsLeftDrawerOpen 5/      ��  �� ElementName$ MenuToggleButton�� �� Path$ 	IsChecked�   LeftDrawerContent 50      ]� 51      $	��220��6   .+�52      �� $
��0,20q�6!   $	��Top=�6/   .+�53      �� $��50��54      $��50��55      $��/Images/yun.jpg��56       J� Clip 57      @� 58   "    @� Center$ 25,25&�59   %    @� RadiusX$ 25Q�5:   %    @� RadiusY$ 25Q�5;   %   5=      �� 5>      $
��0,10q�5?      $��Center=�5@      ��5A      �� *
UserName+5B      �� 5D      -   5         ��Name$ menuBar��5E        MyListBoxItemStyle#��[ 5F      ��5G      �� *
MenuBars+*  $Microsoft.Xaml.Behaviors.Interaction   Triggers 5I      +  %Microsoft.Xaml.Behaviors.EventTrigger  5J         	EventName$ SelectionChanged��6-   *  $Microsoft.Xaml.Behaviors.TriggerBase   Actions. 5K   "   2  ,Microsoft.Xaml.Behaviors.InvokeCommandAction     Command 68   �� *NavigateCommand+   CommandParameter 5L   8   �� *SelectedItem+ �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType ListBox5M      ��5O      �� 5P      .R�5Q   "   �� 
������  �� Orientation$ 
Horizontal=�6F   .+�5R   &   '	  !MaterialDesignThemes.Wpf.PackIcon	  $
��15,0q�6>    	  Kind 6L   �� *Icon+�� 5S   &   $
��10,0q�60   ��6>   �� *Title+5T   #   5U      5W      5X      .��5Z      �  � RowDefinitions 5[      �� 5\      $
��auto �6$   �� 5]      .+�5_      '
  !MaterialDesignThemes.Wpf.Snackbar
  -   5         
 Name$ Snackbar��5_   *     K� ZIndex$  1��6<   ! 
  MessageQueue! 6M   4  .MaterialDesignThemes.Wpf.MessageQueueExtension  (  "MaterialDesignThemes.Wpf.ColorZone  5`      -   5        "  Name$" 	ColorZone��5b   "   $��5q�5a   "   .  (MaterialDesignThemes.Wpf.ElevationAssist#   	Elevation$	# Dp4=�5c   "   $	��Top=�5d   "   $   Mode$$ 
PrimaryMid=�5e   "   .��5f      ]� $
��True��6!   .+�5h      �� $��Right=�6%   $ 
Horizontal=�6<   .+�5k      �� -   5        % ��Name$% btnMin��5l   !   $	��—��5m   !    ! MaterialDesignFlatMidBgButton#��[ 5n   !   �� 5p      -   5        $% btnMax��5q   !   $	��☐��5r   !   #��[ 5s   !   �� 5u      -   5        $% btnClose��5v   !   $	��✕��5w   !   #��[ 5x   !   5y      '  !MaterialDesignThemes.Wpf.PopupBox  5{      $��Right=�62   &   ToggleContent& 5|      �� 5}   "   $��25��5~   !   $��25��5   !   $��/Images/yun.jpg��5�   !    5�   &   @� 5�   *   $ 	12.5,12.5&�5�   )   $
 12.5Q�5�   )   $
 12.5Q�5�   )   5�   #   '   PopupContent.' 5�      �� .+�5�   "   �� $��个人中心��6N   ��6)   �� *AppCenterCommand+6N   �� 5�   "   $��注销当前账户��6M   ��6)   �� *LoginOutCommand+6M   5�      5�      �� 5�      $ 
Horizontal=�6%   .+�5�      d� -	   5        ( d�Name$( MenuToggleButton��5�   !   4  .System.Windows.Automation.AutomationProperties)   Name$) HamburgerToggleButton��5�       * d� 	IsChecked$* FalseY�5�        ' #MaterialDesignHamburgerToggleButton#��[ 5�       �� 5�      $��24,0,0,0q�6%     MaterialDesignToolButton#��[ 5�   "   + � ToolTip$+ Previous Item��5�   "   +  %MaterialDesignThemes.Wpf.RippleAssist,   Feedback, 5�   "   ��  �� *Self+$ 
Foreground�  BrushRoundConverter- �� 	Converter#- [ ��5�   "   �� *GoBackCommand+��5�   "   0  *MaterialDesignThemes.Wpf.PackIconExtension  .   Kind$. 	ArrowLeft=�/   Size/ 24��5�   "   ��  �� " �$ (TextElement.Foreground)�5�   "   �� 5�      $��16,0,0,0q�6%   #��[ 5�   "   $+ 	Next Item��5�   "   , 5�   "   ��  �� *Self+$ 
Foreground�#- [ ��5�   "   �� *GoForwardCommand+��5�   "     $. 
ArrowRight=�/ 24��5�   "   ��  �� " �$ (TextElement.Foreground)�5�   "   �� 5�      $��16,0,0,0q�6(   $��Center=�5�      $��Center=�5�      $%) Material Design In XAML Toolkit��5�      $��22(�5�      $��	备忘录��5�      5�      5�      5�      �� 5�      $��1��6!   "   Memo.Extensions.PrismManager0   MainViewRegionName! 	 Prism.Regions.RegionManager1   
RegionName#1 Z0 6.   5�      5�      5�      5�      