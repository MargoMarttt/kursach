   M S B A M L   `   `   `  ����J  FFront-End-Three, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Front_End_ThreeFront_End_Three  5   	       Front_End_Three.FindAdmin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Front_End_Three     5   	     � Title$  	FindAdmin��5   	   $	��400��6   $	��700��6(   ��5	      �� 5
   
    �� Color$ DarkSeaGreen��6    �� Opacity$	 0.6Q�6/   6?   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $��60 �6   6*   �� 5      $	��100 �6   6+   �� 5      $	��140��6   6.   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��20��6   6/   �� 5      $��* �6   6+   �� 5      $	��400��6   60   �� 5      $	��130 �6   6-   �� 5      $��80��6   6/   �� 5      $��20��6   6/   .+�5   
   �� $��Center=�6   $
��Left=�60   $��1��6K   $��2��6X   $
��Bold"�6h   
�������$��35(�6�   )'Поиск администратора6�   �� 5   
   -   5        $��	DataInput��5   �   $��0 30 0 0q�6      TextBoxStyleInput#���   6R   $��16(�6~   $	��400��6�   $
��Left=�6�   $��Center=�6�   $��40��6�   $��2��6�   $��2��6�   $	��3 5q�6  $��Введите логин��6  63  �� 5   
   -   5        $��FindAdminButton��5   �     ButtonStyleSubmit#���  6   $��40��6=   $��2��6L   $��3��6Y   $��20��6i    � Cursor$
 Hand��6�   $��
Найти��6�   $��80��6�   o� 5   
   -   5        $��MainMenuFind��5   �     	MenuStyle#���  6   $
��Left=�63   $	��250��6N   $��18��6]   $��1��6i   $��2��6v   $��	0,60,0,70q�6�   $��2��6�   $	��122��6�   ��5       �� 5!      .R�5"      $�  $� Orientation$ Vertical=�6-   5#      .@�5%      m� -   5        $��TopMenuItem��5%   ]   $(��"Категория поиска...��6     MenuItemStyle#���  65   .@�5&      m� -   5        $��FindByLogin��5&   �   $
 Hand��6   $��0 4 0 0q�6)   $	��6 2q�6:   #���  6H   $"��Поиск по логину��6p   5(      m� 5)      -   5        $��ShowAll��5)   �   $
 Hand��6   $��0 4 0 0q�6)   $	��6 2q�6:   #���  6H   $&�� Вывод всех данных��6p   5+      5,      5-      �� 5/   
   $��2��6   $��3��6%   $	��Top=�62    �� Orientation$ Vertical=�6J   $	��380��6a   $
��Left=�6m   .+�50      �� .+�51      �  52      �� 53      $	��100 �6(   67   �� 54      $��* �6(   65    56      �� 57      $	��100 �6+   69   �� 58      $��* �6+   67   .+�5:      �� -   5        $��
AdminImage��5:   n   $��0 0 2 0q�6   $��0��6-   $��0��6=   $ ��Resources/defaultPhoto.png��6J   6n   �� 5;      $��0��6   $��1��6+   .@�5<      �� .��5=      �� $ 
Horizontal=�6)   .+�5>   "   �� $��Логин: ��6,   $��0 0 10 0q�6;   6O   �� 5?   "   -   5        $��
AdminLogin��5?   8   $
��____��6,   6L   5@      5A      �� 5B      .��5C      �� $ 
Horizontal=�6)   .+�5D   "   �� $��Статус: ��6,   $��0 0 10 0q�6<   6P   �� 5E   "   -	   5        $��AdminStatus��5E   8   $
��____��6,   6M   5F      5G      �� 5H      .��5I      �� $ 
Horizontal=�6)   .+�5J   "   �� $'��!Последний онлайн: ��6,   $��0 0 10 0q�6F   6Z   �� 5K   "   -
   5        $��AdminLastOnline��5K   8   $
��____��6,   6Q   5L      5M      5N      5O      5P      �� 5Q      $ 
Horizontal=�6   $��Right=�62   $
��0 10q�6N   .+�5R      �� -   5        $��	LeftArrow��5R      $��25��6*   $��25��66   $��0 0 10 0q�6A     ButtonLeftRight#���  6S   $
��🠘��6}   6�   �� 5S      -   5        $��
RightArrow��5S      $��25��6+   $��25��67   #���  6B   $
��🠚��6l   6�   5T      5U      �� 5V   
   -   5        $��GoBack��5V   �     ButtonStyleGoBack#���  6   $��1��6=   $��3��6M   $
 Hand��6Z   $��40(�6h   
�������$��«��6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   $��50��6�   $��30��6�   $��0,68,0,0q�6�   $��2��6  ��5W      �� 5X      $ Transparent��6"   68   ��5Z      �� 5[      $ Transparent��6"   68   5]      5^      5_      