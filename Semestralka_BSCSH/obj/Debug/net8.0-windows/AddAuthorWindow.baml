   M S B A M L   `   `   `  ����  Semestralka_BSCSH'    !Semestralka_BSCSH.AddAuthorWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
Add Author��5      $	��360��6   $	��400��6!    � WindowStartupLocation$ CenterScreen=�5   	   ���#1E1E1E
������� � 
ResizeMode$ NoResize=�5   	   .��5	      �� $��10q�6    �� Padding$ 15q�6    �� CornerRadius$ 12��6&   ���#2B2B2B._�5
   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��10 �6    �� 5      $
��Auto �6    �� 5      $��80 �6    �� 5      $��20 �6    �� 5      $
��Auto �6    .+�5      �� $��Name:��6   $��0��6%   $
��Bold"�62   $��14(�6D   �� 5      -   5         ��Name$ NameTextBox��5      $��1��6+   $��0,5,0,0q�68   ���#1E1E1E
����������#444$��1q�5   )   $��14(�5      $��6q�6$   �� 5"      $
��Bio:��6   $��3��6$   $
��Bold"�61   $��14(�6C   �� 5#      -   5        $ 
BioTextBox��5#      $��4��6*   $��0,5,0,0q�67   ���#1E1E1E
����������#444$��1q�5%   )   $��14(�5&      $��6q�6$    �� TextWrapping$
 Wrap=�60    �� AcceptsReturn$
 True��6D   �� 5)      -   5        $��➕ Add Author��5)      $��6��6,   $��40��69   $��0,10,0,0q�6E   
��� d �
�������$��16(�5*   ?   $
��Bold"�5+      $��0q�6'   	 � Cursor$
	 Hand��6;   ��5-      �� 5.      
 �� 
TargetType"
 ��6&   .R�5/      �� #��z��6!   $ 6��6K   ._�50      �� $��Center=�6/   $��Center=�6L   51      52      ��54      �� 55       �� 
TargetType" ��6    �� Triggers 56      P� 57       P� Property �@J�IsMouseOver P� Value$
 True��6=   . �58   "   ��  �� Property � �� �� Value �#228B226?   59      P� 5:       � ��$
 True��6;   . �5;   "   ��  � �� �#1A661A6?   5<      5>      5@      5B      5C      5D      