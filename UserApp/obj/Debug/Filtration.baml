   M S B A M L   `   `   `  ����J  FFront-End-Three, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Front_End_ThreeFront_End_Three  5   	        Front_End_Three.Filtration   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Front_End_Three     5   	     � Title$  
Filtration��5   	   $	��450��6   $	��700��6)   ��5	      �� 5
   
    �� Color$ DarkSeaGreen��6    �� Opacity$	 0.6Q�6/   6?   .��5      �  � RowDefinitions 5   
   �� 5      $��* �6   6)   �� 5      $��60 �6   6*   �� 5      $��70 �6   6*   �� 5      $	��150��6   6.   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $��20��6   6/   �� 5      $��* �6   6+   �� 5      $	��400��6   60   �� 5      $	��130 �6   6-   �� 5      $��80��6   6/   �� 5      $��20��6   6/   .+�5   
   �� $��Center=�6   $
��Left=�60   $��1��6K   $��2��6X   $
��Thin"�6h   
�������$��40(�6�   Фильтрация6�   �� 5   
   -   5        $��	FindPlane��5   �      ButtonStyleSubmit#���   6   $��40��6=   $��3��6L   $��3��6Y   $��20��6i    � Cursor$
 Hand��6�   $��Фильтровать��6�   $	��100��6�   o� 5   
   -   5        $��MainMenuFind��5   �     	MenuStyle#���  6   $��0 20 0 0q�63   $
��Left=�6E   $	��280��6`   $��18��6o   $��1��6{   $��2��6�   $��2��6�   $	��182��6�   ��5      �� 5       .R�5!      $�  $� Orientation$ Vertical=�6-   5"      .@�5$      m� -   5        $��TopMenuItem��5$   a   $0��*Категория фильтрации...��6     MenuItemStyle#���  69   .@�5%      m� -   5        $��FilterByRating��5%   �   $
 Hand��6   $��0 4 0 0q�6)   $	��6 2q�6:   #���  6H   $2��,Фильтровать по рейтингу��6p   5'      m� 5(      -   5        $��FilterByCost��5(   �   $
 Hand��6   $��0 4 0 0q�6)   $	��6 2q�6:   #���  6H   $9��3Фильтровать по цене ремонта��6p   5*      m� 5+      -   5        $��FilterByVendorCode��5+   �   $
 Hand��6   $��0 4 0 0q�6)   $	��6 2q�6:   #���  6H   $?��9Фильтровать по учетному номеру��6p   5-      5.      5/      �� 51   
    �� Orientation$ 
Horizontal=�6   $��0 20 0 0q�6.   $��2��6@   $��2��6M   .+�52      �� $��От:��6   $��Center=�6$   $��20��6?   
�������6_   �� 53      -   5        $��
FirstValue��53      $��Center=�6(   $��Center=�6E   $��30��6`   $��30��6k   $��0 0 30 0q�6w   6�   �� 54      $��До:��6   $��Center=�6#   $��20��6>   
�������6^   �� 55      -   5        $��SecondValue��55   N   $��Center=�6   $��Center=�63   $��30��6a   $��30��6l   6z   56      �� 57   
   $��2��6   $��3��6%   $	��Top=�62   $ Vertical=�6J   $	��380��6a   $
��Left=�6m   .+�58      �� $ 
Horizontal=�6   $��Center=�62   .+�59      �  5:      �� 5;      $	��105 �6(   67   �� 5<      $��* �6(   65    5>      �� 5?      $	��100 �6+   69   �� 5@      $��* �6+   67   .+�5B      �� -	   5        $��CarImage��5B   n   $��0 0 2 0q�6   $��0��6-   $��0��6=   $ ��Resources/defaultPhoto.png��6J   6n   �� 5C      $	��240��6   $��0��6-   $��1��6:   .@�5D      �� .��5E      �� $ 
Horizontal=�6)   .+�5F   "   �� $��Название: ��6,   $��0 0 10 0q�6>   6R   �� 5G   "   -
   5        $��CarName��5G   8   $
��____��6,   6I   5H      5I      �� 5J      $��0 5 0 0q�6&   .��5K      �� $ 
Horizontal=�6)   .+�5L   "   �� $!��Учетный номер: ��6,   $��0 0 10 0q�6C   6W   �� 5M   "   -   5        $��CarVendorCode��5M   8   $
��____��6,   6O   5N      5O      �� 5P      $��0 5 0 0q�6&   .��5Q      �� $ 
Horizontal=�6)   .+�5R   "   �� $��Цена ремонта: ��6,   $��0 0 10 0q�6B   6V   �� 5S   "   -   5        $��CarCost��5S   8   $
��____��6,   6I   5T      5U      �� 5V      $��0 5 0 0q�6&   .��5W      �� $ 
Horizontal=�6)   .+�5X   "   �� $��Оценка: ��6,   $��0 0 10 0q�6<   6P   �� 5Y   "   -   5        $��CarRate��5Y   8   $
��____��6,   6I   5Z      5[      5\      5]      �� 5_      $��Center=�6   $��10 0 0 0q�68   $ Vertical=�6J   $��Right=�6a   .+�5`      �� -   5        $��	LeftArrow��5`      $��25��6.   $��25��6:   $��0 0 0 5q�6E     ButtonLeftRight#���  6V   $
��🠘��6�   6�   �� 5a      -   5        $��
RightArrow��5a      $��25��6/   $��25��6;   #���  6F   $
��🠚��6p   $
 Hand��6�   6�   5b      5c      5e      �� 5f   
   -   5        $��GoBack��5f   �     ButtonStyleGoBack#���  6   $��1��6=   $��3��6M   $
 Hand��6Z   $��40(�6h   
�������$��«��6�    J� RenderTransformOrigin$ 0.769,0.831&�6�   $��50��6�   $��30��6�   $��0,64,0,0q�6�   $��2��6  ��5g      �� 5h      $ Transparent��6"   68   ��5j      �� 5k      $ Transparent��6"   68   5m      5n      5o      