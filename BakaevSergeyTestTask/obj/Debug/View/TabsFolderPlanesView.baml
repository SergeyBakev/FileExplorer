   M S B A M L   `   `   `  ����O  KBakaevSergeyTestTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BakaevSergeyTestTaskBakaevSergeyTestTask  5   	   O,clr-namespace:BakaevSergeyTestTask.ViewModelBakaevSergeyTestTask.ViewModel  5   	   E'clr-namespace:BakaevSergeyTestTask.ViewBakaevSergeyTestTask.View  5	   	   G(clr-namespace:BakaevSergeyTestTask.ModelBakaevSergeyTestTask.Model  5
   	   4    .BakaevSergeyTestTask.View.TabsFolderPlanesView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:BakaevSergeyTestTask     5   	   ;vm,clr-namespace:BakaevSergeyTestTask.ViewModel     5   	   6vw'clr-namespace:BakaevSergeyTestTask.View     5	   	   8mdl(clr-namespace:BakaevSergeyTestTask.Model     5
   	     � 	Resources  5         FolderPlaneHeader �� DataType,   &BakaevSergeyTestTask.Model.FolderPlane � ToolTip   ButtonOnlyScrollViewerDataVm �� 
TargetType � IsSharedSizeScope � ColumnDefinitions z� SharedSizeGroup �� CanContentScroll  RoundButton  
FolderTabs, �� %ComputedHorizontalScrollBarVisibility  ButtonOnlyScrollViewer	 ��Name  FolderPlaneTabs
 �� 
TargetType �� Property �� Value �� CornerRadius �� Triggers P� Property P� Value �� 
TargetName  FolderTabsSelected  FolderTabsNotSelected��%�  &	        &	 �     7  7  7  7  &	 D    7  &	 �    7  7  �� 5   
   "  61   .R�5      �� $��10,4,8,4q�6   $��10(�6*   
����  ���5      �� *FriendlyName+ 62   �� *FullPathName+5      �� 5   
   " ��6?   .R�5      �  .  5      �� 5      $��* �6'   �� 5      $
��Auto �6'   $ ScrollButtonW��64   .+�5      �� $��0��5      #��z��5      #��z��5      #��z��5      # z��5      #��z��5      �� 5       $��1��6   $��3q�6-   .+�5!      �� $ Scroll Tabs Left��5"      8��  8�� #y�z 5%      $��ScrollBar.PageLeftCommand��5&      $	����5'      $��Marlett)�5(      $��14(�5)      �� 5*      $ Scroll Tabs Right��5+      8�� 8�� #y�z 5.      $ ��ScrollBar.PageRightCommand��5/      $	����50      $��Marlett)�51      $��14(�52      53      54      55      �� 56   
   " ��69   .R�57      �� $	 ScrollViewer��6   8��  .��58      �� 59      5:      �� 5;   
   "
 ��6(   .�5<      ��  � �� 5=      �� 5>      " ��6&   .R�5?      �� $��ListboxItemBorder��6!   $ 	10,10,0,0��6:   $��3,3,3,0q�6S   ._�5@      �� 5A       5B      P� 5C       �@��
IsSelected . . �5D   "   �� $ ListboxItemBorder��6)   8    � ��6^   5E      P� 5F       �@��
IsSelected .  . �5G   "   �� $ ListboxItemBorder��6)   8   � ��6^   5H      5J      5L      5M      5N      .��5O      �  5P   
   �� 5Q      $
��Auto �6   �� 5R      $��* �6   �� 5S      $
��Auto �6   .+�5U   
   �� $��0��6   $��3q�6%   .+�5V      �� -   5         ��Name$ CloseCurrentTab��5W   	   #��[ 5X   	   $ Close/remove Current Tab��5Y   	   $	����5Z   	   $��WebDings)�5[   	   
���Gc����5]   	   �� *CloseTabCommand+�� 5^      -   5        $ FolderUp��5_   	   #��[ 5`   	   $ FolderUp��5a   	   $	����5b   	   $��	WingDings)�5c   	   
��� �����5e   	   �� *FolderUpCommand+5f      �� 5g   
   -   5         ��Name$ FolderPlanesHeaders��5i      $��1��5h      #��[  5l      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5m      #��[ 5n      #��[ 5o      ��5j      �� *FolderPlanes+ �� SelectedItem 5k      �� *SelectedFolderPlane+ �� Mode$ TwoWay=���5q      �� 5r      .R�5s      ��  �� Orientation$ 
Horizontal=�6!   5t      5v      �� 5w   
   $��2��6   $	��100��6#   .��5x      �  � RowDefinitions 5y      �� 5z      $
��Auto �6$   �� 5{      $
��Auto �6$   �� 5|      $
��Auto �6$   .+�5~      �� $	��300��6   $��20��6&   $��1��62   ��6?   �� *
SearchText+$ TwoWay=�6i   �� 5      $��SearchInCurrentDirectory��6   $��0��6<   �� 5�      $��30��6   $��30��6%   $��Search��61   $��2��6B   ��6O   �� *SearchInFolder+6u   5�      5�      5�      5�      