   M S B A M L   `   `   `  ����I  EAirportProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:AirportProjectAirportProject  5   	       AirportProject.Ticket   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:AirportProject     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Icon$N HC:\Users\Даниил\Desktop\AirportProject\AirportProject\icon_air.png��5   	    � Title$/ )Авиакомпания Ласточка��5
   	   $	��450��6'   $	��800��64   .��5      � ��5   
   �� 5       �� ImageSource$ samoliot-nebo-sled-2.jpg��6   .+�5   
   �� -   5        $��
Назад��5      $
��Left=�6!   $��338,354,0,0q�6<   $	��Top=�6Q   $	��120��6i   
����������	#FF006CC7$��30��6�   
������� �� 	ClickMode$ Press=�6�   �� 5   
   -   5         ��Name$ from_country��5      $
��Left=�6)   $��
90,165,0,0q�6D   $	��Top=�6X   $	��120��6p   $��Times New Roman)�6�   $��16(�6�   �� 5   
   -   5        $ 
to_country��5      $
��Left=�6'   $��410,165,0,0q�6B   $	��Top=�6W   $	��120��6o   $��Times New Roman)�6�   $��16(�6�   �� 5   
   $ ��Откуда(Страна)��6   $
��Left=�6)   $��
90,134,0,0q�6D   $	��Top=�6X   $	��120��6p   $��Times New Roman)�6|   $��16(�6�   
������� J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5   
   $��Куда(Страна)��6   $
��Left=�6'   $��410,134,0,0q�6B   $	��Top=�6W   $	��120��6o   $��Times New Roman)�6{   $��16(�6�   
������� 5        5      5      �� 5   
   $��
Когда��6   $
��Left=�6    $��338,213,0,0q�6;   $	��Top=�6P   $	��120��6h   $��Times New Roman)�6t   $��16(�6�   
������� 5        5      5       (  "System.Windows.Controls.DatePicker  5!   
   -   5          Name$ date_out��5!      $
��Left=�6'   $��338,243,0,0q�6B   $	��Top=�6W   $	��120��6o   $��Times New Roman)�6{   $��16(�6�   �� 5"   
   -   5         ��Name$ search��5"      $��Искать��6!   $
��Left=�62   $��338,319,0,0q�6M   $	��Top=�6b   $	��120��6z   
����������	#FF006CC7$��30��6�   
�������$ Press=�6�   �� 5#   
   $��Откуда(Город)��6   $
��Left=�6(   $��235,134,0,0q�6C   $	��Top=�6X   $	��120��6p   $��Times New Roman)�6|   $��16(�6�   
������� 5$        5%      5'      �� 5(   
   -   5        $ to_city��5(      $
��Left=�6$   $��535,165,0,0q�6?   $	��Top=�6T   $	��120��6l   $��Times New Roman)�6x   $��16(�6�   �� 5)   
   $��Куда(Город)��6   $
��Left=�6&   $��535,134,0,0q�6A   $	��Top=�6V   $	��120��6n   $��Times New Roman)�6z   $��16(�6�   
������� 5*        5+      5-      �� 5.   
   -   5        $ 	from_city��5.      $
��Left=�6&   $��235,165,0,0q�6A   $	��Top=�6V   $	��120��6n   $��Times New Roman)�6{   $��16(�6�   50      51      