   M S B A M L   `   `   `  ����J  FFront-End-Three, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Front_End_ThreeFront_End_Three  5   	        Front_End_Three.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Front_End_Three     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��450��6)   ��5	      �� 5
   
    �� Color$ DarkSeaGreen��6    �� Opacity$	 0.6Q�6/   6?   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $	��290 �6   �� 5      $��* �6   .+�5   
   �� -   5         ��Name$ 
MainBorder��5   !   $��1��6    �� Padding$ 30q�66    �� CornerRadius$ 10��6C   $	��280��6U   $��Center=�6e   ._�5      ��  �� Orientation$ Vertical=�6   .+�5      �� -   5         ��Name$ 
SignInText��5      $��10q�60   $��Center=�6<   $��
ExtraLight"�6Z   $��18(�6r   |�.  
�������
Вход6�   �� 5      -   5        $��loginInputBox��5   x      TextBoxStyleInput#���   6L   $��7q�6�   $��Center=�6�   	 � Cursor$
	 Hand��6�   |�. $	��150��6�   $	��2.2q�6�   $��
Логин��6�   .��6�   ��5      �� 5      $ White��6*   $	 0.8Q�68   .��5   *   ��5      �� 5      $ White��6*   $	 0.9Q�68   .��5   +   5      �� 5      -   5        
 ��Name$
 passwordInputBox��5   ]   $��Пароль��6O   $��8q�6w   $��Center=�6�   $
	 Hand��6�   |�. $	��150��6�   $	��2.2q�6�   #���   6�   .��6  ��5      �� 5       $ White��6*   $	 0.8Q�68   .��5!   *   ��5"      �� 5#      $ White��6*   $	 0.9Q�68   6H   .��5$   +   5%      �� 5&      $ Vertical=�6   $
��0 10q�64   .+�5'      �� -   5         ��Name$ 	BtnSignIn��5'   r   $��24��6   $��0q�6,     ButtonStyleSubmit#���  67   $
��10 3q�6c   
�������
�������$��Center=�6�   $��Center=�6�   $
	 Hand��6�   $��70��6�   ��5(      �� 5)      $ Transparent��6.   .��5*   -   
Войти5,      �� 5-      -   5        $ HyperlinkRegistration��5-   f   #���  6   $
��10 3q�6I   $
��0 10q�6X   $��Center=�6�   $
	 Hand��6�   
�������.��5.      �� 
������� �� TextDecorations$
 none{�6>   .�6U   &$Зарегистрироваться5/   1   ��50      �� 51      $ Transparent��6.   53      54      55      56      57      58      