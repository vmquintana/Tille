   M S B A M L   `   `   `  ����C  ?TilleWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TilleWPF.DomainTilleWPF.Domain  5      1clr-namespace:TilleWPF.DomainTilleWPF.Domain  5           TilleWPF.Domain.SearchView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      V RMaterialDesignThemes.Wpf, Version=2.5.0.1205, Culture=neutral, PublicKeyToken=nullFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:TilleWPF.Domain     5      0domainclr-namespace:TilleWPF.Domain     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $f `pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Button.xaml?�6%   �� 5      $d ^pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Card.xaml?�6%   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.CheckBox.xaml?�6%   �� 5      $g apack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Flipper.xaml?�6%   �� 5      $h bpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Popupbox.xaml?�6%   �� 5      $q kpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.DataGrid.ComboBox.xaml?�6%   �� 5      $g apack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Shadows.xaml?�6%   �� 5      $g apack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.TextBox.xaml?�6%   �� 5      $j dpack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.DialogHost.xaml?�6%   �� 5      $f `pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.Slider.xaml?�6%   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5       � RowDefinitions 5    
   �� 5!      $
��auto �6   �� 5"      $��* �6   �� 5#      $
��auto �6   .+�5%   
   �� $��18(�6   $��5q�6"   $
��Bold"�6-   Resultado de la búsqueda6Y   �� 5&   
   $��1��6   .@�5'      �� $��Estadísticas��6   .��5(      �  5)      �� 5*      $��* �6+   .+�5,      �� -   5         ��Name$ statisticsSearch��5,   !   $y�Visible=�6;   $��0��6P   .+�5-      � $��	18 10 5 5q�6    5.      �� 5/   "   $
��Auto �60   �� 50   "   $��* �60   �� 51   "   $
��Auto �60   .+�54      �� $��0��6)   $��10 5 5 0q�66   $
��Left=�6H   .+�55   "   � .+�56   &   �� $��0��61   $��Center=�6A   $��5q�6\    �� Orientation$ 
Horizontal=�6g   .+�57   *   �� $
��Bold"�64   $��16(�6F    �� Padding$ 0 0 10 0q�6T   �� 6h   $�Estadísticas��6l   6�   �� 58   *   -   5        $!��monthYearCurrentStatsSearch��58   4   $��Light"�6W   $��14(�6j   �� 6y   $�
Abril/2019��6}   6�   59   '   5:   #   5;      � 5>      $��1��6#   $��7 0 7 7q�60    5?   "   �� 5@   &   $
��Auto �67   �� 5A   &   $
��Auto �67   �� 5B   &   $
��Auto �67   .+�5D   "   �� $��0��6-   $��	10 4 10 4q�6=   .+�5F   *   � $��32��6/    5G   .   �� 5H   2   $��* �6C   �� 5I   2   $��* �6C   .+�5K   .   �� $��0��69   $��Center=�6I   .+�5L   2   �� $
��Bold"�6<   $��14(�6N   �� 6^   $�Gastos��6b   6s   5M   /   �� 5O   .   $��1��6:   $ 
Horizontal=�6J   $��Right=�6c   .+�5P   2   '  !MaterialDesignThemes.Wpf.PopupBox     MaterialDesignToolPopupBox#��[  6J   $��2 0 2 0q�6~      PopupContent. 5Q   6   �� .+�5R   :   �� -   5        	 ��Name$)	 #expensesToggleDescriptionVisibility��5R   A   $��Mostrar descripciones��6n   6�   �� 5S   :   $��Editar��6A   5T   7   5U   3   5V   /   5W   +   &   System.Windows.Controls.DataGrid  5Y   *   -   5        
  Name$
 dataGridExpensesSearch��5Z   3      AutoGenerateColumns .  $	��380��5Y   P   $	��Top=�5Z   S   |�.     CanUserAddRows .     CanUserDeleteRows .     CanUserReorderColumns .     Columns 5]   .   0  *System.Windows.Controls.DataGridTextColumn  5^   2   ,  &System.Windows.Controls.DataGridColumn   MinWidth$ 50Q�6^      Header$ Acción��6m      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *Type+6}     5_   2   $ 96Q�6e   $ Descripción��6t    .    
Visibility$ Hidden=�6�    6E   �� *Description+6�     5`   2   $ 50Q�6^   $ Fecha��6m    .  6E   �� *Date+6|     5a   2   $ 50Q�6t   $ Costo��6�    .  6E   �� *Cost+ �� StringFormat$ {0:C}��6�   5c   +     5e   *   -   5        $"
 datagridFooterExpensesSearch��5e   3    5f   .     5g   2   $ 50Q�6E     5h   2   $ 50Q�6E   $ Total��6S     5i   2   $ 50Q�6E   5k   +   5l   '   �� 5m   "   $��1��6-   $��4 4 10 4q�6=   .+�5o   "   � $��32��6'    5p   &   �� 5q   *   $��* �6;   �� 5r   *   $��* �6;   .+�5t   &   �� $��0��61   $��Center=�6A   .+�5u   *   �� $
