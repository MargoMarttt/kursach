   M S B A M L   `   `   `  ����J  FFront-End-Three, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Front_End_ThreeFront_End_Three  5   	        Front_End_Three.ExpertMenu   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Front_End_Three     5   	     � Title$  
ExpertMenu��5   	   $	��400��6   $	��650��6)   ��5	      �� 5
   
    �� ImageSource$ Resources/background.jpg��6   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $	��170 �6   6+   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $	��250 �6   6-   �� 5      $��60 �6   6,   �� 5      $��* �6   6+   .+�5   
   �� $��Center=�6   $
��Left=�60   $��0��6K   $��1��6X   $
��Bold"�6h   
�������$��30(�6�   $��92.6,55,0,5q�6�   Главная6�   �� 5   
   -   5         ��Name$ GoBack��5   �      ButtonStyleGoBack#���   6   $��Center=�6>   $��Center=�6Y   $��2��6v   $��0��6�    � Cursor$
 Hand��6�   $��40(�6�   
�������$��«��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��50��6  $��30��6"  
������ 
������ $��	0,-12,0,0q�6c  o� 5   
   -   5        $��MainMenu��5   �     	MenuStyle#���  6   $	��180��63   $
��Left=�6B   $	��250��6]   $��80��6l   $��1��6{   $��1��6�   $��60.6,0,0,0.4q�6�   ��5      �� 5      .R�5      $�  $� Orientation$ Vertical=�6-   5      .@�5       m� -   5        $
��Find��5    ]     MenuItemStyle#���  6   $
 Hand��6@   $��
Найти��6N   6�   m� 5!      -   5        $
��Sort��5!   b   #���  6   $
 Hand��6?   $��Сортировать��6M   6�   m� 5"      -   5        $��Filter��5"   b   #���  6   $
 Hand��6?   $��Фильтровать��6M   6�   m� 5#      -   5        $��ExpertMethod��5#   q   #���  6   $
 Hand��6?   $8��2Выполнить экспертный метод��6M   6�   5$      5&      5'      