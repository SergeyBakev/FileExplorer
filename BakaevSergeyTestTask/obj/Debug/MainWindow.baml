   M S B A M L   `   `   `  ����R  NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics  5      O KBakaevSergeyTestTask, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:BakaevSergeyTestTaskBakaevSergeyTestTask 5      O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5	      7)clr-namespace:System.IO;assembly=mscorlib	System.IO 5
      G(clr-namespace:BakaevSergeyTestTask.UtilsBakaevSergeyTestTask.Utils 5      O,clr-namespace:BakaevSergeyTestTask.ViewModelBakaevSergeyTestTask.ViewModel 5      E'clr-namespace:BakaevSergeyTestTask.ViewBakaevSergeyTestTask.View 5      %   BakaevSergeyTestTask.MainWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      >d2http://schemas.microsoft.com/expression/blend/2008     5      Ddiag5clr-namespace:System.Diagnostics;assembly=WindowsBase     5      2local"clr-namespace:BakaevSergeyTestTask     5      Hmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      4sys&clr-namespace:System;assembly=mscorlib     5	      9sysIO)clr-namespace:System.IO;assembly=mscorlib     5
      8utils(clr-namespace:BakaevSergeyTestTask.Utils     5      9vm,clr-namespace:BakaevSergeyTestTask.ViewModel     5      4vw'clr-namespace:BakaevSergeyTestTask.View     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml      5      -   5          �Name$  
NameWindow��5       � DataContext 5      +  %BakaevSergeyTestTask.ViewModel.MainVm  5      -   5        MyMainVm��5       � 	Resources 5         DirInfo �� Orientation �� Path �� Mode��%  &	        �� 5   
   .R�5      �� $ 
Horizontal=�6   .+�5      �� $
��Fill=�5      ��5      �� $ MyIcon�$ OneTime=�5      �� 5      $	��100��5      $��3 5 3 5q�6   ��5      �� $ SelectedPath�$ TwoWay=�5      5      5      5      .��5       �  � IsSharedSizeScope . $
��Left=�6$    � RowDefinitions 5!   
   �� 5"      $
��Auto �6   �� 5#      $��* �6   �� 5$      $
��Auto �6    � ColumnDefinitions 5&   
   �� 5'      $	��300 �6   �� 5(      $
��Auto �6   �� 5)      $��* �6   �� 5*      $	��400 �6   .+�5,   
   �� $��1��5-      $��1��6   $��1��5.      $��Center=�5/      
�������50      �� 51   
   $��0��52      $��3��6   $��
10,5,00,10q�53      
�������$��10(�55      ��54      �� *SelectedPath+$ TwoWay=�.��55   .   	 J� InputBindings	 56      �� 57      

 �� Key$
 Return��6    �� Command 6*   �� *OpenListViewWithTextBox+ �� CommandParameter 58      �� $
 Text� �� RelativeSource ��  �� AncestorType" ��.��59   %   5:      /  )BakaevSergeyTestTask.View.TabbedTreesView  5;   
   $��0��6   $��1��6-    6:   �� *TabbedTrees+� 5<   
   $��1��6   $��2��6    5=      �� 5>      $
��Auto �6    �� 5?      $��* �6    �� 5@      $
��Auto �6    .+�5B      4  .BakaevSergeyTestTask.View.TabsFolderPlanesView  $��0��6&   /  )BakaevSergeyTestTask.View.FolderPlaneView  5C      $��1��6!   5D      �� 5E   
   $��2��6   $��2��6   $��Rebuild Tree��6.   ��6E   �� *RebuildTreeCommand+6n   �� 5F   
    �� CornerRadius$ 6��6   $��4q�6"   $��3��66   $��3��6F   ._�5G      �� .+�5H      �� ��6   �� *" SelectedFolderPlane.FullPathName+6P   �� 5I      ��6   �� *SelectedFolderPlane.Data+6H   �� 5J      ��6   �� *SelectedFolderPlane.Size+6H   �� 5K      ��6   �� *SelectedFolderPlane.Ext+6G   5L      5M      5N      5O      