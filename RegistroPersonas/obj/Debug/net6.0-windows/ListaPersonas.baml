   M S B A M L   `   `   `  ����K  GRegistroPersonas, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:RegistroPersonasRegistroPersonas  5   	   $    RegistroPersonas.ListaPersonas   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:RegistroPersonas     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 
MainWindow��5	   	   $	��450��6   $	��800��6)   .��5
      �  � RowDefinitions 5   
   �� 5      $��70 �6   6*   �� 5      $��* �6   6)   �� 5      $��45 �6   6*   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 
dgPersonas��5      $��1��6'   $��20q�64      AutoGenerateColumns$ False��6@      
IsReadOnly$
 True��6\      SelectionMode$ Single=�6n      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Nombre Completo��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6>   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6H   �� *
FullName+6g     5      $ Correo electrónico��6%   $	 * 6A   
 6K   �� *EmailAddress+6n     5      $ 	Teléfono��6%   $	 * 67   
 6A   �� *PhoneNumber+6c     5      $ Tarjeta de Crédito��6%   $	 * 6A   
 6K   �� *
CardNumber+6l   5      �� 5   
   $��Lista Personas��6   $��Center=�6)   $	��Top=�6F   $��20(�6^   $
��Bold"�6l   $��0,19,0,0q�6~   $��0��6�   �� 5   
   -   5         ��Name$ btnEliminar��5      $��Eliminar��6&   $
��Left=�69   $��20,0,0,0q�6T   $��2��6f   $��Center=�6s   $	��110��6�   6�   �� 5   
   -   5        $ 	btnEditar��5      $��Editar��6$   $
��Left=�65   $��	161,0,0,0q�6P   $��2��6c   $��Center=�6p   $	��110��6�   6�   �� 5   
   -   5        $ btnRegistrar��5      $��Registrar Nuevo��6'   $
��Left=�6A   $��	670,0,0,0q�6\   $��2��6o   $��Center=�6|   $	��110��6�   6�   �� 5   
   -   5        $��Recargar��5      $
��Left=�6$   $��
690,19,0,0q�6?   $	��Top=�6S   $��90��6k   6v   5      5      