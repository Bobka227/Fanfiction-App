   M S B A M L   `   `   `  ����L  HSemestralka_BSCSH, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Semestralka_BSCSHSemestralka_BSCSH  5   	   "    Semestralka_BSCSH.AutorsPage   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:Semestralka_BSCSH     5   	     � Title$  
AutorsPage��5   	   $	��450��6   $	��800��6)    � 	Resources 5	         SearchBoxStyle �� 
TargetType �� Property �� Value��%?  &	        �� 5
   
   " ��6'   .�5      ��  � �� �#FFFFFF6+   �� 5       � �� �#2222226+   �� 5       � �� �#CCCCCC6,   �� 5       � ��$ 1q�60   �� 5       � ��$ 16(�6)   �� 5       � ��$ 10q�6(   �� 5       � ��$ Center=�69   5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   .+�5   
   ]� ���#333$��50��6&   $��0��62    K� ZIndex$ 1��6?   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�62   .+�5      �� -   5        $��
Add Author��5      
�������$	��5,0q�6A   
������ $��0q�5   2   �� 5      $��Authors��6   
�������
������ $��0q�6W   $	��5,0q�6k   �� 5       $��Popular��6   
�������
������ $��0q�6W   $	��5,0q�6k   5!      �� 5"      -   5         ��Name$ 	SearchBox��5"      $	��400��6)   $��50��65   $��10q�6A   $��Center=�5#      $��Right=�61   $��16(�5$      $��6q�6$   $��1q�5%      ���#CCCCCC���#FFFFFF���#222222#��[  5'      6>   5(      � 5*   
   $��1��6   ���#222.+�5+      �� -   5        	 ��Name$	 AuthorsList��5+      
������ 
�������$��20q�5.      $
��Auto=�50      ��51      �� 52      .R�53      ]� $��5q�6$   .+�54      �� -   5        $	��✖��54   %   $��Right=�55   %   
������ 
���  ��$��14(�58   %   $
��Bold"�59   %   $��0q�5:   %   
 � Cursor$

 Hand��5;   %   $��30��5<   %   $��30��5=   %    � ToolTip$ Delete author��5>   %   
 � Tag 5?   %   �� 5@   %   �� 5B      $ 
Horizontal=�6)   .+�5C   "   �� $��50��6B   $��50��6M   $��5q�6Y   ��6(   �� *Avatar+6Y   �� 5D   "   .+�5E   &   �� 
�������$
��Bold"�6Y   ��60   �� *Name+6Y   �� 5F   &   
���������60   �� *Bio+6E   5G   #   5H      5I      5J      5L      5M      5N      5O      