��Bold"�64   $��14(�6F   �� 6U   $�	Ganancias��6Y   6m   5v   '   �� 5x   &   $��1��62   $ 
Horizontal=�6B   $��Right=�6[   .+�5z   *     #��[  6B   $��2 0 2 0q�6v   . 5{   .   �� .+�5|   2   �� -   5        $'	 !profitToggleDescriptionVisibility��5|   9   $��Mostrar descripción��6d   6�   �� 5}   2   $��Editar��69   5~   /   5   +   5�   '   5�   #     5�   "   -   5        $
 dataGridProfitsSearch��5�   -    .  $	��380��5�   G   $	��Top=�5�   -   |�.   .   .   .   5�   &     5�   *   $ 50Q�6V   $ Acción��6e    .  6=   �� *Type+6u     5�   *   $ 96Q�6]   $ Descripción��6l    . $ Hidden=�6�    6=   �� *Description+6�     5�   *   $ 50Q�6V   $ Fecha��6e    .  6=   �� *Date+6t     5�   *   $ 50Q�6l   $ Monto��6{    .  6=   �� *Cost+$ {0:C}��6�   5�   #     5�   "   -   5        $!
 datagridFooterProfitsSearch��5�   8   $��2��6+    5�   &     5�   *   $ 50Q�6=     5�   *   $ 50Q�6=   $ Total��6K     5�   *   $ 50Q�6=   5�   #   5�      �� 5�   "   $��2��6-   $��4 4 10 4q�6=   .+�5�   &   � $��32��6+   $	��200��67   .+�5�   *   �� $��0��65   $��Center=�6E   .+�5�   .   �� $
��Bold"�68   $��14(�6J   $ 2 0 2 0q�6X   �� 6k   $�Resúmen��6o   6�   5�   +   5�   '     5�   &   -	   5        $
 dataGridStatsSearch��5�   3    .  $	��Top=�5�   3   |�.   .   .   .   5�   *     5�   .   $	 116Q�6\   $ Acción��6l    .  6A   �� *Action+6|     5�   .   $ 50Q�6q   $ Total��6�    .  6A   �� *Total+$ {0:C}��6�   5�   '     5�   &   -
   5        $
 datagridFooterStatsSearch��5�   <   $��2��6/    5�   *     5�   .   $	 116Q�6A   $ Ganancia(Neta)��6P     5�   .   $ 50Q�6A   5�   '   5�   #   5�      5�      5�      5�      5�      �� 5�      $
��Book��6   .��5�      � $��	18 10 5 5q�6    5�      �� 5�      $��0* �6+   �� 5�      $��18 �6+   �� 5�       5�      �� 5�      $
��Auto �6(   �� 5�      $��* �6(   �� 5�      $
��Auto �6(   �� 5�      $
��Auto �6(   .+�5�      �� $��0��6!   $��	10,8,0,10q�6.   $
��Left=�6A   $��3��6\   .+�5�      �  5�      �� 5�   "   $	��500 �63   �� 5�   "   $��* �63   .+�5�      �� $��0��6)   $��Center=�69   $ 
Horizontal=�6T   .+�5�   "   �� $
��Bold"�6,   $��16(�6>   $ 0 0 10 0q�6L   �� 6`   $�Libro��6d   6t   �� 5�   "   -   5        $ ��monthYearCurrentBookSearch��5�   ,   $��Light"�6N   $��14(�6a   �� 6p   $�
Abril/2019��6t   6�   5�      �� 5�      $��1��6*   $ 
Horizontal=�6:   $��Right=�6S   .+�5�   "     #��[  6:   $��2 0 2 0q�6n   . 5�   &   �� .+�5�   *   �� $��
Actualizar��61   �� 5�   *   $
��Baja��61   �� 5�   *   $��Editar��61   5�   '   5�   #   5�      5�      5�        5�      -   5        $
 dataGridBookSearch��5�   /    .  $	��400��5�   /   $	��Top=�5�   /   $��1��5�   /   |�.   .   .   .  $��3��5�   M    5�        5�      $ 50Q�6S   $ Clientes(#)��6b    .  61   �� *ClientsAmount+6w     5�      $ 50Q�6M   $
 Pais��6\    .  61   �� *	Country+6j     5�      $ 50Q�6L   $ Fecha Entrada��6[    .  61   �� *DateIn+6r     5�      $ 50Q�6M   $ Fecha Salida��6\    .  61   �� *	DateOut+6r     5�      $ 50Q�6j   $ Precio(Estimado)��6y    .  61   �� *EstimatedPrice+$ {0:C}��6�     5�      $ 50Q�6a   $ Precio��6p    .  61   �� *Price+$ {0:C}��6�   5�        5�      -   5        $
 datagridFooterBookSearch��5�   ,   $��2��6   $��3��6N    5�        5�      $ 50Q�61     5�      $ 50Q�61   $ Total��6?     5�      $ 50Q�61     5�      $ 50Q�61   5�      5�      5�      �� 5�      $��	Servicios��6   .��5�      � $��	18 10 5 5q�6    5�      �� 5�      $
��Auto �6,   �� 5�      $��* �6,   �� 5�      $
��Auto �6,   .+�5      �� $��0��6%   $��
10 8 10 10q�62   $
��Left=�6F   .+�5     �  5  "   �� 5  &   $	��350 �67   �� 5  &   $��* �67   .+�5  "   �� $��0��6-   $��Center=�6=   $ 
Horizontal=�6X   .+�5  &   �� $
��Bold"�60   $��16(�6B   $ 0 0 8 0q�6P   �� 6c   $�	Servicios��6g   6{   �� 5  &   -   5        $$��monthYearCurrentServicesSearch��5  0   $��Light"�6V   $��14(�6i   �� 6x   $�
Abril/2019��6|   6�   5	  #   �� 5
  "   $��1��6.   $ 
Horizontal=�6>   $��Right=�6W   .+�5  &     #��[  6>   $��2 0 2 0q�6r   . 5  *   �� .+�5  .   �� $
��Baja��65   �� 5  .   $��Editar��65   5  +   5  '   5  #   5     5       5     -   5        $
 dataGridServicesSearch��5  /    .  $	��400��5  /   $	��Top=�5  /   $��1��5  /   |�.   .   .   .   5       5  "   $ 50Q�6N   $
 Tipo��6]    .  65   �� *Type+6k     5  "   $ 50Q�6N   $ Fecha��6]    .  65   �� *Date+6l     5  "   $ 50Q�6U   $ Descripción��6d    .  65   �� *Description+6y     5  "   $ 50Q�6e   $ Precio��6t    .  65   �� *Price+$ {0:C}��6�   5!     5"     5#     5$     �� 5%  
   $��2��6   .+�5&     ��   MaterialDesignFlatButton#��[ 6    �� IsCancel . $	��100��5(     $��Right=�6!   $��10q�6=   $2��,materialDesign:DialogHost.CloseDialogCommand��5)     $��Cancelar��5*     5+     5,     5-     