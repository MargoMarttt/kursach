   M S B A M L   `   `   `  ����J  FFront-End-Three, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Front_End_ThreeFront_End_Three  5   	       Front_End_Three.UserMenu   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Front_End_Three     5   	     � Title$  UserMenu��5   	   $	��400��6   $	��650��6'   ��5	      �� 5
   
    �� ImageSource$ Resources/background.jpg��6   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $	��170 �6   6+   �� 5      $��50��6   6-    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   �� 5      $	��250 �6   6-   �� 5      $��60 �6   6,   �� 5      $��* �6   6+   .+�5   
   �� $��Center=�6   $
��Left=�60   $��0��6K   $��1��6X   $
��Bold"�6h   
�������$��30(�6�   $��111.6,91,0,19.4q�6�   Главная6�   �� 5   
   -   5         ��Name$ GoBack��5   �      ButtonStyleGoBack#���   6   $��Center=�6=   $��Center=�6X   $��2��6u   $��0��6�    � Cursor$
 Hand��6�   $��40(�6�   
�������$��«��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��50��6  $��30��6!  
������ 
������ $��	0,-12,0,0q�6b  o� 5   
   -   5        $��MainMenu��5   �     	MenuStyle#���  6   $	��160��63   $
��Left=�6B   $	��250��6]   $��85��6l   $��1��6{   $��2��6�   $��90.6,131,0,19.8q�6�   $��2��6�   ��5      �� 5      .R�5      $�  $� Orientation$ Vertical=�6-   5      .@�5       m� -   5        $
��Find��5    m   $
 Hand��6     MenuItemStyle#���  6%   $��0 5 0 0q�6M   $��
Найти��6^   5"      m� 5#      -   5        $
��Sort��5#   s   $
 Hand��6   #���  6%   $��0 5 0 0q�6M   $��Сортировать��6^   5%      m� 5&      -   5        $��Filter��5&   s   $
 Hand��6   #���  6%   $��0 5 0 0q�6M   $��Фильтровать��6^   5(      5*      5,      5-      